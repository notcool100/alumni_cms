use axum::{
    extract::{Path, State},
    http::StatusCode,
    Json,
};
use sqlx::PgPool;
use uuid::Uuid;
use validator::Validate;
use tracing::{info, warn, error};

use crate::models::{ApiResponse, Event, CreateEventRequest};

pub async fn list_events(
    State(pool): State<PgPool>,
) -> Result<Json<ApiResponse<Vec<Event>>>, (StatusCode, Json<ApiResponse<String>>)> {
    info!("📅 Listing all events");
    
    let events = sqlx::query_as::<_, Event>(
        "SELECT * FROM events ORDER BY start_date ASC"
    )
    .fetch_all(&pool)
    .await
    .map_err(|e| {
        error!("❌ Database error while listing events: {}", e);
        (
            StatusCode::INTERNAL_SERVER_ERROR,
            Json(ApiResponse::error(format!("Database error: {}", e))),
        )
    })?;

    info!("✅ Successfully retrieved {} events", events.len());
    Ok(Json(ApiResponse::success(events)))
}

pub async fn get_event(
    State(pool): State<PgPool>,
    Path(id): Path<Uuid>,
) -> Result<Json<ApiResponse<Event>>, (StatusCode, Json<ApiResponse<String>>)> {
    info!("📅 Fetching event with ID: {}", id);
    
    let event = sqlx::query_as::<_, Event>(
        "SELECT * FROM events WHERE id = $1"
    )
    .bind(id)
    .fetch_optional(&pool)
    .await
    .map_err(|e| {
        error!("❌ Database error while fetching event {}: {}", id, e);
        (
            StatusCode::INTERNAL_SERVER_ERROR,
            Json(ApiResponse::error(format!("Database error: {}", e))),
        )
    })?
    .ok_or_else(|| {
        warn!("⚠️  Event not found with ID: {}", id);
        (
            StatusCode::NOT_FOUND,
            Json(ApiResponse::error("Event not found".to_string())),
        )
    })?;

    info!("✅ Successfully retrieved event with ID: {}", id);
    Ok(Json(ApiResponse::success(event)))
}

pub async fn create_event(
    State(pool): State<PgPool>,
    Json(payload): Json<CreateEventRequest>,
) -> Result<Json<ApiResponse<Event>>, (StatusCode, Json<ApiResponse<String>>)> {
    info!("➕ Creating new event: {}", payload.title);
    
    // Validate input
    if let Err(e) = payload.validate() {
        warn!("❌ Event creation validation failed: {}", e);
        return Err((
            StatusCode::BAD_REQUEST,
            Json(ApiResponse::error(format!("Validation error: {}", e))),
        ));
    }

    // In a real implementation, you'd get the created_by from the JWT token
    let created_by = Uuid::new_v4(); // Placeholder

    let event = sqlx::query_as::<_, Event>(
        r#"
        INSERT INTO events (
            id, title, description, location, start_date, end_date,
            max_attendees, current_attendees, is_online, meeting_url,
            created_by, created_at, updated_at
        )
        VALUES ($1, $2, $3, $4, $5, $6, $7, $8, $9, $10, $11, NOW(), NOW())
        RETURNING *
        "#
    )
    .bind(Uuid::new_v4())
    .bind(&payload.title)
    .bind(&payload.description)
    .bind(&payload.location)
    .bind(payload.start_date)
    .bind(payload.end_date)
    .bind(&payload.max_attendees)
    .bind(0) // current_attendees starts at 0
    .bind(payload.is_online)
    .bind(&payload.meeting_url)
    .bind(created_by)
    .fetch_one(&pool)
    .await
    .map_err(|e| {
        (
            StatusCode::INTERNAL_SERVER_ERROR,
            Json(ApiResponse::error(format!("Database error: {}", e))),
        )
    })?;

    Ok(Json(ApiResponse::success(event)))
}

pub async fn update_event(
    State(pool): State<PgPool>,
    Path(id): Path<Uuid>,
    Json(payload): Json<CreateEventRequest>,
) -> Result<Json<ApiResponse<Event>>, (StatusCode, Json<ApiResponse<String>>)> {
    // Validate input
    if let Err(e) = payload.validate() {
        return Err((
            StatusCode::BAD_REQUEST,
            Json(ApiResponse::error(format!("Validation error: {}", e))),
        ));
    }

    let event = sqlx::query_as::<_, Event>(
        r#"
        UPDATE events SET
            title = $1, description = $2, location = $3, start_date = $4,
            end_date = $5, max_attendees = $6, is_online = $7, meeting_url = $8,
            updated_at = NOW()
        WHERE id = $9
        RETURNING *
        "#
    )
    .bind(&payload.title)
    .bind(&payload.description)
    .bind(&payload.location)
    .bind(payload.start_date)
    .bind(payload.end_date)
    .bind(&payload.max_attendees)
    .bind(payload.is_online)
    .bind(&payload.meeting_url)
    .bind(id)
    .fetch_optional(&pool)
    .await
    .map_err(|e| {
        (
            StatusCode::INTERNAL_SERVER_ERROR,
            Json(ApiResponse::error(format!("Database error: {}", e))),
        )
    })?
    .ok_or((
        StatusCode::NOT_FOUND,
        Json(ApiResponse::error("Event not found".to_string())),
    ))?;

    Ok(Json(ApiResponse::success(event)))
}

pub async fn delete_event(
    State(pool): State<PgPool>,
    Path(id): Path<Uuid>,
) -> Result<Json<ApiResponse<String>>, (StatusCode, Json<ApiResponse<String>>)> {
    let result = sqlx::query("DELETE FROM events WHERE id = $1")
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
            Json(ApiResponse::error("Event not found".to_string())),
        ));
    }

    Ok(Json(ApiResponse::success("Event deleted successfully".to_string())))
}
