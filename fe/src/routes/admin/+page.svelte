<script lang="ts">
	import { onMount } from 'svelte';
	import { apiService } from '$lib/api';
	import { 
		Users, 
		Calendar, 
		FileText, 
		TrendingUp, 
		Plus,
		ArrowUpRight,
		ArrowDownRight,
		Eye,
		Edit,
		Trash2
	} from 'lucide-svelte';
	
	let stats = {
		totalAlumni: 0,
		totalEvents: 0,
		totalUsers: 0,
		recentRegistrations: 0
	};
	
	let recentAlumni: any[] = [];
	let upcomingEvents: any[] = [];
	let loading = true;
	
	onMount(() => {
		loadDashboardData();
	});
	
	async function loadDashboardData() {
		try {
			// Load alumni data
			const alumniResponse = await apiService.getAlumni();
			if (alumniResponse.success && alumniResponse.data) {
				stats.totalAlumni = alumniResponse.data.length;
				recentAlumni = alumniResponse.data.slice(0, 5);
			}
			
			// Load events data
			const eventsResponse = await apiService.getEvents();
			if (eventsResponse.success && eventsResponse.data) {
				stats.totalEvents = eventsResponse.data.length;
				upcomingEvents = eventsResponse.data
					.filter((event: any) => new Date(event.start_date) > new Date())
					.slice(0, 5);
			}
			
			// Mock data for demo
			stats.totalUsers = 125;
			stats.recentRegistrations = 12;
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
	<title>Admin Dashboard - Alumni Network CMS</title>
</svelte:head>

{#if loading}
	<div class="flex items-center justify-center h-64">
		<div class="animate-spin rounded-full h-12 w-12 border-b-2 border-primary-600"></div>
	</div>
{:else}
	<!-- Page Header -->
	<div class="mb-8">
		<h1 class="text-3xl font-bold text-gray-900">Admin Dashboard</h1>
		<p class="text-gray-600 mt-2">Welcome to the Alumni Network Content Management System</p>
	</div>

	<!-- Stats Grid -->
	<div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
		<div class="card">
			<div class="flex items-center justify-between">
				<div>
					<p class="text-sm font-medium text-gray-600">Total Alumni</p>
					<p class="text-3xl font-bold text-gray-900">{stats.totalAlumni}</p>
					<p class="text-sm text-green-600 flex items-center mt-1">
						<ArrowUpRight class="h-4 w-4 mr-1" />
						+12% from last month
					</p>
				</div>
				<div class="bg-primary-100 p-3 rounded-lg">
					<Users class="h-8 w-8 text-primary-600" />
				</div>
			</div>
		</div>
		
		<div class="card">
			<div class="flex items-center justify-between">
				<div>
					<p class="text-sm font-medium text-gray-600">Total Events</p>
					<p class="text-3xl font-bold text-gray-900">{stats.totalEvents}</p>
					<p class="text-sm text-green-600 flex items-center mt-1">
						<ArrowUpRight class="h-4 w-4 mr-1" />
						+5 this month
					</p>
				</div>
				<div class="bg-green-100 p-3 rounded-lg">
					<Calendar class="h-8 w-8 text-green-600" />
				</div>
			</div>
		</div>
		
		<div class="card">
			<div class="flex items-center justify-between">
				<div>
					<p class="text-sm font-medium text-gray-600">Total Users</p>
					<p class="text-3xl font-bold text-gray-900">{stats.totalUsers}</p>
					<p class="text-sm text-green-600 flex items-center mt-1">
						<ArrowUpRight class="h-4 w-4 mr-1" />
						+8% from last month
					</p>
				</div>
				<div class="bg-yellow-100 p-3 rounded-lg">
					<FileText class="h-8 w-8 text-yellow-600" />
				</div>
			</div>
		</div>
		
		<div class="card">
			<div class="flex items-center justify-between">
				<div>
					<p class="text-sm font-medium text-gray-600">Recent Registrations</p>
					<p class="text-3xl font-bold text-gray-900">{stats.recentRegistrations}</p>
					<p class="text-sm text-red-600 flex items-center mt-1">
						<ArrowDownRight class="h-4 w-4 mr-1" />
						-3% from last week
					</p>
				</div>
				<div class="bg-purple-100 p-3 rounded-lg">
					<TrendingUp class="h-8 w-8 text-purple-600" />
				</div>
			</div>
		</div>
	</div>

	<!-- Quick Actions -->
	<div class="grid grid-cols-1 md:grid-cols-3 gap-6 mb-8">
		<div class="card">
			<h3 class="text-lg font-semibold text-gray-900 mb-4">Quick Actions</h3>
			<div class="space-y-3">
				<a href="/admin/alumni/new" class="flex items-center p-3 bg-primary-50 rounded-lg hover:bg-primary-100 transition-colors duration-200">
					<Plus class="h-5 w-5 text-primary-600 mr-3" />
					<span class="text-sm font-medium text-primary-700">Add New Alumni</span>
				</a>
				<a href="/admin/events/new" class="flex items-center p-3 bg-green-50 rounded-lg hover:bg-green-100 transition-colors duration-200">
					<Plus class="h-5 w-5 text-green-600 mr-3" />
					<span class="text-sm font-medium text-green-700">Create Event</span>
				</a>
				<a href="/admin/content/new" class="flex items-center p-3 bg-yellow-50 rounded-lg hover:bg-yellow-100 transition-colors duration-200">
					<Plus class="h-5 w-5 text-yellow-600 mr-3" />
					<span class="text-sm font-medium text-yellow-700">Add Content</span>
				</a>
			</div>
		</div>
		
		<div class="card">
			<h3 class="text-lg font-semibold text-gray-900 mb-4">Recent Alumni</h3>
			<div class="space-y-3">
				{#each recentAlumni as alumni}
					<div class="flex items-center justify-between p-3 bg-gray-50 rounded-lg">
						<div>
							<p class="text-sm font-medium text-gray-900">{alumni.graduation_year} Graduate</p>
							<p class="text-xs text-gray-500">{alumni.degree} in {alumni.major}</p>
						</div>
						<div class="flex space-x-2">
							<button class="p-1 text-gray-400 hover:text-gray-600">
								<Eye class="h-4 w-4" />
							</button>
							<button class="p-1 text-gray-400 hover:text-blue-600">
								<Edit class="h-4 w-4" />
							</button>
						</div>
					</div>
				{/each}
			</div>
		</div>
		
		<div class="card">
			<h3 class="text-lg font-semibold text-gray-900 mb-4">Upcoming Events</h3>
			<div class="space-y-3">
				{#each upcomingEvents as event}
					<div class="p-3 bg-gray-50 rounded-lg">
						<p class="text-sm font-medium text-gray-900">{event.title}</p>
						<p class="text-xs text-gray-500">{formatDate(event.start_date)}</p>
						{#if event.location}
							<p class="text-xs text-gray-500">{event.location}</p>
						{/if}
					</div>
				{/each}
			</div>
		</div>
	</div>

	<!-- Recent Activity -->
	<div class="card">
		<h3 class="text-lg font-semibold text-gray-900 mb-4">Recent Activity</h3>
		<div class="space-y-4">
			<div class="flex items-center space-x-4 p-4 bg-gray-50 rounded-lg">
				<div class="w-8 h-8 bg-green-100 rounded-full flex items-center justify-center">
					<Users class="h-4 w-4 text-green-600" />
				</div>
				<div class="flex-1">
					<p class="text-sm font-medium text-gray-900">New alumni registration</p>
					<p class="text-xs text-gray-500">John Doe joined the network</p>
				</div>
				<span class="text-xs text-gray-500">2 hours ago</span>
			</div>
			
			<div class="flex items-center space-x-4 p-4 bg-gray-50 rounded-lg">
				<div class="w-8 h-8 bg-blue-100 rounded-full flex items-center justify-center">
					<Calendar class="h-4 w-4 text-blue-600" />
				</div>
				<div class="flex-1">
					<p class="text-sm font-medium text-gray-900">Event created</p>
					<p class="text-xs text-gray-500">Alumni Networking Mixer scheduled</p>
				</div>
				<span class="text-xs text-gray-500">4 hours ago</span>
			</div>
			
			<div class="flex items-center space-x-4 p-4 bg-gray-50 rounded-lg">
				<div class="w-8 h-8 bg-yellow-100 rounded-full flex items-center justify-center">
					<FileText class="h-4 w-4 text-yellow-600" />
				</div>
				<div class="flex-1">
					<p class="text-sm font-medium text-gray-900">Content updated</p>
					<p class="text-xs text-gray-500">Homepage announcement modified</p>
				</div>
				<span class="text-xs text-gray-500">1 day ago</span>
			</div>
		</div>
	</div>
{/if}
