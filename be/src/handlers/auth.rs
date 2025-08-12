use axum::{
    extract::State,
    http::StatusCode,
    Json,
};
use axum_extra::TypedHeader;
use headers::Authorization;
use headers::authorization::Bearer;
use sqlx::PgPool;
use uuid::Uuid;
use tracing::{info, warn, error};

use validator::Validate;

use crate::{
    auth::auth::AuthService,
    models::{ApiResponse, AuthResponse, LoginRequest, RegisterRequest, User, UserResponse},
};

pub async fn register(
    State(pool): State<PgPool>,
    Json(payload): Json<RegisterRequest>,
) -> Result<Json<ApiResponse<AuthResponse>>, (StatusCode, Json<ApiResponse<String>>)> {
    info!("📝 User registration attempt for email: {}", payload.email);
    
    // Validate input
    if let Err(e) = payload.validate() {
        warn!("❌ Registration validation failed for email {}: {}", payload.email, e);
        return Err((
            StatusCode::BAD_REQUEST,
            Json(ApiResponse::error(format!("Validation error: {}", e))),
        ));
    }

    // Check if user already exists
    let existing_user = sqlx::query_as::<_, User>(
        "SELECT * FROM users WHERE email = $1"
    )
    .bind(&payload.email)
    .fetch_optional(&pool)
    .await
    .map_err(|e| {
        (
            StatusCode::INTERNAL_SERVER_ERROR,
            Json(ApiResponse::error(format!("Database error: {}", e))),
        )
    })?;

    if existing_user.is_some() {
        warn!("⚠️  Registration failed: User with email {} already exists", payload.email);
        return Err((
            StatusCode::CONFLICT,
            Json(ApiResponse::error("User with this email already exists".to_string())),
        ));
    }

    // Hash password
    let auth_service = AuthService::new("your-super-secret-jwt-key-change-in-production".to_string());
    let password_hash = auth_service
        .hash_password(&payload.password)
        .map_err(|e| {
            (
                StatusCode::INTERNAL_SERVER_ERROR,
                Json(ApiResponse::error(format!("Password hashing error: {}", e))),
            )
        })?;

    // Create user
    let user_id = Uuid::new_v4();
    let user = sqlx::query_as::<_, User>(
        r#"
        INSERT INTO users (id, email, password_hash, first_name, last_name, role, created_at, updated_at)
        VALUES ($1, $2, $3, $4, $5, $6, NOW(), NOW())
        RETURNING *
        "#
    )
    .bind(user_id)
    .bind(&payload.email)
    .bind(&password_hash)
    .bind(&payload.first_name)
    .bind(&payload.last_name)
    .bind("alumni")
    .fetch_one(&pool)
    .await
    .map_err(|e| {
        (
            StatusCode::INTERNAL_SERVER_ERROR,
            Json(ApiResponse::error(format!("Database error: {}", e))),
        )
    })?;

    // Generate JWT token
    let token = auth_service
        .create_token(user.id, &user.email, &user.role.to_string())
        .map_err(|e| {
            (
                StatusCode::INTERNAL_SERVER_ERROR,
                Json(ApiResponse::error(format!("Token generation error: {}", e))),
            )
        })?;

    let user_response = UserResponse {
        id: user.id,
        email: user.email,
        first_name: user.first_name,
        last_name: user.last_name,
        role: user.role,
    };

    let auth_response = AuthResponse {
        token,
        user: user_response,
    };

    info!("✅ User registration successful for email: {}", payload.email);
    Ok(Json(ApiResponse::success(auth_response)))
}

pub async fn login(
    State(pool): State<PgPool>,
    Json(payload): Json<LoginRequest>,
) -> Result<Json<ApiResponse<AuthResponse>>, (StatusCode, Json<ApiResponse<String>>)> {
    info!("🔑 User login attempt for email: {}", payload.email);
    
    // Validate input
    if let Err(e) = payload.validate() {
        warn!("❌ Login validation failed for email {}: {}", payload.email, e);
        return Err((
            StatusCode::BAD_REQUEST,
            Json(ApiResponse::error(format!("Validation error: {}", e))),
        ));
    }

    // Find user
    let user = sqlx::query_as::<_, User>(
        "SELECT * FROM users WHERE email = $1"
    )
    .bind(&payload.email)
    .fetch_optional(&pool)
    .await
    .map_err(|e| {
        (
            StatusCode::INTERNAL_SERVER_ERROR,
            Json(ApiResponse::error(format!("Database error: {}", e))),
        )
    }    )?
    .ok_or_else(|| {
        warn!("❌ Login failed: User with email {} not found", payload.email);
        (
            StatusCode::UNAUTHORIZED,
            Json(ApiResponse::error("Invalid email or password".to_string())),
        )
    })?;

    // Verify password
    let auth_service = AuthService::new("your-super-secret-jwt-key-change-in-production".to_string());
    let is_valid = auth_service
        .verify_password(&payload.password, &user.password_hash)
        .map_err(|e| {
            (
                StatusCode::INTERNAL_SERVER_ERROR,
                Json(ApiResponse::error(format!("Password verification error: {}", e))),
            )
        })?;

    if !is_valid {
        warn!("❌ Login failed: Invalid password for email {}", payload.email);
        return Err((
            StatusCode::UNAUTHORIZED,
            Json(ApiResponse::error("Invalid email or password".to_string())),
        ));
    }

    // Generate JWT token
    let token = auth_service
        .create_token(user.id, &user.email, &user.role.to_string())
        .map_err(|e| {
            (
                StatusCode::INTERNAL_SERVER_ERROR,
                Json(ApiResponse::error(format!("Token generation error: {}", e))),
            )
        })?;

    let user_response = UserResponse {
        id: user.id,
        email: user.email,
        first_name: user.first_name,
        last_name: user.last_name,
        role: user.role,
    };

    let auth_response = AuthResponse {
        token,
        user: user_response,
    };

    info!("✅ User login successful for email: {}", payload.email);
    Ok(Json(ApiResponse::success(auth_response)))
}

pub async fn me(
    State(pool): State<PgPool>,
    TypedHeader(auth): TypedHeader<Authorization<Bearer>>,
) -> Result<Json<ApiResponse<UserResponse>>, (StatusCode, Json<ApiResponse<String>>)> {
    info!("👤 User profile request received");
    
    // Extract and verify JWT token
    let auth_service = AuthService::new("your-super-secret-jwt-key-change-in-production".to_string());
    
    let claims = auth_service
        .verify_token(auth.token())
        .map_err(|_e| {
            (
                StatusCode::UNAUTHORIZED,
                Json(ApiResponse::error("Invalid or expired token".to_string())),
            )
        })?;

    // Get user ID from claims
    let user_id = Uuid::parse_str(&claims.sub)
        .map_err(|_e| {
            (
                StatusCode::BAD_REQUEST,
                Json(ApiResponse::error("Invalid user ID in token".to_string())),
            )
        })?;

    // Fetch user from database
    let user = sqlx::query_as::<_, User>(
        "SELECT * FROM users WHERE id = $1"
    )
    .bind(user_id)
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
        Json(ApiResponse::error("User not found".to_string())),
    ))?;

    let user_response = UserResponse {
        id: user.id,
        email: user.email,
        first_name: user.first_name,
        last_name: user.last_name,
        role: user.role,
    };

    info!("✅ User profile retrieved successfully for user ID: {}", user.id);
    Ok(Json(ApiResponse::success(user_response)))
}
