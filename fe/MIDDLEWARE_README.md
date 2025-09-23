# Frontend Middleware System

This document explains the comprehensive middleware system implemented in the Alumni Network frontend for perfect authentication and authorization handling.

## Overview

The middleware system provides multiple layers of protection:

1. **Server-Side Hooks** (`hooks.server.ts`) - Primary security layer
2. **Route Load Functions** - Server-side route protection
3. **Client-Side Middleware** - Additional client-side protection
4. **Route Guards** - Component-level protection

## Architecture

### 1. Server-Side Hooks (`src/hooks.server.ts`)

The primary middleware layer that runs on every request:

```typescript
export const handle: Handle = async ({ event, resolve }) => {
  // JWT token verification
  // Route protection
  // Role-based access control
  // Security headers
}
```

#### Features:
- **JWT Token Verification**: Validates tokens from cookies or headers
- **Route Protection**: Automatically redirects unauthenticated users
- **Role-Based Access**: Enforces admin-only route access
- **Security Headers**: Adds security headers to all responses
- **Cookie Management**: Handles auth tokens in secure cookies

#### Protected Routes:
```typescript
const PROTECTED_ROUTES = [
  '/dashboard',
  '/admin',
  '/profile',
  '/alumni/new',
  '/events/new'
];

const ADMIN_ROUTES = [
  '/admin'
];
```

### 2. Route Load Functions

Server-side load functions for specific route groups:

#### Main Layout (`src/routes/+layout.server.ts`)
```typescript
export const load: LayoutServerLoad = async ({ locals }) => {
  return {
    user: locals.user,
    isAuthenticated: locals.isAuthenticated
  };
};
```

#### Admin Layout (`src/routes/admin/+layout.server.ts`)
```typescript
export const load: LayoutServerLoad = async ({ locals }) => {
  // Check authentication
  if (!locals.isAuthenticated || !locals.user) {
    throw redirect(302, '/auth/login?redirect=' + encodeURIComponent('/admin'));
  }

  // Check admin role
  const isAdmin = locals.user.roleName === 'Admin' || locals.user.roleName === 'SuperAdmin';
  if (!isAdmin) {
    throw redirect(302, '/dashboard');
  }

  return { user: locals.user, isAuthenticated: true, isAdmin: true };
};
```

#### Dashboard Layout (`src/routes/dashboard/+layout.server.ts`)
```typescript
export const load: LayoutServerLoad = async ({ locals }) => {
  if (!locals.isAuthenticated || !locals.user) {
    throw redirect(302, '/auth/login?redirect=' + encodeURIComponent('/dashboard'));
  }
  return { user: locals.user, isAuthenticated: true };
};
```

#### Auth Layout (`src/routes/auth/+layout.server.ts`)
```typescript
export const load: LayoutServerLoad = async ({ locals }) => {
  // Redirect authenticated users away from auth pages
  if (locals.isAuthenticated && locals.user) {
    const isAdmin = locals.user.roleName === 'Admin' || locals.user.roleName === 'SuperAdmin';
    const redirectTo = isAdmin ? '/admin' : '/dashboard';
    throw redirect(302, redirectTo);
  }
  return { user: null, isAuthenticated: false };
};
```

### 3. Client-Side Middleware (`src/lib/middleware/clientMiddleware.ts`)

Additional protection layer for client-side operations:

```typescript
export class ClientMiddleware {
  async checkAuth(options: MiddlewareOptions): Promise<boolean>
  async requireAdmin(): Promise<boolean>
  async requireAuth(): Promise<boolean>
  async requireGuest(): Promise<boolean>
}
```

#### Usage Examples:

```typescript
// In component onMount
import { clientMiddleware } from '$lib/middleware/clientMiddleware';

onMount(async () => {
  const hasAccess = await clientMiddleware.requireAdmin();
  if (!hasAccess) return;
  // Continue with component logic
});
```

### 4. Route Guards (`src/lib/utils/routeGuards.ts`)

Component-level protection utilities:

```typescript
export const requireAuth = createRouteGuard({ requireAuth: true });
export const requireAdmin = createRouteGuard({ 
  requireAuth: true, 
  requiredRoles: ['Admin', 'SuperAdmin'] 
});
```

## Security Features

