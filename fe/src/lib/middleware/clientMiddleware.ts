import { goto } from '$app/navigation';
import { page } from '$app/stores';
import { get } from 'svelte/store';
import { apiService } from '$lib/api';
import type { User } from '$lib/api';

export interface MiddlewareOptions {
  requireAuth?: boolean;
  requireRole?: string | string[];
  redirectTo?: string;
  allowUnauthenticated?: boolean;
}

export class ClientMiddleware {
  private static instance: ClientMiddleware;
  private currentUser: User | null = null;
  private isInitialized = false;

  private constructor() {}

  static getInstance(): ClientMiddleware {
    if (!ClientMiddleware.instance) {
      ClientMiddleware.instance = new ClientMiddleware();
    }
    return ClientMiddleware.instance;
  }

  async initialize(): Promise<void> {
    if (this.isInitialized) return;

    // Initialize from localStorage
    this.currentUser = apiService.getCurrentUserFromStorage();
    this.isInitialized = true;
  }

  async checkAuth(options: MiddlewareOptions = {}): Promise<boolean> {
    await this.initialize();

    const {
      requireAuth = true,
      requireRole,
      redirectTo = '/auth/login',
      allowUnauthenticated = false
    } = options;

    // Check if user is authenticated
    const isAuthenticated = apiService.isAuthenticated();
    
    if (!isAuthenticated) {
      if (requireAuth && !allowUnauthenticated) {
        // Store current path for redirect after login
        const currentPath = get(page).url.pathname;
        if (currentPath !== '/auth/login') {
          goto(`/auth/login?redirect=${encodeURIComponent(currentPath)}`);
        }
        return false;
      }
      return true; // Allow unauthenticated access
    }

    // Update current user
    this.currentUser = apiService.getCurrentUserFromStorage();

    // Check role requirements
    if (requireRole && this.currentUser) {
      const requiredRoles = Array.isArray(requireRole) ? requireRole : [requireRole];
      const hasRequiredRole = requiredRoles.includes(this.currentUser.roleName);
      
      if (!hasRequiredRole) {
        // Redirect based on user's actual role
        const redirectPath = this.getRedirectPathForRole(this.currentUser.roleName);
        goto(redirectPath);
        return false;
      }
    }

    return true;
  }

  private getRedirectPathForRole(roleName: string): string {
    switch (roleName) {
      case 'Admin':
      case 'SuperAdmin':
        return '/admin';
      case 'Alumni':
      case 'User':
      default:
        return '/dashboard';
    }
  }

  // Middleware for admin routes
  async requireAdmin(): Promise<boolean> {
    return this.checkAuth({
      requireAuth: true,
      requireRole: ['Admin', 'SuperAdmin'],
      redirectTo: '/dashboard'
    });
  }

  // Middleware for authenticated users
  async requireAuth(): Promise<boolean> {
    return this.checkAuth({
      requireAuth: true,
      redirectTo: '/auth/login'
    });
  }

  // Middleware for public routes (redirect authenticated users)
  async requireGuest(): Promise<boolean> {
    const isAuthenticated = apiService.isAuthenticated();
    
    if (isAuthenticated) {
      const user = apiService.getCurrentUserFromStorage();
      const redirectPath = this.getRedirectPathForRole(user?.roleName || 'User');
      goto(redirectPath);
      return false;
    }
    
    return true;
  }

  // Get current user
  getCurrentUser(): User | null {
    return this.currentUser;
  }

  // Check if user has specific role
  hasRole(role: string | string[]): boolean {
    if (!this.currentUser) return false;
    
    const roles = Array.isArray(role) ? role : [role];
    return roles.includes(this.currentUser.roleName);
  }

  // Check if user is admin
  isAdmin(): boolean {
    return this.hasRole(['Admin', 'SuperAdmin']);
  }

  // Check if user is authenticated
  isAuthenticated(): boolean {
    return apiService.isAuthenticated();
  }
}

// Export singleton instance
export const clientMiddleware = ClientMiddleware.getInstance();

// Convenience functions
export const requireAuth = () => clientMiddleware.requireAuth();
export const requireAdmin = () => clientMiddleware.requireAdmin();
export const requireGuest = () => clientMiddleware.requireGuest();
export const checkAuth = (options: MiddlewareOptions) => clientMiddleware.checkAuth(options);
