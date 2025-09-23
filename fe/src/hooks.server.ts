import type { Handle } from '@sveltejs/kit';
import { JWT_SECRET } from '$env/dynamic/private';
import jwt from 'jsonwebtoken';

// Define protected routes that require authentication
const PROTECTED_ROUTES = [
  '/dashboard',
  '/admin',
  '/profile',
  '/alumni/new',
  '/events/new'
];

// Define admin-only routes
const ADMIN_ROUTES = [
  '/admin'
];

// Define public routes that don't require authentication
const PUBLIC_ROUTES = [
  '/',
  '/auth/login',
  '/auth/register',
  '/alumni',
  '/events',
  '/about',
  '/contact'
];

export const handle: Handle = async ({ event, resolve }) => {
  const { url, request } = event;
  const pathname = url.pathname;

  // Skip middleware for static assets and API routes
  if (pathname.startsWith('/api/') || pathname.startsWith('/_app/') || pathname.includes('.')) {
    return resolve(event);
  }

  // Get the JWT token from cookies
  const token = event.cookies.get('authToken') || 
                request.headers.get('authorization')?.replace('Bearer ', '');

  let user: {
    id: string;
    email: string;
    firstName: string;
    lastName: string;
    roleId: string;
    roleName: string;
  } | undefined = undefined;
  let isAuthenticated = false;

  // Verify JWT token if present
  if (token) {
    try {
      const decoded = jwt.verify(token, JWT_SECRET) as any;
      user = {
        id: decoded.id,
        email: decoded.email,
        firstName: decoded.firstName,
        lastName: decoded.lastName,
        roleId: decoded.roleId,
        roleName: decoded.roleName
      };
      isAuthenticated = true;
      
      // Add user to event.locals for use in load functions
      event.locals.user = user;
      event.locals.isAuthenticated = true;
    } catch (error) {
      console.log('Invalid JWT token:', error);
      // Clear invalid token
      event.cookies.delete('authToken', { path: '/' });
      user = undefined;
      event.locals.user = undefined;
      event.locals.isAuthenticated = false;
    }
  }

  // Check if route requires authentication
  const requiresAuth = PROTECTED_ROUTES.some(route => pathname.startsWith(route));
  const isAdminRoute = ADMIN_ROUTES.some(route => pathname.startsWith(route));
  const isPublicRoute = PUBLIC_ROUTES.some(route => pathname === route || pathname.startsWith(route));

  // Handle authentication requirements
  if (requiresAuth && !isAuthenticated) {
    // Redirect to login if accessing protected route without authentication
    return new Response(null, {
      status: 302,
      headers: {
        Location: '/auth/login?redirect=' + encodeURIComponent(pathname)
      }
    });
  }

  // Handle admin route access
  if (isAdminRoute && isAuthenticated) {
    const isAdmin = user?.roleName === 'Admin' || user?.roleName === 'SuperAdmin';
    if (!isAdmin) {
      // Redirect non-admin users to dashboard
      return new Response(null, {
        status: 302,
        headers: {
          Location: '/dashboard'
        }
      });
    }
  }

  // Handle authenticated users trying to access auth pages
  if (isAuthenticated && (pathname === '/auth/login' || pathname === '/auth/register')) {
    // Redirect authenticated users to appropriate dashboard
    const isAdmin = user?.roleName === 'Admin' || user?.roleName === 'SuperAdmin';
    const redirectTo = isAdmin ? '/admin' : '/dashboard';
    
    return new Response(null, {
      status: 302,
      headers: {
        Location: redirectTo
      }
    });
  }

  // Add authentication headers for client-side use
  event.locals.user = user;
  event.locals.isAuthenticated = isAuthenticated;

  const response = await resolve(event);

  // Add security headers
  response.headers.set('X-Frame-Options', 'DENY');
  response.headers.set('X-Content-Type-Options', 'nosniff');
  response.headers.set('Referrer-Policy', 'strict-origin-when-cross-origin');
  response.headers.set('Permissions-Policy', 'camera=(), microphone=(), geolocation=()');

  return response;
};