### 1. JWT Token Management
- **Secure Cookies**: Tokens stored in HTTP-only cookies
- **Automatic Cleanup**: Invalid tokens are automatically removed
- **Cross-Site Protection**: SameSite=Strict for CSRF protection

### 2. Role-Based Access Control
- **Server-Side Validation**: Primary role checks on server
- **Client-Side Validation**: Additional checks on client
- **Automatic Redirects**: Users redirected to appropriate dashboards

### 3. Security Headers
```typescript
response.headers.set('X-Frame-Options', 'DENY');
response.headers.set('X-Content-Type-Options', 'nosniff');
response.headers.set('Referrer-Policy', 'strict-origin-when-cross-origin');
response.headers.set('Permissions-Policy', 'camera=(), microphone=(), geolocation=()');
```

### 4. Redirect Handling
- **Preserve Intent**: Original URL preserved in redirect parameters
- **Role-Based Routing**: Users redirected to appropriate dashboards
- **Prevent Loops**: Auth pages redirect authenticated users

## Implementation Flow

### 1. User Access Flow
```
User Request → Server Hooks → Route Load → Component Mount → Client Middleware
     ↓              ↓              ↓              ↓              ↓
Token Check → Auth Validation → Role Check → Route Guard → Final Access
```

### 2. Authentication Flow
```
Login → Token Generation → Cookie Set → Server Validation → Route Access
  ↓           ↓              ↓              ↓              ↓
API Call → JWT Create → Secure Cookie → Hook Verify → Load Function
```

### 3. Authorization Flow
```
Route Access → Role Check → Permission Validation → Component Render
     ↓            ↓              ↓                    ↓
Load Function → Role Match → Access Granted → UI Display
```

## Best Practices

### 1. Security First
- Always validate on server-side first
- Use client-side checks as UX enhancement only
- Implement proper error handling

### 2. Performance
- Minimize middleware overhead
- Cache authentication state appropriately
- Use efficient token validation

### 3. User Experience
- Preserve user intent with redirect parameters
- Provide clear error messages
- Implement smooth transitions

### 4. Maintainability
- Keep middleware logic centralized
- Use consistent patterns across routes
- Document all security decisions

## Configuration

### Environment Variables
```bash
# Required for JWT verification
JWT_SECRET=your-secret-key-here
```

### Route Configuration
```typescript
// Add new protected routes
const PROTECTED_ROUTES = [
  '/dashboard',
  '/admin',
  '/profile',
  '/alumni/new',
  '/events/new',
  '/your-new-route'  // Add here
];

// Add new admin routes
const ADMIN_ROUTES = [
  '/admin',
  '/your-admin-route'  // Add here
];
```

## Testing

### Manual Testing
1. **Unauthenticated Access**: Try accessing protected routes without login
2. **Wrong Role Access**: Try accessing admin routes with regular user
3. **Authenticated Redirect**: Login and try accessing auth pages
4. **Token Expiry**: Test with expired tokens

### Automated Testing
```typescript
// Test middleware functions
import { clientMiddleware } from '$lib/middleware/clientMiddleware';

test('admin middleware', async () => {
  const result = await clientMiddleware.requireAdmin();
  expect(result).toBe(true);
});
```

## Troubleshooting

### Common Issues

1. **Token Not Found**: Check cookie settings and JWT_SECRET
2. **Redirect Loops**: Verify route configurations
3. **Role Mismatch**: Ensure role names match between frontend and backend
4. **CORS Issues**: Check API endpoint configurations

### Debug Mode
```typescript
// Enable debug logging in hooks.server.ts
console.log('Auth check:', { pathname, isAuthenticated, user });
```

## Future Enhancements

1. **Rate Limiting**: Add request rate limiting
2. **Session Management**: Implement session timeout handling
3. **Multi-Factor Auth**: Add 2FA support
4. **Audit Logging**: Track authentication events
5. **Permission Granularity**: More fine-grained permissions

## Conclusion

This middleware system provides comprehensive protection at multiple levels:

- **Server-side security** with JWT validation and route protection
- **Client-side UX** with smooth redirects and error handling
- **Role-based access** with automatic routing
- **Security headers** for additional protection

The system ensures that users can only access appropriate resources while maintaining a smooth user experience.
