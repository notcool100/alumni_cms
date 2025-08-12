use axum::{
    extract::{Path, State},
    http::StatusCode,
    Json,
};
use sqlx::PgPool;
use uuid::Uuid;
use validator::Validate;
use tracing::{info, warn, error};

use crate::models::{ApiResponse, Alumni, CreateAlumniRequest};

pub async fn list_alumni(
    State(pool): State<PgPool>,
) -> Result<Json<ApiResponse<Vec<Alumni>>>, (StatusCode, Json<ApiResponse<String>>)> {
    info!("📋 Listing all public alumni profiles");
    
    let alumni = sqlx::query_as::<_, Alumni>(
        "SELECT * FROM alumni WHERE is_public = true ORDER BY created_at DESC"
    )
    .fetch_all(&pool)
    .await
    .map_err(|e| {
        error!("❌ Database error while listing alumni: {}", e);
        (
            StatusCode::INTERNAL_SERVER_ERROR,
            Json(ApiResponse::error(format!("Database error: {}", e))),
        )
    })?;

    info!("✅ Successfully retrieved {} alumni profiles", alumni.len());
    Ok(Json(ApiResponse::success(alumni)))
}

pub async fn get_alumni(
    State(pool): State<PgPool>,
    Path(id): Path<Uuid>,
) -> Result<Json<ApiResponse<Alumni>>, (StatusCode, Json<ApiResponse<String>>)> {
    info!("👤 Fetching alumni profile with ID: {}", id);
    
    let alumni = sqlx::query_as::<_, Alumni>(
        "SELECT * FROM alumni WHERE id = $1 AND is_public = true"
    )
    .bind(id)
    .fetch_optional(&pool)
    .await
    .map_err(|e| {
        error!("❌ Database error while fetching alumni {}: {}", id, e);
        (
            StatusCode::INTERNAL_SERVER_ERROR,
            Json(ApiResponse::error(format!("Database error: {}", e))),
        )
    })?
    .ok_or_else(|| {
        warn!("⚠️  Alumni profile not found with ID: {}", id);
        (
            StatusCode::NOT_FOUND,
            Json(ApiResponse::error("Alumni not found".to_string())),
        )
    })?;

    info!("✅ Successfully retrieved alumni profile for ID: {}", id);
    Ok(Json(ApiResponse::success(alumni)))
}

pub async fn create_alumni(
    State(pool): State<PgPool>,
    Json(payload): Json<CreateAlumniRequest>,
) -> Result<Json<ApiResponse<Alumni>>, (StatusCode, Json<ApiResponse<String>>)> {
    info!("➕ Creating new alumni profile for user");
    
    // Validate input
    if let Err(e) = payload.validate() {
        warn!("❌ Alumni creation validation failed: {}", e);
        return Err((
            StatusCode::BAD_REQUEST,
            Json(ApiResponse::error(format!("Validation error: {}", e))),
        ));
    }

    // In a real implementation, you'd get the user_id from the JWT token
    let user_id = Uuid::new_v4(); // Placeholder

    let alumni = sqlx::query_as::<_, Alumni>(
        r#"
        INSERT INTO alumni (
            id, user_id, graduation_year, degree, major, current_company, 
            current_position, location, bio, linkedin_url, github_url, 
            website_url, is_public, created_at, updated_at
        )
        VALUES ($1, $2, $3, $4, $5, $6, $7, $8, $9, $10, $11, $12, $13, NOW(), NOW())
        RETURNING *
        "#
    )
    .bind(Uuid::new_v4())
    .bind(user_id)
    .bind(payload.graduation_year)
    .bind(&payload.degree)
    .bind(&payload.major)
    .bind(&payload.current_company)
    .bind(&payload.current_position)
    .bind(&payload.location)
    .bind(&payload.bio)
    .bind(&payload.linkedin_url)
    .bind(&payload.github_url)
    .bind(&payload.website_url)
    .bind(payload.is_public)
    .fetch_one(&pool)
    .await
    .map_err(|e| {
        error!("❌ Database error while creating alumni profile: {}", e);
        (
            StatusCode::INTERNAL_SERVER_ERROR,
            Json(ApiResponse::error(format!("Database error: {}", e))),
        )
    })?;

    info!("✅ Successfully created alumni profile with ID: {}", alumni.id);
    Ok(Json(ApiResponse::success(alumni)))
}

pub async fn update_alumni(
    State(pool): State<PgPool>,
    Path(id): Path<Uuid>,
    Json(payload): Json<CreateAlumniRequest>,
) -> Result<Json<ApiResponse<Alumni>>, (StatusCode, Json<ApiResponse<String>>)> {
    info!("✏️  Updating alumni profile with ID: {}", id);
    
    // Validate input
    if let Err(e) = payload.validate() {
        warn!("❌ Alumni update validation failed for ID {}: {}", id, e);
        return Err((
            StatusCode::BAD_REQUEST,
            Json(ApiResponse::error(format!("Validation error: {}", e))),
        ));
    }

    let alumni = sqlx::query_as::<_, Alumni>(
        r#"
        UPDATE alumni SET
            graduation_year = $1, degree = $2, major = $3, current_company = $4,
            current_position = $5, location = $6, bio = $7, linkedin_url = $8,
            github_url = $9, website_url = $10, is_public = $11, updated_at = NOW()
        WHERE id = $12
        RETURNING *
        "#
    )
    .bind(payload.graduation_year)
    .bind(&payload.degree)
    .bind(&payload.major)
    .bind(&payload.current_company)
    .bind(&payload.current_position)
    .bind(&payload.location)
    .bind(&payload.bio)
    .bind(&payload.linkedin_url)
    .bind(&payload.github_url)
    .bind(&payload.website_url)
    .bind(payload.is_public)
    .bind(id)
    .fetch_optional(&pool)
    .await
    .map_err(|e| {
        error!("❌ Database error while updating alumni {}: {}", id, e);
        (
            StatusCode::INTERNAL_SERVER_ERROR,
            Json(ApiResponse::error(format!("Database error: {}", e))),
        )
    })?
    .ok_or_else(|| {
        warn!("⚠️  Alumni profile not found for update with ID: {}", id);
        (
            StatusCode::NOT_FOUND,
            Json(ApiResponse::error("Alumni not found".to_string())),
        )
    })?;

    info!("✅ Successfully updated alumni profile with ID: {}", id);
    Ok(Json(ApiResponse::success(alumni)))
}

pub async fn delete_alumni(
    State(pool): State<PgPool>,
    Path(id): Path<Uuid>,
) -> Result<Json<ApiResponse<String>>, (StatusCode, Json<ApiResponse<String>>)> {
    info!("🗑️  Deleting alumni profile with ID: {}", id);
    
    let result = sqlx::query("DELETE FROM alumni WHERE id = $1")
        .bind(id)
        .execute(&pool)
        .await
        .map_err(|e| {
            (
                StatusCode::INTERNAL_SERVER_ERROR,
                Json(ApiResponse::error(format!("Database error: {}", e))),
            )
        })?;

    if result.rows_affected() == 0 {
        return Err((
            StatusCode::NOT_FOUND,
            Json(ApiResponse::error("Alumni not found".to_string())),
        ));
    }

    Ok(Json(ApiResponse::success("Alumni deleted successfully".to_string())))
}
