use chrono::{DateTime, Utc};
use uuid::Uuid;

/// Format a UUID for display
pub fn format_uuid(uuid: &Uuid) -> String {
    uuid.to_string()
}

/// Format a datetime for display
pub fn format_datetime(dt: &DateTime<Utc>) -> String {
    dt.format("%Y-%m-%d %H:%M:%S UTC").to_string()
}

/// Generate a random UUID
pub fn generate_uuid() -> Uuid {
    Uuid::new_v4()
}
