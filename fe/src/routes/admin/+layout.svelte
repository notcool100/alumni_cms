<script lang="ts">
	import { onMount } from 'svelte';
	import { goto } from '$app/navigation';
	import { apiService } from '$lib/api';
	import { 
		Users, 
		Calendar, 
		FileText, 
		Settings, 
		BarChart3, 
		Bell, 
		LogOut,
		Menu,
		X,
		Home,
		Plus,
		Download,
		Image,
		Shield,
		Mail,
		User
	} from 'lucide-svelte';
	
	let user: any = null;
	let sidebarOpen = false;
	let currentPath = '';
	
	onMount(() => {
		// Check if user is authenticated and is admin
		if (!apiService.isAuthenticated()) {
			goto('/auth/login');
			return;
		}
		
		user = apiService.getCurrentUserFromStorage();
		console.log(user," this is user");
		if (user?.role !== 'Admin') {
			goto('/dashboard');
			return;
		}
		
		currentPath = window.location.pathname;
	});
	
	function handleLogout() {
		apiService.logout();
		goto('/');
	}
	
	function toggleSidebar() {
		sidebarOpen = !sidebarOpen;
	}
	
	const navigation = [
		{ name: 'Dashboard', href: '/admin', icon: Home },
		{ name: 'Alumni Management', href: '/admin/alumni', icon: Users },
		{ name: 'Event Management', href: '/admin/events', icon: Calendar },
		{ name: 'Content Management', href: '/admin/content', icon: FileText },
		{ name: 'User Management', href: '/admin/users', icon: Settings },
		{ name: 'Analytics', href: '/admin/analytics', icon: BarChart3 },
	];

	const subNavigation = {
		alumni: [
			{ name: 'All Alumni', href: '/admin/alumni', icon: Users },
			{ name: 'Add New Alumni', href: '/admin/alumni/new', icon: Plus },
			{ name: 'Import Alumni', href: '/admin/alumni/import', icon: Download },
		],
		events: [
			{ name: 'All Events', href: '/admin/events', icon: Calendar },
			{ name: 'Create Event', href: '/admin/events/new', icon: Plus },
			{ name: 'Event Calendar', href: '/admin/events/calendar', icon: Calendar },
		],
		content: [
			{ name: 'All Content', href: '/admin/content', icon: FileText },
			{ name: 'Create Content', href: '/admin/content/new', icon: Plus },
			{ name: 'Media Library', href: '/admin/content/media', icon: Image },
		],
		users: [
			{ name: 'All Users', href: '/admin/users', icon: Users },
			{ name: 'Add User', href: '/admin/users/new', icon: Plus },
			{ name: 'Roles & Permissions', href: '/admin/users/roles', icon: Shield },
		],
	};
</script>

<svelte:head>
	<title>Admin Dashboard - Alumni Network CMS</title>
</svelte:head>

