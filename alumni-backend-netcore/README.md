# Alumni Backend - Clean Architecture

This project has been restructured into a Clean Architecture pattern with separate class library projects.

## Project Structure

### Domain Layer (`Alumni.Domain`)
- **Purpose**: Contains the core business logic and entities
- **Dependencies**: None (no external dependencies)
- **Contains**:
  - Entities (User, Alumni, Event, EventRegistration)
  - Value Objects
  - Domain Interfaces (IRepository, IUserRepository, etc.)
  - Domain Events

### Application Layer (`Alumni.Application`)
- **Purpose**: Contains use cases, CQRS (MediatR), and application logic
- **Dependencies**: Domain layer only
- **Contains**:
  - Use Cases (Commands and Queries using MediatR)
  - DTOs (Data Transfer Objects)
  - Application Interfaces (IJwtService, IPasswordService)
  - Validation (FluentValidation)
  - MediatR Behaviors

### Infrastructure Layer (`Alumni.Infrastructure`)
- **Purpose**: Contains external concerns and implementations
- **Dependencies**: Application and Domain layers
- **Contains**:
  - EF Core DbContext (AppDbContext)
  - Repository implementations
  - Service implementations (JwtService, PasswordService)
  - Identity configuration
  - Database configuration

### WebAPI Layer (`Alumni.WebAPI`)
- **Purpose**: Contains the API controllers and configuration
- **Dependencies**: Application layer (and Infrastructure only through DI)
- **Contains**:
  - Controllers
  - Program.cs configuration
  - Middleware setup
  - Swagger configuration

## Key Features

### MediatR Implementation
- CQRS pattern with Commands and Queries
- Pipeline behaviors for validation
- Clean separation of concerns

### Entity Framework Core
- Clean domain entities with proper encapsulation
- Repository pattern implementation
- PostgreSQL database support

### JWT Authentication
- Secure token-based authentication
- Password hashing with BCrypt
- Role-based authorization

### Validation
- FluentValidation integration
- Automatic validation through MediatR pipeline
- Clean error responses

## Project Dependencies

```
Domain ← Application ← Infrastructure ← WebAPI
```

The dependencies flow inward, ensuring that:
- Domain has no external dependencies
- Application depends only on Domain
- Infrastructure depends on Application and Domain
- WebAPI depends on Application (and Infrastructure through DI)

## Getting Started

1. **Build the solution**:
   ```bash
   dotnet build
   ```

2. **Run the WebAPI project**:
   ```bash
   cd WebAPI
   dotnet run
   ```

3. **Access Swagger UI**:
   - Navigate to `https://localhost:5001/swagger` or `http://localhost:5000/swagger`

## Database Setup

The project uses PostgreSQL. Make sure to:
1. Update the connection string in `appsettings.json`
2. Run Entity Framework migrations when ready

## API Endpoints

### Authentication
- `POST /api/auth/register` - Register a new user
- `POST /api/auth/login` - Login user

### Alumni
- `GET /api/alumni` - Get all public alumni profiles
- `GET /api/alumni/{id}` - Get specific alumni profile
- `POST /api/alumni` - Create alumni profile (authenticated)
- `PUT /api/alumni/{id}` - Update alumni profile (authenticated)

## Architecture Benefits

1. **Separation of Concerns**: Each layer has a specific responsibility
2. **Testability**: Easy to unit test business logic in isolation
3. **Maintainability**: Clear structure makes code easier to maintain
4. **Scalability**: Easy to add new features without affecting existing code
5. **Dependency Inversion**: High-level modules don't depend on low-level modules

## Future Enhancements

- Add more use cases for Events and Event Registrations
- Implement caching layer
- Add logging and monitoring
- Implement event sourcing
- Add unit and integration tests
