import { goto } from '$app/navigation';
import { apiService } from '$lib/api';

export interface RouteGuardOptions {
  requireAuth?: boolean;
  requiredRoles?: string[];
  redirectTo?: string;
}

export function createRouteGuard(options: RouteGuardOptions = {}) {
  const {
    requireAuth = true,
    requiredRoles = [],
    redirectTo = '/auth/login'
  } = options;

  return function guard() {
    // Check authentication
    if (requireAuth && !apiService.isAuthenticated()) {
      goto(redirectTo);
      return false;
    }

    // Check roles if specified
    if (requiredRoles.length > 0) {
      const user = apiService.getCurrentUserFromStorage();
      if (!user || !requiredRoles.includes(user.roleName)) {
        goto('/dashboard');
        return false;
      }
    }

    return true;
  };
}

// Predefined guards for common use cases
export const requireAuth = createRouteGuard({ requireAuth: true });
export const requireAdmin = createRouteGuard({ 
  requireAuth: true, 
  requiredRoles: ['Admin', 'SuperAdmin'],
  redirectTo: '/dashboard'
});

// Dynamic role-based guard that relies on navigation permissions
export function requireNavigationAccess() {
  if (!apiService.isAuthenticated()) {
    goto('/auth/login');
    return false;
  }
  
  const user = apiService.getCurrentUserFromStorage();
  if (!user) {
    goto('/auth/login');
    return false;
  }
  
  // The navigation system will handle access control
  // If user can navigate to this route, they have permission
  return true;
}
