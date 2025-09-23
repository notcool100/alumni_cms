import type { LayoutServerLoad } from './$types';
import { redirect } from '@sveltejs/kit';

export const load: LayoutServerLoad = async ({ locals }) => {
  // Check if user is authenticated
  if (!locals.isAuthenticated || !locals.user) {
    throw redirect(302, '/auth/login?redirect=' + encodeURIComponent('/admin'));
  }

  // Check if user has admin role
  const isAdmin = locals.user.roleName === 'Admin' || locals.user.roleName === 'SuperAdmin';
  if (!isAdmin) {
    throw redirect(302, '/dashboard');
  }

  return {
    user: locals.user,
    isAuthenticated: locals.isAuthenticated,
    isAdmin: true
  };
};
