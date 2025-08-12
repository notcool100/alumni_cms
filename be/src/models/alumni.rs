use serde::{Deserialize, Serialize};
use chrono::{DateTime, Utc};
use uuid::Uuid;
use validator::Validate;

#[derive(Debug, Serialize, Deserialize, sqlx::FromRow)]
pub struct Alumni {
    pub id: Uuid,
    pub user_id: Uuid,
    pub graduation_year: i32,
    pub degree: String,
    pub major: String,
    pub current_company: Option<String>,
    pub current_position: Option<String>,
    pub location: Option<String>,
    pub bio: Option<String>,
    pub linkedin_url: Option<String>,
    pub github_url: Option<String>,
    pub website_url: Option<String>,
    pub profile_image_url: Option<String>,
    pub is_public: bool,
    pub created_at: DateTime<Utc>,
    pub updated_at: DateTime<Utc>,
}

#[derive(Debug, Deserialize, Validate)]
pub struct CreateAlumniRequest {
    #[validate(range(min = 1950, max = 2030))]
    pub graduation_year: i32,
    #[validate(length(min = 1))]
    pub degree: String,
    #[validate(length(min = 1))]
    pub major: String,
    pub current_company: Option<String>,
    pub current_position: Option<String>,
    pub location: Option<String>,
    pub bio: Option<String>,
    pub linkedin_url: Option<String>,
    pub github_url: Option<String>,
    pub website_url: Option<String>,
    pub is_public: bool,
}
