<script lang="ts">
	import { onMount } from 'svelte';
	import { goto } from '$app/navigation';
	import { page } from '$app/stores';
	import { authStore } from '$lib/stores/auth';
	import { apiService } from '$lib/api';
	import Navigation from '$lib/components/Navigation.svelte';
	import { 
		Users, 
		Calendar, 
		MapPin, 
		Building, 
		Mail, 
		Bell, 
		Settings, 
		LogOut,
		Home,
		User,
		GraduationCap,
		BarChart3
	} from 'lucide-svelte';

	let user: any = null;
	let sidebarOpen = true;

	onMount(() => {
		// Check if user is authenticated
		if (!apiService.isAuthenticated()) {
			goto('/auth/login');
			return;
		}
		
		user = apiService.getCurrentUserFromStorage();
	});

	function handleLogout() {
		apiService.logout();
		goto('/');
	}

	// Navigation will be loaded dynamically from the database
</script>

<div class="min-h-screen bg-gray-50">
	<!-- Sidebar -->
	<div class="fixed inset-y-0 left-0 z-50 w-64 bg-white shadow-lg transform transition-transform duration-300 ease-in-out {sidebarOpen ? 'translate-x-0' : '-translate-x-full'}">
		<div class="flex items-center justify-between h-16 px-6 border-b border-gray-200">
			<div class="flex items-center">
				<svg class="h-8 w-8 text-primary-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
					<path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 14l9-5-9-5-9 5 9 5z"></path>
					<path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 14l6.16-3.422a12.083 12.083 0 01.665 6.479A11.952 11.952 0 0012 20.055a11.952 11.952 0 00-6.824-2.998 12.078 12.078 0 01.665-6.479L12 14z"></path>
				</svg>
				<span class="ml-2 text-xl font-bold text-gray-900">Alumni Network</span>
			</div>
		</div>

		<!-- User Profile -->
		<div class="px-6 py-4 border-b border-gray-200">
			<div class="flex items-center">
				<div class="w-10 h-10 bg-primary-600 rounded-full flex items-center justify-center">
					<span class="text-white font-medium">
						{user?.firstName?.[0]}{user?.lastName?.[0]}
					</span>
				</div>
				<div class="ml-3">
					<p class="text-sm font-medium text-gray-900">{user?.firstName} {user?.lastName}</p>
					<p class="text-xs text-gray-500">{user?.email}</p>
					<p class="text-xs text-primary-600 capitalize">{user?.roleName}</p>
				</div>
			</div>
		</div>

		<!-- Navigation -->
		<nav class="mt-6 px-3">
			<Navigation type="main" className="space-y-1" />
		</nav>

		<!-- Logout -->
		<div class="absolute bottom-0 left-0 right-0 p-4 border-t border-gray-200">
			<button
				on:click={handleLogout}
				class="w-full flex items-center px-3 py-2 text-sm font-medium text-gray-700 hover:bg-gray-100 rounded-lg transition-colors duration-200"
			>
				<LogOut class="mr-3 h-5 w-5" />
				Sign Out
			</button>
		</div>
	</div>

	<!-- Main Content -->
	<div class="pl-64">
		<!-- Top Navigation -->
		<header class="bg-white shadow-sm border-b border-gray-200">
			<div class="flex items-center justify-between h-16 px-6">
				<div class="flex items-center">
					<button
						on:click={() => sidebarOpen = !sidebarOpen}
						class="p-2 text-gray-400 hover:text-gray-500 lg:hidden"
						aria-label="{sidebarOpen ? 'Close sidebar' : 'Open sidebar'}"
					>
						<svg class="h-6 w-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
							<path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 6h16M4 12h16M4 18h16"></path>
						</svg>
					</button>
					<h1 class="text-xl font-semibold text-gray-900 ml-4">
						{#if $page.url.pathname === '/dashboard'}
							Dashboard
						{:else if $page.url.pathname.startsWith('/dashboard/alumni')}
							Alumni Directory
						{:else if $page.url.pathname.startsWith('/dashboard/events')}
							Events
						{:else if $page.url.pathname.startsWith('/dashboard/profile')}
							My Profile
						{:else if $page.url.pathname.startsWith('/dashboard/analytics')}
							Analytics
						{:else if $page.url.pathname.startsWith('/dashboard/settings')}
							Settings
						{:else}
							Dashboard
						{/if}
					</h1>
				</div>

				<div class="flex items-center space-x-4">
					<button class="p-2 text-gray-400 hover:text-gray-500">
						<Bell class="h-6 w-6" />
					</button>
					<button class="p-2 text-gray-400 hover:text-gray-500">
						<Mail class="h-6 w-6" />
					</button>
				</div>
			</div>
		</header>

		<!-- Page Content -->
		<main class="p-6">
			<slot />
		</main>
	</div>
</div>
