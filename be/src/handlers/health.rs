use axum::{http::StatusCode, Json};
use serde_json::json;
use tracing::info;

pub async fn health_check() -> (StatusCode, Json<serde_json::Value>) {
    info!("🏥 Health check endpoint called");
    
    let response = (
        StatusCode::OK,
        Json(json!({
            "status": "healthy",
            "timestamp": chrono::Utc::now().to_rfc3339(),
            "service": "alumni-backend"
        })),
    );
    
    info!("✅ Health check completed successfully");
    response
}
