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

### Role-Based Access Control (RBAC)
- Comprehensive role management system
- Granular permissions for different operations
- Role-based navigation system
- Pre-configured roles: SuperAdmin, Admin, Staff, Alumni, Moderator
- 25+ predefined permissions covering all major system functions

### Navigation System
- Hierarchical navigation structure
- Role-based navigation access
- Organized navigation groups (Main, Administration, Content, Analytics, Settings)
- 19+ predefined navigation items

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

1. **Clone and build the solution**:
   ```bash
   git clone <repository-url>
   cd alumni-backend-netcore
   dotnet build
   ```

2. **Set up the database**:
   ```bash
   # Install Entity Framework Tools (if not already installed)
   dotnet tool install --global dotnet-ef
   
   # Update connection string in WebAPI/appsettings.json
   # Then run migrations
   dotnet ef database update --project Infrastructure --startup-project WebAPI
   ```

3. **Run the WebAPI project**:
   ```bash
   cd WebAPI
   dotnet run
   ```

4. **Access Swagger UI**:
   - Navigate to `https://localhost:5001/swagger` or `http://localhost:5000/swagger`

5. **Test the API**:
   - Use the sample users from the seed data:
     - `admin@alumni.com` (SuperAdmin)
     - `staff@alumni.com` (Staff)
     - `alumni@example.com` (Alumni)

## Database Setup

The project uses PostgreSQL with Entity Framework Core. Follow these steps to set up the database:

### Prerequisites
1. **PostgreSQL**: Make sure PostgreSQL is installed and running
2. **Connection String**: Update the connection string in `WebAPI/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Port=5432;Database=alumni_db;User Id=postgres;Password=your_password;"
     }
   }
   ```

### Database Migration and Seeding

1. **Install Entity Framework Tools** (if not already installed):
   ```bash
   dotnet tool install --global dotnet-ef
   ```

2. **Create and apply migrations**:
   ```bash
   # Create initial migration (if no migrations exist)
   dotnet ef migrations add InitialCreate --project Infrastructure --startup-project WebAPI
   
   # Apply migrations to create/update database
   dotnet ef database update --project Infrastructure --startup-project WebAPI
   ```

3. **Add seed data migration** (if needed):
   ```bash
   # Create migration for seed data
   dotnet ef migrations add SeedData --project Infrastructure --startup-project WebAPI
   
   # Apply seed data
   dotnet ef database update --project Infrastructure --startup-project WebAPI
   ```

### Database Schema

The database includes the following tables with seed data:

#### Core Tables
- **users** - User accounts with role-based access
- **alumni** - Alumni profiles linked to users
- **events** - Event management
- **event_registrations** - Event attendance tracking

#### Role-Based Access Control
- **roles** - User roles (SuperAdmin, Admin, Staff, Alumni, Moderator)
- **permissions** - System permissions (25 predefined permissions)
- **role_permissions** - Role-permission relationships
- **approval_levels** - Approval workflow management

#### Navigation System
- **navigation_groups** - Navigation organization (Main, Administration, Content, Analytics, Settings)
- **navigation_items** - Individual navigation items (19 predefined items)
- **role_navigation** - Role-based navigation access

### Seed Data

The system comes pre-configured with:

#### Roles
- **SuperAdmin**: Full system access
- **Admin**: Management access (except system settings)
- **Staff**: Limited administrative access
- **Alumni**: Basic access for alumni members
- **Moderator**: Content and approval rights

#### Permissions
- User Management (view, create, edit, delete)
- Alumni Management (view, create, edit, delete, approve)
- Event Management (view, create, edit, delete, approve)
- Content Management (view, create, edit, delete, publish)
- Analytics (view, export)
- Role Management (view, create, edit, delete)
- System Settings (view, edit)

#### Navigation Items
- **Main**: Dashboard, Alumni, Events, Profile
- **Administration**: User Management, Role Management, Alumni Management, Event Management, Approvals
- **Content**: Content Management, News & Updates, Resources
- **Analytics**: Dashboard Analytics, Alumni Reports, Event Reports, Engagement Metrics
- **Settings**: System Settings, Email Templates, Navigation

#### Sample Users
- `admin@alumni.com` - SuperAdmin role
- `staff@alumni.com` - Staff role
- `alumni@example.com` - Alumni role

### Migration Commands Reference

```bash
# List all migrations
dotnet ef migrations list --project Infrastructure --startup-project WebAPI

# Remove last migration
dotnet ef migrations remove --project Infrastructure --startup-project WebAPI

# Drop database (use with caution)
dotnet ef database drop --project Infrastructure --startup-project WebAPI --force

# Generate SQL script (without applying)
dotnet ef migrations script --project Infrastructure --startup-project WebAPI

# Update to specific migration
dotnet ef database update MigrationName --project Infrastructure --startup-project WebAPI
```

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
