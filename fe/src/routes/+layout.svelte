<script>
	import '../app.css';
	import { onMount } from 'svelte';
	import { authStore } from '$lib/stores/auth';
	import { page } from '$app/stores';
	import { Menu, X, Users, Calendar, Info, Mail, LogIn, UserPlus } from 'lucide-svelte';
	import Navigation from '$lib/components/Navigation.svelte';

	onMount(() => {
		authStore.init();
	});

	$: isAuthenticated = $authStore.isAuthenticated;
	$: user = $authStore.user;
	$: isAdmin = user?.roleName === 'Admin';
	$: isAlumni = user?.roleName === 'Alumni';
	
	let mobileMenuOpen = false;
</script>

<div class="min-h-screen bg-gray-50">
	<!-- Navigation Header -->
	<header class="bg-white shadow-sm border-b border-gray-200">
		<div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
			<div class="flex justify-between items-center h-16">
				<!-- Logo -->
				<div class="flex items-center">
					<a href="/" class="flex items-center space-x-2">
						<div class="w-8 h-8 bg-primary-600 rounded-lg flex items-center justify-center">
							<Users class="w-5 h-5 text-white" />
						</div>
						<span class="text-xl font-bold text-gray-900">Alumni Network</span>
					</a>
				</div>

				<!-- Desktop Navigation -->
				<nav class="hidden md:flex items-center space-x-8">
					{#if isAuthenticated}
						<Navigation type="main" className="flex items-center space-x-8" />
					{:else}
						<a href="/" class="text-gray-700 hover:text-primary-600 px-3 py-2 rounded-md text-sm font-medium">
							Home
						</a>
						<a href="/alumni" class="text-gray-700 hover:text-primary-600 px-3 py-2 rounded-md text-sm font-medium">
							Alumni Directory
						</a>
						<a href="/events" class="text-gray-700 hover:text-primary-600 px-3 py-2 rounded-md text-sm font-medium">
							Events
						</a>
						<a href="/about" class="text-gray-700 hover:text-primary-600 px-3 py-2 rounded-md text-sm font-medium">
							About
						</a>
						<a href="/contact" class="text-gray-700 hover:text-primary-600 px-3 py-2 rounded-md text-sm font-medium">
							Contact
						</a>
					{/if}
				</nav>

				<!-- Desktop Auth Buttons -->
				<div class="hidden md:flex items-center space-x-4">
					{#if isAuthenticated}
						<div class="flex items-center space-x-4">
							{#if isAdmin}
								<a href="/admin" class="text-gray-700 hover:text-primary-600 px-3 py-2 rounded-md text-sm font-medium">
									Admin Panel
								</a>
							{:else if isAlumni}
								<a href="/dashboard" class="text-gray-700 hover:text-primary-600 px-3 py-2 rounded-md text-sm font-medium">
									Dashboard
								</a>
							{/if}
							<div class="flex items-center space-x-2">
								<span class="text-sm text-gray-700">Welcome, {user?.firstName}</span>
								<button 
									onclick={() => authStore.logout()}
									class="text-gray-700 hover:text-primary-600 px-3 py-2 rounded-md text-sm font-medium"
								>
									Logout
								</button>
							</div>
						</div>
					{:else}
						<a href="/auth/login" class="text-gray-700 hover:text-primary-600 px-3 py-2 rounded-md text-sm font-medium flex items-center space-x-1">
							<LogIn class="w-4 h-4" />
							<span>Login</span>
						</a>
						<a href="/auth/register" class="btn-primary px-4 py-2 text-sm font-medium flex items-center space-x-1">
							<UserPlus class="w-4 h-4" />
							<span>Join</span>
						</a>
					{/if}
				</div>

				<!-- Mobile menu button -->
				<div class="md:hidden">
					<button
						onclick={() => mobileMenuOpen = !mobileMenuOpen}
						class="text-gray-700 hover:text-primary-600 p-2 rounded-md"
					>
						{#if mobileMenuOpen}
							<X class="w-6 h-6" />
						{:else}
							<Menu class="w-6 h-6" />
						{/if}
					</button>
				</div>
			</div>

			<!-- Mobile Navigation -->
			{#if mobileMenuOpen}
				<div class="md:hidden">
					<div class="px-2 pt-2 pb-3 space-y-1 sm:px-3 border-t border-gray-200">
						{#if isAuthenticated}
							<Navigation type="mobile" className="space-y-1" />
						{:else}
							<a href="/" class="text-gray-700 hover:text-primary-600 block px-3 py-2 rounded-md text-base font-medium">
								Home
							</a>
							<a href="/alumni" class="text-gray-700 hover:text-primary-600 block px-3 py-2 rounded-md text-base font-medium">
								Alumni Directory
							</a>
							<a href="/events" class="text-gray-700 hover:text-primary-600 block px-3 py-2 rounded-md text-base font-medium">
								Events
							</a>
							<a href="/about" class="text-gray-700 hover:text-primary-600 block px-3 py-2 rounded-md text-base font-medium">
								About
							</a>
							<a href="/contact" class="text-gray-700 hover:text-primary-600 block px-3 py-2 rounded-md text-base font-medium">
								Contact
							</a>
						{/if}
						
						{#if isAuthenticated}
							<div class="border-t border-gray-200 pt-4">
								{#if isAdmin}
									<a href="/admin" class="text-gray-700 hover:text-primary-600 block px-3 py-2 rounded-md text-base font-medium">
										Admin Panel
									</a>
								{:else if isAlumni}
									<a href="/dashboard" class="text-gray-700 hover:text-primary-600 block px-3 py-2 rounded-md text-base font-medium">
										Dashboard
									</a>
								{/if}
								<div class="px-3 py-2 text-sm text-gray-600">
									Welcome, {user?.firstName}
								</div>
								<button 
									onclick={() => authStore.logout()}
									class="text-gray-700 hover:text-primary-600 block w-full text-left px-3 py-2 rounded-md text-base font-medium"
								>
									Logout
								</button>
							</div>
						{:else}
							<div class="border-t border-gray-200 pt-4 space-y-2">
								<a href="/auth/login" class="text-gray-700 hover:text-primary-600 block px-3 py-2 rounded-md text-base font-medium">
									Login
								</a>
								<a href="/auth/register" class="btn-primary block px-3 py-2 rounded-md text-base font-medium text-center">
									Join Network
								</a>
							</div>
						{/if}
					</div>
				</div>
			{/if}
		</div>
	</header>

	<!-- Main Content -->
	<main>
		<slot />
	</main>

	<!-- Footer -->
	<footer class="bg-gray-900 text-white">
		<div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-12">
			<div class="grid grid-cols-1 md:grid-cols-4 gap-8">
				<div class="col-span-1 md:col-span-2">
					<div class="flex items-center space-x-2 mb-4">
						<div class="w-8 h-8 bg-primary-600 rounded-lg flex items-center justify-center">
							<Users class="w-5 h-5 text-white" />
						</div>
						<span class="text-xl font-bold">Alumni Network</span>
					</div>
					<p class="text-gray-300 mb-4 max-w-md">
						Connect with fellow alumni, discover opportunities, and stay updated with your alma mater's latest achievements and events.
					</p>
					<div class="flex space-x-4">
						<a href="#" class="text-gray-400 hover:text-white">
							<span class="sr-only">LinkedIn</span>
							<svg class="w-6 h-6" fill="currentColor" viewBox="0 0 24 24">
								<path d="M20.447 20.452h-3.554v-5.569c0-1.328-.027-3.037-1.852-3.037-1.853 0-2.136 1.445-2.136 2.939v5.667H9.351V9h3.414v1.561h.046c.477-.9 1.637-1.85 3.37-1.85 3.601 0 4.267 2.37 4.267 5.455v6.286zM5.337 7.433c-1.144 0-2.063-.926-2.063-2.065 0-1.138.92-2.063 2.063-2.063 1.14 0 2.064.925 2.064 2.063 0 1.139-.925 2.065-2.064 2.065zm1.782 13.019H3.555V9h3.564v11.452zM22.225 0H1.771C.792 0 0 .774 0 1.729v20.542C0 23.227.792 24 1.771 24h20.451C23.2 24 24 23.227 24 22.271V1.729C24 .774 23.2 0 22.222 0h.003z"/>
							</svg>
						</a>
						<a href="#" class="text-gray-400 hover:text-white">
							<span class="sr-only">Twitter</span>
							<svg class="w-6 h-6" fill="currentColor" viewBox="0 0 24 24">
								<path d="M23.953 4.57a10 10 0 01-2.825.775 4.958 4.958 0 002.163-2.723c-.951.555-2.005.959-3.127 1.184a4.92 4.92 0 00-8.384 4.482C7.69 8.095 4.067 6.13 1.64 3.162a4.822 4.822 0 00-.666 2.475c0 1.71.87 3.213 2.188 4.096a4.904 4.904 0 01-2.228-.616v.06a4.923 4.923 0 003.946 4.827 4.996 4.996 0 01-2.212.085 4.936 4.936 0 004.604 3.417 9.867 9.867 0 01-6.102 2.105c-.39 0-.779-.023-1.17-.067a13.995 13.995 0 007.557 2.209c9.053 0 13.998-7.496 13.998-13.985 0-.21 0-.42-.015-.63A9.935 9.935 0 0024 4.59z"/>
							</svg>
						</a>
					</div>
				</div>
				
				<div>
					<h3 class="text-lg font-semibold mb-4">Quick Links</h3>
					<ul class="space-y-2">
						<li><a href="/alumni" class="text-gray-300 hover:text-white">Alumni Directory</a></li>
						<li><a href="/events" class="text-gray-300 hover:text-white">Events</a></li>
						<li><a href="/about" class="text-gray-300 hover:text-white">About Us</a></li>
						<li><a href="/contact" class="text-gray-300 hover:text-white">Contact</a></li>
					</ul>
				</div>
				
				<div>
					<h3 class="text-lg font-semibold mb-4">Contact Info</h3>
					<div class="space-y-2 text-gray-300">
						<p>alumni@university.edu</p>
						<p>+1 (555) 123-4567</p>
						<p>123 University Ave<br>City, State 12345</p>
					</div>
				</div>
			</div>
			
			<div class="border-t border-gray-800 mt-8 pt-8 text-center text-gray-400">
				<p>&copy; 2024 Alumni Network. All rights reserved.</p>
			</div>
		</div>
	</footer>
</div>
