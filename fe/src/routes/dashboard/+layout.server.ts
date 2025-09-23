import type { LayoutServerLoad } from './$types';
import { redirect } from '@sveltejs/kit';

export const load: LayoutServerLoad = async ({ locals }) => {
  // Check if user is authenticated
  if (!locals.isAuthenticated || !locals.user) {
    throw redirect(302, '/auth/login?redirect=' + encodeURIComponent('/dashboard'));
  }

  return {
    user: locals.user,
    isAuthenticated: locals.isAuthenticated
  };
};
