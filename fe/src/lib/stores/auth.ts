import { writable } from 'svelte/store';
import { apiService, type User } from '$lib/api';

interface AuthState {
  user: User | null;
  isAuthenticated: boolean;
  loading: boolean;
}

function createAuthStore() {
  const { subscribe, set, update } = writable<AuthState>({
    user: null,
    isAuthenticated: false,
    loading: true,
  });

  return {
    subscribe,
    
    // Initialize auth state from localStorage
    init: () => {
      update(state => ({
        ...state,
        isAuthenticated: apiService.isAuthenticated(),
        user: apiService.getCurrentUserFromStorage(),
        loading: false,
      }));
    },

    // Set user data
    setUser: (user: User) => {
      update(state => ({
        ...state,
        user,
        isAuthenticated: true,
        loading: false,
      }));
    },

    // Clear auth state
    logout: () => {
      apiService.logout();
      set({
        user: null,
        isAuthenticated: false,
        loading: false,
      });
    },

    // Set loading state
    setLoading: (loading: boolean) => {
      update(state => ({ ...state, loading }));
    },
  };
}

export const authStore = createAuthStore();