<div class="min-h-screen bg-gray-50">
	<!-- Mobile sidebar overlay -->
	{#if sidebarOpen}
		<div class="fixed inset-0 z-40 lg:hidden" on:click={toggleSidebar}>
			<div class="fixed inset-0 bg-gray-600 bg-opacity-75"></div>
		</div>
	{/if}
	
	<!-- Sidebar -->
	<div class="fixed inset-y-0 left-0 z-50 w-64 bg-white shadow-lg transform transition-transform duration-300 ease-in-out lg:translate-x-0 lg:static lg:inset-0 {sidebarOpen ? 'translate-x-0' : '-translate-x-full'}">
		<div class="flex items-center justify-between h-16 px-6 border-b border-gray-200">
			<div class="flex items-center">
				<svg class="h-8 w-8 text-primary-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
					<path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 14l9-5-9-5-9 5 9 5z"></path>
					<path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 14l6.16-3.422a12.083 12.083 0 01.665 6.479A11.952 11.952 0 0012 20.055a11.952 11.952 0 00-6.824-2.998 12.078 12.078 0 01.665-6.479L12 14z"></path>
				</svg>
				<span class="ml-2 text-xl font-bold text-gray-900">Admin CMS</span>
			</div>
			<button on:click={toggleSidebar} class="lg:hidden">
				<X class="h-6 w-6 text-gray-400" />
			</button>
		</div>
		
		<!-- Quick Actions -->
		<div class="px-6 py-4">
			<h3 class="text-xs font-semibold text-gray-500 uppercase tracking-wider mb-3">Quick Actions</h3>
			<div class="space-y-2">
				<a href="/admin/alumni/new" class="flex items-center px-3 py-2 text-sm font-medium text-primary-700 bg-primary-50 rounded-lg hover:bg-primary-100 transition-colors duration-200">
					<Plus class="h-4 w-4 mr-2" />
					Add Alumni
				</a>
				<a href="/admin/events/new" class="flex items-center px-3 py-2 text-sm font-medium text-green-700 bg-green-50 rounded-lg hover:bg-green-100 transition-colors duration-200">
					<Plus class="h-4 w-4 mr-2" />
					Create Event
				</a>
				<a href="/admin/content/new" class="flex items-center px-3 py-2 text-sm font-medium text-blue-700 bg-blue-50 rounded-lg hover:bg-blue-100 transition-colors duration-200">
					<Plus class="h-4 w-4 mr-2" />
					Add Content
				</a>
			</div>
		</div>

		<nav class="px-6">
			<h3 class="text-xs font-semibold text-gray-500 uppercase tracking-wider mb-3">Navigation</h3>
			<div class="space-y-2">
				{#each navigation as item}
					{@const isActive = currentPath === item.href || currentPath.startsWith(item.href + '/')}
					{@const hasSubNav = item.name === 'Alumni Management' ? subNavigation.alumni : 
						item.name === 'Event Management' ? subNavigation.events :
						item.name === 'Content Management' ? subNavigation.content :
						item.name === 'User Management' ? subNavigation.users : null}
					
					<div class="space-y-1">
						<a 
							href={item.href}
							class="flex items-center justify-between px-3 py-2 text-sm font-medium rounded-lg transition-colors duration-200 {isActive ? 'bg-primary-100 text-primary-700' : 'text-gray-700 hover:bg-gray-100'}"
						>
							<div class="flex items-center">
								<svelte:component this={item.icon} class="h-5 w-5 mr-3" />
								{item.name}
							</div>
							{#if hasSubNav}
								<svg class="h-4 w-4 transform transition-transform {isActive ? 'rotate-90' : ''}" fill="none" stroke="currentColor" viewBox="0 0 24 24">
									<path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 5l7 7-7 7"></path>
								</svg>
							{/if}
						</a>
						
						{#if hasSubNav && isActive}
							<div class="ml-6 space-y-1">
								{#each hasSubNav as subItem}
									<a 
										href={subItem.href}
										class="flex items-center px-3 py-2 text-sm rounded-lg transition-colors duration-200 {currentPath === subItem.href ? 'bg-primary-50 text-primary-600' : 'text-gray-600 hover:bg-gray-50'}"
									>
										<svelte:component this={subItem.icon} class="h-4 w-4 mr-3" />
										{subItem.name}
									</a>
								{/each}
							</div>
						{/if}
					</div>
				{/each}
			</div>
		</nav>
		
		<!-- Settings Section -->
		<div class="mt-8 px-6">
			<h3 class="text-xs font-semibold text-gray-500 uppercase tracking-wider mb-3">Settings</h3>
			<div class="space-y-1">
				<a href="/admin/settings" class="flex items-center px-3 py-2 text-sm font-medium text-gray-700 rounded-lg hover:bg-gray-100 transition-colors duration-200">
					<Settings class="h-5 w-5 mr-3" />
					Site Settings
				</a>
				<a href="/admin/settings/email" class="flex items-center px-3 py-2 text-sm font-medium text-gray-700 rounded-lg hover:bg-gray-100 transition-colors duration-200">
					<Mail class="h-5 w-5 mr-3" />
					Email Templates
				</a>
			</div>
		</div>

		<!-- User Profile Section -->
		<div class="absolute bottom-0 left-0 right-0 p-6 border-t border-gray-200 bg-white">
			<div class="flex items-center space-x-3 mb-4">
				<div class="w-10 h-10 bg-primary-600 rounded-full flex items-center justify-center">
					<span class="text-white font-medium">
						{user?.first_name?.[0]}{user?.last_name?.[0]}
					</span>
				</div>
				<div class="flex-1 min-w-0">
					<p class="text-sm font-medium text-gray-900 truncate">{user?.first_name} {user?.last_name}</p>
					<p class="text-xs text-gray-500 truncate">{user?.email}</p>
					<p class="text-xs text-primary-600 font-medium">Administrator</p>
				</div>
			</div>
			<div class="flex items-center space-x-2">
				<a 
					href="/admin/profile"
					class="flex items-center flex-1 px-3 py-2 text-sm font-medium text-gray-700 rounded-lg hover:bg-gray-100 transition-colors duration-200"
				>
					<User class="h-4 w-4 mr-2" />
					Profile
				</a>
				<button 
					on:click={handleLogout}
					class="flex items-center px-3 py-2 text-sm font-medium text-gray-700 rounded-lg hover:bg-gray-100 transition-colors duration-200"
				>
					<LogOut class="h-4 w-4" />
				</button>
			</div>
		</div>
	</div>
	
	<!-- Main content -->
	<div class="lg:pl-64">
		<!-- Top navigation -->
		<header class="bg-white shadow-sm border-b border-gray-200">
			<div class="flex items-center justify-between h-16 px-4 sm:px-6 lg:px-8">
				<div class="flex items-center space-x-4">
					<button on:click={toggleSidebar} class="lg:hidden">
						<Menu class="h-6 w-6 text-gray-400" />
					</button>
					
					<!-- Search -->
					<div class="hidden md:block">
						<div class="relative">
							<input
								type="text"
								placeholder="Search alumni, events, content..."
								class="w-80 pl-10 pr-4 py-2 text-sm border border-gray-300 rounded-lg focus:ring-2 focus:ring-primary-500 focus:border-primary-500"
							/>
							<svg class="absolute left-3 top-1/2 transform -translate-y-1/2 h-4 w-4 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
								<path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z"></path>
							</svg>
						</div>
					</div>
				</div>
				
				<div class="flex items-center space-x-4">
					<!-- Notifications -->
					<div class="relative">
						<button class="p-2 text-gray-400 hover:text-gray-500 relative">
							<Bell class="h-6 w-6" />
							<span class="absolute top-0 right-0 block h-2 w-2 rounded-full bg-red-400 ring-2 ring-white"></span>
						</button>
					</div>
					
					<!-- User Info -->
					<div class="hidden md:flex items-center space-x-3">
						<div class="text-right">
							<p class="text-sm font-medium text-gray-900">{user?.first_name} {user?.last_name}</p>
							<p class="text-xs text-gray-500">Administrator</p>
						</div>
						<div class="w-8 h-8 bg-primary-600 rounded-full flex items-center justify-center">
							<span class="text-white text-sm font-medium">
								{user?.first_name?.[0]}{user?.last_name?.[0]}
							</span>
						</div>
					</div>
				</div>
			</div>
		</header>
		
		<!-- Page content -->
		<main class="p-4 sm:p-6 lg:p-8">
			<!-- Breadcrumb -->
			{#if currentPath !== '/admin'}
				<nav class="flex mb-6" aria-label="Breadcrumb">
					<ol class="inline-flex items-center space-x-1 md:space-x-3">
						<li class="inline-flex items-center">
							<a href="/admin" class="inline-flex items-center text-sm font-medium text-gray-700 hover:text-primary-600">
								<Home class="h-4 w-4 mr-2" />
								Dashboard
							</a>
						</li>
						{#if currentPath.includes('/alumni')}
							<li>
								<div class="flex items-center">
									<svg class="w-6 h-6 text-gray-400" fill="currentColor" viewBox="0 0 20 20">
										<path fill-rule="evenodd" d="M7.293 14.707a1 1 0 010-1.414L10.586 10 7.293 6.707a1 1 0 011.414-1.414l4 4a1 1 0 010 1.414l-4 4a1 1 0 01-1.414 0z" clip-rule="evenodd"></path>
									</svg>
									<span class="ml-1 text-sm font-medium text-gray-500 md:ml-2">Alumni Management</span>
								</div>
							</li>
						{:else if currentPath.includes('/events')}
							<li>
								<div class="flex items-center">
									<svg class="w-6 h-6 text-gray-400" fill="currentColor" viewBox="0 0 20 20">
										<path fill-rule="evenodd" d="M7.293 14.707a1 1 0 010-1.414L10.586 10 7.293 6.707a1 1 0 011.414-1.414l4 4a1 1 0 010 1.414l-4 4a1 1 0 01-1.414 0z" clip-rule="evenodd"></path>
									</svg>
									<span class="ml-1 text-sm font-medium text-gray-500 md:ml-2">Event Management</span>
								</div>
							</li>
						{:else if currentPath.includes('/content')}
							<li>
								<div class="flex items-center">
									<svg class="w-6 h-6 text-gray-400" fill="currentColor" viewBox="0 0 20 20">
										<path fill-rule="evenodd" d="M7.293 14.707a1 1 0 010-1.414L10.586 10 7.293 6.707a1 1 0 011.414-1.414l4 4a1 1 0 010 1.414l-4 4a1 1 0 01-1.414 0z" clip-rule="evenodd"></path>
									</svg>
									<span class="ml-1 text-sm font-medium text-gray-500 md:ml-2">Content Management</span>
								</div>
							</li>
						{:else if currentPath.includes('/users')}
							<li>
								<div class="flex items-center">
									<svg class="w-6 h-6 text-gray-400" fill="currentColor" viewBox="0 0 20 20">
										<path fill-rule="evenodd" d="M7.293 14.707a1 1 0 010-1.414L10.586 10 7.293 6.707a1 1 0 011.414-1.414l4 4a1 1 0 010 1.414l-4 4a1 1 0 01-1.414 0z" clip-rule="evenodd"></path>
									</svg>
									<span class="ml-1 text-sm font-medium text-gray-500 md:ml-2">User Management</span>
								</div>
							</li>
						{:else if currentPath.includes('/analytics')}
							<li>
								<div class="flex items-center">
									<svg class="w-6 h-6 text-gray-400" fill="currentColor" viewBox="0 0 20 20">
										<path fill-rule="evenodd" d="M7.293 14.707a1 1 0 010-1.414L10.586 10 7.293 6.707a1 1 0 011.414-1.414l4 4a1 1 0 010 1.414l-4 4a1 1 0 01-1.414 0z" clip-rule="evenodd"></path>
									</svg>
									<span class="ml-1 text-sm font-medium text-gray-500 md:ml-2">Analytics</span>
								</div>
							</li>
						{/if}
					</ol>
				</nav>
			{/if}
			<slot />
		</main>
	</div>
</div>
