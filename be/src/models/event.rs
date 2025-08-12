use serde::{Deserialize, Serialize};
use chrono::{DateTime, Utc};
use uuid::Uuid;
use validator::Validate;

#[derive(Debug, Serialize, Deserialize, sqlx::FromRow)]
pub struct Event {
    pub id: Uuid,
    pub title: String,
    pub description: String,
    pub location: Option<String>,
    pub start_date: DateTime<Utc>,
    pub end_date: DateTime<Utc>,
    pub max_attendees: Option<i32>,
    pub current_attendees: i32,
    pub is_online: bool,
    pub meeting_url: Option<String>,
    pub created_by: Uuid,
    pub created_at: DateTime<Utc>,
    pub updated_at: DateTime<Utc>,
}

#[derive(Debug, Serialize, Deserialize, sqlx::FromRow)]
pub struct EventRegistration {
    pub id: Uuid,
    pub event_id: Uuid,
    pub user_id: Uuid,
    pub registration_date: DateTime<Utc>,
    pub status: RegistrationStatus,
}

#[derive(Debug, Serialize, Deserialize, sqlx::Type)]
#[sqlx(type_name = "registration_status", rename_all = "lowercase")]
pub enum RegistrationStatus {
    Confirmed,
    Pending,
    Cancelled,
}

#[derive(Debug, Deserialize, Validate)]
pub struct CreateEventRequest {
    #[validate(length(min = 1))]
    pub title: String,
    #[validate(length(min = 1))]
    pub description: String,
    pub location: Option<String>,
    pub start_date: DateTime<Utc>,
    pub end_date: DateTime<Utc>,
    pub max_attendees: Option<i32>,
    pub is_online: bool,
    pub meeting_url: Option<String>,
}
