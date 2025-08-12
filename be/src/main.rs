use axum::{
    routing::{get, post, put, delete},
    Router,
    http::Method,
    middleware,
};
use std::net::SocketAddr;
use tower_http::cors::{CorsLayer, Any};
use tracing_subscriber::{layer::SubscriberExt, util::SubscriberInitExt};

mod handlers;
mod models;
mod db;
mod auth;
mod config;
mod utils;

use config::config::Config;
use db::db::Database;

#[tokio::main]
async fn main() -> Result<(), Box<dyn std::error::Error>> {
    // Load environment variables
    dotenv::dotenv().ok();

    // Initialize tracing
    tracing_subscriber::registry()
        .with(tracing_subscriber::EnvFilter::new(
            std::env::var("RUST_LOG").unwrap_or_else(|_| "info".into()),
        ))
        .with(tracing_subscriber::fmt::layer())
        .init();

    // Load configuration
    let config = Config::from_env()?;
    tracing::info!("Configuration loaded successfully");

    // Initialize database
    let db = Database::new(&config.database_url).await?;
    tracing::info!("Database connection established");

    // Create CORS layer
    let cors = CorsLayer::new()
        .allow_methods([Method::GET, Method::POST, Method::PUT, Method::DELETE])
        .allow_origin(Any)
        .allow_headers(Any);

    // Build application
    let app = Router::new()
        .route("/health", get(handlers::health::health_check))
        .route("/api/auth/register", post(handlers::auth::register))
        .route("/api/auth/login", post(handlers::auth::login))
        .route("/api/auth/me", get(handlers::auth::me))
        .route("/api/alumni", get(handlers::alumni::list_alumni))
        .route("/api/alumni/:id", get(handlers::alumni::get_alumni))
        .route("/api/alumni", post(handlers::alumni::create_alumni))
        .route("/api/alumni/:id", put(handlers::alumni::update_alumni))
        .route("/api/alumni/:id", delete(handlers::alumni::delete_alumni))
        .route("/api/events", get(handlers::events::list_events))
        .route("/api/events", post(handlers::events::create_event))
        .route("/api/events/:id", get(handlers::events::get_event))
        .route("/api/events/:id", put(handlers::events::update_event))
        .route("/api/events/:id", delete(handlers::events::delete_event))
        .with_state(db.pool)
        .layer(cors)
        .layer(middleware::from_fn(utils::logging::api_logging_middleware))
        .layer(middleware::from_fn(utils::logging::auth_logging_middleware))
        .layer(middleware::from_fn(utils::logging::db_logging_middleware));

    // Start server
    let addr = SocketAddr::from(([127, 0, 0, 1], config.port));
    tracing::info!("Starting server on {}", addr);

    let listener = tokio::net::TcpListener::bind(addr).await?;
    axum::serve(listener, app).await?;

    Ok(())
}
