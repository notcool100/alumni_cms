# Frontend-Backend Connection Setup

This guide explains how to connect the Svelte frontend with the .NET backend.

## Overview

- **Frontend**: Svelte application running on `http://localhost:5173`
- **Backend**: .NET Web API running on `http://localhost:5037`
- **Database**: PostgreSQL (configured in backend)

## Quick Start

### Option 1: Use the automated script
```bash
./start-dev.sh
```

This script will:
- Check if required ports are available
- Start the .NET backend
- Start the Svelte frontend
- Provide URLs for both services

### Option 2: Manual startup

#### 1. Start the Backend
```bash
cd alumni-backend-netcore/WebAPI
dotnet run
```

The backend will be available at:
- API: http://localhost:5037
- Swagger UI: http://localhost:5037/swagger

#### 2. Start the Frontend
```bash
cd fe
npm run dev
```

The frontend will be available at:
- http://localhost:5173

## API Configuration

The frontend is configured to connect to the backend via the `api.ts` file located at `fe/src/lib/api.ts`.

### Key Configuration Points:

1. **Base URL**: Set to `http://localhost:5037` (matches backend port)
2. **CORS**: Backend is configured to allow all origins in development
3. **Authentication**: JWT-based authentication with Bearer tokens
4. **Data Structures**: Updated to match .NET backend DTOs

### Available Endpoints:

#### Authentication
- `POST /api/auth/register` - User registration
- `POST /api/auth/login` - User login
- `GET /api/auth/me` - Get current user (requires authentication)

#### Alumni Management
- `GET /api/alumni` - Get all alumni
- `GET /api/alumni/{id}` - Get specific alumni
- `POST /api/alumni` - Create alumni (requires authentication)
- `PUT /api/alumni/{id}` - Update alumni (requires authentication)

#### Health Check
- `GET /health` - Backend health status

## Data Structure Mapping

The frontend interfaces have been updated to match the .NET backend DTOs:

### User Interface
```typescript
export interface User {
  id: string;
  email: string;
  firstName: string;  // Changed from first_name
  lastName: string;   // Changed from last_name
  roleId: string;     // Added
  roleName: string;   // Added
}
```

### Alumni Interface
```typescript
export interface Alumni {
  id: string;
  userId: string;           // Changed from user_id
  user: User;               // Added nested user object
  graduationYear: number;   // Changed from graduation_year
  degree: string;
  major: string;
  currentCompany?: string;  // Changed from current_company
  currentPosition?: string; // Changed from current_position
  location?: string;
  bio?: string;
  linkedinUrl?: string;     // Changed from linkedin_url
  githubUrl?: string;       // Changed from github_url
  websiteUrl?: string;      // Changed from website_url
  profileImageUrl?: string; // Changed from profile_image_url
  isPublic: boolean;        // Changed from is_public
  createdAt: string;        // Changed from created_at
  updatedAt: string;        // Changed from updated_at
}
```

## Testing the Connection

### 1. Health Check
Visit http://localhost:5037/health to verify the backend is running.

### 2. Swagger Documentation
Visit http://localhost:5037/swagger to see the API documentation and test endpoints.

### 3. Frontend Integration
The frontend will automatically attempt to connect to the backend when:
- User logs in/registers
- Alumni data is fetched
- Events are managed

## Troubleshooting

### Common Issues:

1. **CORS Errors**: Ensure the backend is running and CORS is properly configured
2. **Port Conflicts**: Make sure ports 5037 and 5173 are available
3. **Database Connection**: Ensure PostgreSQL is running and connection string is correct
4. **Authentication**: Check JWT configuration in `appsettings.json`

### Debug Steps:

1. Check backend logs for errors
2. Verify database connection
3. Test API endpoints via Swagger
4. Check browser console for frontend errors
5. Verify CORS configuration

## Development Workflow

1. Start the backend first (it needs to be running for the frontend to work)
2. Start the frontend
3. Make API changes in the backend
4. Update frontend interfaces if DTOs change
5. Test the integration

## Production Considerations

For production deployment:
1. Update CORS policy to specific origins
2. Use HTTPS
3. Configure proper JWT secrets
4. Set up proper database connection strings
5. Configure environment-specific settings
