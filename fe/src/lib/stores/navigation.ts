import { writable } from 'svelte/store';
import { apiService, type UserNavigation, type NavigationItem, type NavigationGroup } from '$lib/api';

interface NavigationState {
  navigation: UserNavigation | null;
  loading: boolean;
  error: string | null;
}

function createNavigationStore() {
  const { subscribe, set, update } = writable<NavigationState>({
    navigation: null,
    loading: false,
    error: null
  });

  return {
    subscribe,
    
    async loadUserNavigation() {
      update(state => ({ ...state, loading: true, error: null }));
      
      try {
        const response = await apiService.getUserNavigation();
        if (response.success && response.data) {
          update(state => ({ 
            ...state, 
            navigation: response.data, 
            loading: false 
          }));
        } else {
          update(state => ({ 
            ...state, 
            error: response.message || 'Failed to load navigation', 
            loading: false 
          }));
        }
      } catch (error) {
        console.error('Navigation loading error:', error);
        update(state => ({ 
          ...state, 
          error: error instanceof Error ? error.message : 'Failed to load navigation', 
          loading: false 
        }));
      }
    },

    async loadNavigationByRole(roleId: string) {
      update(state => ({ ...state, loading: true, error: null }));
      
      try {
        const response = await apiService.getNavigationByRole(roleId);
        if (response.success && response.data) {
          update(state => ({ 
            ...state, 
            navigation: response.data, 
            loading: false 
          }));
        } else {
          update(state => ({ 
            ...state, 
            error: response.message || 'Failed to load navigation', 
            loading: false 
          }));
        }
      } catch (error) {
        update(state => ({ 
          ...state, 
          error: error instanceof Error ? error.message : 'Failed to load navigation', 
          loading: false 
        }));
      }
    },

    clear() {
      set({ navigation: null, loading: false, error: null });
    },

    // Helper methods
    getNavigationItems(): NavigationItem[] {
      let state: NavigationState;
      const unsubscribe = subscribe(s => { state = s; });
      unsubscribe();
      return state?.navigation?.flatItems || [];
    },

    getNavigationGroups(): NavigationGroup[] {
      let state: NavigationState;
      const unsubscribe = subscribe(s => { state = s; });
      unsubscribe();
      return state?.navigation?.groups || [];
    },

    getMainNavigation(): NavigationItem[] {
      let state: NavigationState;
      const unsubscribe = subscribe(s => { state = s; });
      unsubscribe();
      return state?.navigation?.groups
        .find(g => g.name.toLowerCase().includes('main'))
        ?.navigationItems || [];
    },

    getAdminNavigation(): NavigationItem[] {
      let state: NavigationState;
      const unsubscribe = subscribe(s => { state = s; });
      unsubscribe();
      return state?.navigation?.groups
        .filter(g => ['administration', 'content', 'analytics', 'settings'].some(name => 
          g.name.toLowerCase().includes(name)))
        .flatMap(g => g.navigationItems) || [];
    }
  };
}

export const navigationStore = createNavigationStore();
