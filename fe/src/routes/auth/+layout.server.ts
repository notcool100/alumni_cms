import type { LayoutServerLoad } from './$types';
import { redirect } from '@sveltejs/kit';

export const load: LayoutServerLoad = async ({ locals, url }) => {
  // If user is already authenticated, redirect to appropriate dashboard
  if (locals.isAuthenticated && locals.user) {
    const isAdmin = locals.user.roleName === 'Admin' || locals.user.roleName === 'SuperAdmin';
    const redirectTo = isAdmin ? '/admin' : '/dashboard';
    throw redirect(302, redirectTo);
  }

  return {
    user: null,
    isAuthenticated: false
  };
};
