# Alumni Backend API

A modern Rust-based REST API for the Alumni Management System built with Axum, SQLx, and PostgreSQL.

## Features

- 🔐 JWT-based authentication
- 👥 Alumni profile management
- 📅 Event management and registration
- 🗄️ PostgreSQL database with migrations
- ✅ Input validation
- 🔒 Password hashing with bcrypt
- 🌐 CORS support
- 📝 Comprehensive logging

## Prerequisites

- Rust 1.70+ (install via [rustup](https://rustup.rs/))
- PostgreSQL 12+
- SQLx CLI (optional, for migrations)

## Setup

1. **Install dependencies:**
   ```bash
   cargo install sqlx-cli
   ```

2. **Create PostgreSQL database:**
   ```bash
   createdb alumni_db
   ```

3. **Set up environment variables:**
   Create a `.env` file in the `be` directory:
   ```env
   DATABASE_URL=postgresql://postgres:yourdad@localhost:5432/alumni_db
   JWT_SECRET=your-super-secret-jwt-key-change-in-production
   PORT=3000
   ENVIRONMENT=development
   RUST_LOG=info
   ```

4. **Run database migrations:**
   ```bash
   sqlx migrate run
   ```

5. **Build and run:**
   ```bash
   cargo run
   ```

The API will be available at `http://localhost:3000`

## API Endpoints

### Authentication
- `POST /api/auth/register` - Register a new user
- `POST /api/auth/login` - Login user
- `GET /api/auth/me` - Get current user profile

### Alumni
- `GET /api/alumni` - List all public alumni profiles
- `GET /api/alumni/:id` - Get specific alumni profile
- `POST /api/alumni` - Create new alumni profile
- `PUT /api/alumni/:id` - Update alumni profile
- `DELETE /api/alumni/:id` - Delete alumni profile

### Events
- `GET /api/events` - List all events
- `GET /api/events/:id` - Get specific event
- `POST /api/events` - Create new event
- `PUT /api/events/:id` - Update event
- `DELETE /api/events/:id` - Delete event

### Health Check
- `GET /health` - API health status

## Default Admin Account

The system comes with a default admin account:
- Email: `admin@alumni.com`
- Password: `admin123`

## Development

### Running tests
```bash
cargo test
```

### Database migrations
```bash
# Create new migration
sqlx migrate add migration_name

# Run migrations
sqlx migrate run

# Revert last migration
sqlx migrate revert
```

### Code formatting
```bash
cargo fmt
```

### Linting
```bash
cargo clippy
```

## Project Structure

```
src/
├── main.rs          # Application entry point
├── config.rs        # Configuration management
├── db.rs           # Database connection and setup
├── auth.rs         # Authentication utilities
├── models.rs       # Data models and DTOs
├── handlers/       # API route handlers
│   ├── mod.rs
│   ├── health.rs
│   ├── auth.rs
│   ├── alumni.rs
│   └── events.rs
└── utils/          # Utility functions

migrations/         # Database migrations
```

## Security Features

- Password hashing with bcrypt
- JWT token authentication
- Input validation with validator crate
- CORS configuration
- SQL injection prevention with parameterized queries

## Error Handling

The API uses consistent error responses:
```json
{
  "success": false,
  "data": null,
  "message": "Error description"
}
```

## Logging

The application uses structured logging with the `tracing` crate. Log levels can be configured via the `RUST_LOG` environment variable.
