# Alumni Backend API (.NET)

This is the .NET Core backend API for the Alumni Management System, migrated from the original Rust implementation.

## Features

### 🔐 Authentication
- JWT-based authentication
- User registration and login
- Password hashing with BCrypt
- Role-based access control (alumni, admin)

### 👥 Alumni Management
- Create, read, update, delete alumni profiles
- Public and private profile visibility
- Professional information tracking (company, position, location)
- Social media links (LinkedIn, GitHub, personal website)

### 📅 Event Management
- Create, read, update, delete events
- Event registration system
- Attendee tracking
- Online/offline event support
- Meeting URL support for online events

### 📊 Event Registration
- Register/unregister for events
- Registration status management (pending, confirmed, cancelled)
- Attendee count tracking
- User-specific registration history

### 🏥 Health Monitoring
- Health check endpoint for monitoring
- Database connectivity verification

## API Endpoints

### Authentication
- `POST /api/auth/register` - Register a new user
- `POST /api/auth/login` - Login user
- `GET /api/auth/me` - Get current user profile

### Alumni (Public)
- `GET /api/alumni` - List all public alumni profiles
- `GET /api/alumni/{id}` - Get specific alumni profile

### Alumni (Authenticated)
- `POST /api/alumni` - Create alumni profile
- `PUT /api/alumni/{id}` - Update alumni profile
- `DELETE /api/alumni/{id}` - Delete alumni profile

### Events (Public)
- `GET /api/events` - List all events
- `GET /api/events/{id}` - Get specific event

### Events (Authenticated)
- `POST /api/events` - Create new event
- `PUT /api/events/{id}` - Update event
- `DELETE /api/events/{id}` - Delete event

### Event Registration (Authenticated)
- `GET /api/eventregistration/event/{eventId}` - Get registrations for an event
- `GET /api/eventregistration/user/{userId}` - Get user's registrations
- `POST /api/eventregistration` - Register for an event
- `PUT /api/eventregistration/{id}/status` - Update registration status
- `DELETE /api/eventregistration/{id}` - Cancel registration

### Health
- `GET /api/health` - Health check endpoint

## Technology Stack

- **Framework**: .NET 8.0
- **Database**: PostgreSQL with Entity Framework Core
- **Authentication**: JWT Bearer tokens
- **Password Hashing**: BCrypt.Net-Next
- **API Documentation**: Swagger/OpenAPI
- **Logging**: Built-in .NET logging

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- PostgreSQL database
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd alumni-backend-netcore
   ```

2. **Configure the database**
   - Update the connection string in `appsettings.json`
   - Ensure PostgreSQL is running and accessible

3. **Install dependencies**
   ```bash
   dotnet restore
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

5. **Access the API**
   - API: `https://localhost:5001`
   - Swagger UI: `https://localhost:5001/swagger`

### Configuration

Update `appsettings.json` with your settings:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=5432;Database=alumni_db;User Id=postgres;Password=your_password;"
  },
  "JWT": {
    "Issuer": "YourIssuer",
    "Audience": "YourAudience",
    "Secret": "your-super-secret-jwt-key-change-in-production",
    "ExpiryInHours": 24
  }
}
```

## Database Schema

### Users Table
- `id` (UUID, Primary Key)
- `email` (String, Unique)
- `password_hash` (String)
- `first_name` (String)
- `last_name` (String)
- `role` (String, Default: "alumni")
- `created_at` (DateTime)
- `updated_at` (DateTime)

### Alumni Table
- `id` (UUID, Primary Key)
- `user_id` (UUID, Foreign Key)
- `graduation_year` (Integer)
- `degree` (String)
- `major` (String)
- `current_company` (String, Optional)
- `current_position` (String, Optional)
- `location` (String, Optional)
- `bio` (String, Optional)
- `linkedin_url` (String, Optional)
- `github_url` (String, Optional)
- `website_url` (String, Optional)
- `profile_image_url` (String, Optional)
- `is_public` (Boolean, Default: true)
- `created_at` (DateTime)
- `updated_at` (DateTime)

### Events Table
- `id` (UUID, Primary Key)
- `title` (String)
- `description` (String)
- `location` (String, Optional)
- `start_date` (DateTime)
- `end_date` (DateTime)
- `max_attendees` (Integer, Optional)
- `current_attendees` (Integer, Default: 0)
- `is_online` (Boolean, Default: false)
- `meeting_url` (String, Optional)
- `created_by` (UUID, Foreign Key)
- `created_at` (DateTime)
- `updated_at` (DateTime)

### Event Registrations Table
- `id` (UUID, Primary Key)
- `event_id` (UUID, Foreign Key)
- `user_id` (UUID, Foreign Key)
- `registration_date` (DateTime)
- `status` (Enum: pending, confirmed, cancelled)

## Security Features

- **JWT Authentication**: Secure token-based authentication
- **Password Hashing**: BCrypt for secure password storage
- **Role-based Access**: Different permissions for alumni and admin users
- **Input Validation**: Comprehensive request validation
- **CORS Configuration**: Configurable cross-origin resource sharing

## Migration from Rust

This .NET implementation maintains feature parity with the original Rust backend:

### ✅ Implemented Features
- All CRUD operations for alumni and events
- JWT authentication system
- Event registration functionality
- Health check endpoint
- Comprehensive logging
- Input validation
- Error handling

### 🔄 Key Differences
- **Language**: C# instead of Rust
- **Framework**: ASP.NET Core instead of Axum
- **ORM**: Entity Framework Core instead of SQLx
- **Authentication**: Built-in JWT middleware
- **Validation**: Data annotations instead of validator crate

### 📊 Performance Considerations
- Entity Framework Core provides excellent performance for most use cases
- JWT token validation is handled by the built-in middleware
- Database queries are optimized with proper indexing
- Async/await pattern for non-blocking operations

## Development

### Adding New Features
1. Create models in `Models/Model.cs`
2. Add DbSet to `Data/AlumniContext.cs`
3. Create controller in `Controllers/`
4. Add authentication attributes as needed
5. Update Swagger documentation

### Testing
- Use Swagger UI for API testing
- Postman collections available
- Unit tests can be added using xUnit

### Deployment
- Docker support available
- Can be deployed to Azure, AWS, or any .NET hosting platform
- Environment-specific configuration via `appsettings.{Environment}.json`

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## License

This project is licensed under the MIT License.
