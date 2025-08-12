use axum::{
    extract::Request,
    http::{Method, StatusCode, Uri},
    middleware::Next,
    response::Response,
};
use std::time::Instant;
use tracing::{info, warn, error};

/// Middleware to log all incoming API requests with detailed information
pub async fn api_logging_middleware(
    method: Method,
    uri: Uri,
    request: Request,
    next: Next,
) -> Response {
    let start_time = Instant::now();
    
    // Extract useful information from the request
    let user_agent = request
        .headers()
        .get("user-agent")
        .and_then(|h| h.to_str().ok())
        .unwrap_or("Unknown");
    
    let content_type = request
        .headers()
        .get("content-type")
        .and_then(|h| h.to_str().ok())
        .unwrap_or("Unknown");
    
    let content_length = request
        .headers()
        .get("content-length")
        .and_then(|h| h.to_str().ok())
        .unwrap_or("Unknown");
    
    // Log the incoming request
    info!(
        "🚀 API Request: {} {} | User-Agent: {} | Content-Type: {} | Content-Length: {}",
        method,
        uri.path(),
        user_agent,
        content_type,
        content_length
    );
    
    // Process the request
    let response = next.run(request).await;
    
    // Calculate response time
    let duration = start_time.elapsed();
    
    // Extract response information
    let status = response.status();
    let status_code = status.as_u16();
    
    // Log the response with appropriate log level based on status code
    let log_message = format!(
        "📡 API Response: {} {} -> {} ({}ms)",
        method,
        uri.path(),
        status_code,
        duration.as_millis()
    );
    
    match status_code {
        200..=299 => info!("✅ {}", log_message),
        300..=399 => info!("🔄 {}", log_message),
        400..=499 => warn!("⚠️  {}", log_message),
        500..=599 => error!("❌ {}", log_message),
        _ => info!("❓ {}", log_message),
    }
    
    response
}

/// Middleware to log authentication attempts
pub async fn auth_logging_middleware(
    method: Method,
    uri: Uri,
    request: Request,
    next: Next,
) -> Response {
    let path = uri.path();
    
    // Check if this is an authentication-related endpoint
    if path.contains("/auth") {
        let auth_type = if path.contains("login") {
            "LOGIN"
        } else if path.contains("register") {
            "REGISTER"
        } else if path.contains("me") {
            "ME"
        } else {
            "AUTH"
        };
        
        info!("🔐 Authentication attempt: {} {} ({})", method, path, auth_type);
    }
    
    let response = next.run(request).await;
    
    // Log authentication results
    if path.contains("/auth") {
        let status = response.status();
        let status_code = status.as_u16();
        
        match status_code {
            200..=299 => info!("✅ Authentication successful: {} {}", method, path),
            401 => warn!("❌ Authentication failed (Unauthorized): {} {}", method, path),
            403 => warn!("🚫 Authentication forbidden: {} {}", method, path),
            _ => info!("📝 Authentication response: {} {} -> {}", method, path, status_code),
        }
    }
    
    response
}

/// Middleware to log database operations
pub async fn db_logging_middleware(
    method: Method,
    uri: Uri,
    request: Request,
    next: Next,
) -> Response {
    let path = uri.path();
    
    // Check if this is a database operation endpoint
    if path.contains("/alumni") || path.contains("/events") {
        let operation = match method.as_str() {
            "GET" => "READ",
            "POST" => "CREATE",
            "PUT" => "UPDATE",
            "DELETE" => "DELETE",
            _ => "UNKNOWN",
        };
        
        let resource = if path.contains("/alumni") {
            "ALUMNI"
        } else if path.contains("/events") {
            "EVENTS"
        } else {
            "RESOURCE"
        };
        
        info!("🗄️  Database operation: {} {} ({})", operation, resource, path);
    }
    
    let response = next.run(request).await;
    
    // Log database operation results
    if path.contains("/alumni") || path.contains("/events") {
        let status = response.status();
        let status_code = status.as_u16();
        
        match status_code {
            200..=299 => info!("✅ Database operation successful: {} {}", method, path),
            400..=499 => warn!("⚠️  Database operation client error: {} {} -> {}", method, path, status_code),
            500..=599 => error!("❌ Database operation server error: {} {} -> {}", method, path, status_code),
            _ => info!("📝 Database operation response: {} {} -> {}", method, path, status_code),
        }
    }
    
    response
}
