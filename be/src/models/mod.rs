pub mod user;
pub mod alumni;
pub mod event;
pub mod common;

// Re-export commonly used types for convenience
pub use user::{User, UserRole, RegisterRequest, LoginRequest, AuthResponse, UserResponse};
pub use alumni::{Alumni, CreateAlumniRequest};
pub use event::{Event, EventRegistration, RegistrationStatus, CreateEventRequest};
pub use common::ApiResponse;
