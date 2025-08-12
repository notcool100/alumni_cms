<script lang="ts">
	import { onMount } from 'svelte';
	import { apiService } from '$lib/api';
	import { 
		Users, 
		Calendar, 
		MapPin, 
		Building, 
		ArrowRight
	} from 'lucide-svelte';
	
	let user: any = null;
	let alumni: any[] = [];
	let events: any[] = [];
	let loading = true;
	
	onMount(() => {
		// Check if user is authenticated
		if (!apiService.isAuthenticated()) {
			return;
		}
		
		user = apiService.getCurrentUserFromStorage();
		loadDashboardData();
	});
	
	async function loadDashboardData() {
		try {
			// Load alumni data
			const alumniResponse = await apiService.getAlumni();
			if (alumniResponse.success && alumniResponse.data) {
				alumni = alumniResponse.data.slice(0, 6); // Show first 6
			}
			
			// Load events data
			const eventsResponse = await apiService.getEvents();
			if (eventsResponse.success && eventsResponse.data) {
				events = eventsResponse.data.slice(0, 4); // Show first 4
			}
		} catch (error) {
			console.error('Error loading dashboard data:', error);
		} finally {
			loading = false;
		}
	}
	
	function formatDate(dateString: string) {
		return new Date(dateString).toLocaleDateString('en-US', {
			year: 'numeric',
			month: 'short',
			day: 'numeric'
		});
	}
</script>

<svelte:head>
	<title>Dashboard - Alumni Network</title>
</svelte:head>

{#if loading}
	<div class="flex items-center justify-center py-12">
		<div class="animate-spin rounded-full h-32 w-32 border-b-2 border-primary-600"></div>
	</div>
{:else}
	<!-- Welcome Section -->
	<div class="mb-8">
		<h1 class="text-3xl font-bold text-gray-900">Welcome back, {user?.first_name}!</h1>
		<p class="text-gray-600 mt-2">Here's what's happening in your alumni network today.</p>
	</div>

	<!-- Stats Grid -->
	<div class="grid grid-cols-1 md:grid-cols-4 gap-6 mb-8">
		<div class="card">
			<div class="flex items-center">
				<div class="bg-primary-100 p-3 rounded-lg">
					<Users class="h-6 w-6 text-primary-600" />
				</div>
				<div class="ml-4">
					<p class="text-sm font-medium text-gray-600">Total Alumni</p>
					<p class="text-2xl font-bold text-gray-900">2,547</p>
				</div>
			</div>
		</div>
		
		<div class="card">
			<div class="flex items-center">
				<div class="bg-green-100 p-3 rounded-lg">
					<Calendar class="h-6 w-6 text-green-600" />
				</div>
				<div class="ml-4">
					<p class="text-sm font-medium text-gray-600">Upcoming Events</p>
					<p class="text-2xl font-bold text-gray-900">12</p>
				</div>
			</div>
		</div>
		
		<div class="card">
			<div class="flex items-center">
				<div class="bg-yellow-100 p-3 rounded-lg">
					<MapPin class="h-6 w-6 text-yellow-600" />
				</div>
				<div class="ml-4">
					<p class="text-sm font-medium text-gray-600">Local Chapters</p>
					<p class="text-2xl font-bold text-gray-900">28</p>
				</div>
			</div>
		</div>
		
		<div class="card">
			<div class="flex items-center">
				<div class="bg-purple-100 p-3 rounded-lg">
					<Building class="h-6 w-6 text-purple-600" />
				</div>
				<div class="ml-4">
					<p class="text-sm font-medium text-gray-600">Job Postings</p>
					<p class="text-2xl font-bold text-gray-900">34</p>
				</div>
			</div>
		</div>
	</div>

	<!-- Main Content Grid -->
	<div class="grid grid-cols-1 lg:grid-cols-3 gap-8">
		<!-- Recent Alumni -->
		<div class="lg:col-span-2">
			<div class="card">
				<div class="flex items-center justify-between mb-6">
					<h2 class="text-xl font-semibold text-gray-900">Recent Alumni</h2>
					<a href="/dashboard/alumni" class="text-primary-600 hover:text-primary-500 text-sm font-medium">
						View all
					</a>
				</div>
				
				<div class="space-y-4">
					{#each alumni as person}
						<div class="flex items-center space-x-4 p-4 bg-gray-50 rounded-lg">
							<div class="w-12 h-12 bg-primary-600 rounded-full flex items-center justify-center">
								<span class="text-white font-medium">
									{person.graduation_year?.toString().slice(-2)}
								</span>
							</div>
							<div class="flex-1">
								<p class="text-sm font-medium text-gray-900">
									{person.graduation_year} Graduate
								</p>
								<p class="text-sm text-gray-500">
									{person.degree} in {person.major}
								</p>
								{#if person.current_company}
									<p class="text-sm text-gray-500">
										{person.current_position} at {person.current_company}
									</p>
								{/if}
							</div>
							<button class="text-primary-600 hover:text-primary-500">
								<ArrowRight class="h-5 w-5" />
							</button>
						</div>
					{/each}
				</div>
			</div>
		</div>

		<!-- Upcoming Events -->
		<div>
			<div class="card">
				<div class="flex items-center justify-between mb-6">
					<h2 class="text-xl font-semibold text-gray-900">Upcoming Events</h2>
					<a href="/dashboard/events" class="text-primary-600 hover:text-primary-500 text-sm font-medium">
						View all
					</a>
				</div>
				
				<div class="space-y-4">
					{#each events as event}
						<div class="border-l-4 border-primary-500 pl-4">
							<h3 class="text-sm font-medium text-gray-900">{event.title}</h3>
							<p class="text-sm text-gray-500 mt-1">{formatDate(event.start_date)}</p>
							{#if event.location}
								<p class="text-sm text-gray-500">{event.location}</p>
							{/if}
							<a href="/dashboard/events/{event.id}" class="mt-2 text-xs text-primary-600 hover:text-primary-500 font-medium">
								Learn more
							</a>
						</div>
					{/each}
				</div>
				
				<div class="mt-6">
					<a href="/dashboard/events" class="btn-primary w-full text-center">
						View All Events
					</a>
				</div>
			</div>
		</div>
	</div>
{/if}
