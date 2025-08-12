<script lang="ts">
	import { onMount } from 'svelte';
	import { apiService } from '$lib/api';
	import { Calendar, MapPin, Clock, Users, Plus, Search } from 'lucide-svelte';
	
	let events: any[] = [];
	let loading = true;
	let searchTerm = '';
	
	onMount(() => {
		loadEvents();
	});
	
	async function loadEvents() {
		try {
			const response = await apiService.getEvents();
			if (response.success && response.data) {
				events = response.data;
			}
		} catch (error) {
			console.error('Error loading events:', error);
		} finally {
			loading = false;
		}
	}
	
	function formatDate(dateString: string) {
		return new Date(dateString).toLocaleDateString('en-US', {
			weekday: 'long',
			year: 'numeric',
			month: 'long',
			day: 'numeric'
		});
	}
	
	function formatTime(dateString: string) {
		return new Date(dateString).toLocaleTimeString('en-US', {
			hour: '2-digit',
			minute: '2-digit'
		});
	}
	
	$: filteredEvents = events.filter(event => {
		return !searchTerm || 
			event.title?.toLowerCase().includes(searchTerm.toLowerCase()) ||
			event.description?.toLowerCase().includes(searchTerm.toLowerCase()) ||
			event.location?.toLowerCase().includes(searchTerm.toLowerCase());
	});
	
	$: upcomingEvents = filteredEvents.filter(event => new Date(event.start_date) > new Date());
	$: pastEvents = filteredEvents.filter(event => new Date(event.start_date) <= new Date());
</script>

<svelte:head>
	<title>Events - Alumni Network</title>
</svelte:head>

<div class="space-y-6">
	<!-- Header -->
	<div class="flex items-center justify-between">
		<div>
			<h1 class="text-2xl font-bold text-gray-900">Events</h1>
			<p class="text-gray-600 mt-1">Stay connected through alumni events and gatherings</p>
		</div>
		<button class="btn-primary flex items-center">
			<Plus class="h-4 w-4 mr-2" />
			Create Event
		</button>
	</div>

	<!-- Search -->
	<div class="card">
		<div class="relative">
			<Search class="absolute left-3 top-1/2 transform -translate-y-1/2 h-4 w-4 text-gray-400" />
			<input
				type="text"
				bind:value={searchTerm}
				placeholder="Search events..."
				class="input-field pl-10"
			/>
		</div>
	</div>

	<!-- Results -->
	{#if loading}
		<div class="flex items-center justify-center py-12">
			<div class="animate-spin rounded-full h-32 w-32 border-b-2 border-primary-600"></div>
		</div>
	{:else if filteredEvents.length === 0}
		<div class="card text-center py-12">
			<Calendar class="h-12 w-12 text-gray-400 mx-auto mb-4" />
			<h3 class="text-lg font-medium text-gray-900 mb-2">No events found</h3>
			<p class="text-gray-600">Try adjusting your search criteria.</p>
		</div>
	{:else}
		<!-- Upcoming Events -->
		{#if upcomingEvents.length > 0}
			<div class="space-y-4">
				<h2 class="text-xl font-semibold text-gray-900">Upcoming Events</h2>
				<div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
					{#each upcomingEvents as event}
						<div class="card hover:shadow-lg transition-shadow duration-200 border-l-4 border-green-500">
							<div class="space-y-3">
								<h3 class="text-lg font-semibold text-gray-900">{event.title}</h3>
								<p class="text-gray-600 text-sm line-clamp-2">{event.description}</p>
								
								<div class="space-y-2 text-sm text-gray-600">
									<div class="flex items-center">
										<Calendar class="h-4 w-4 mr-2" />
										{formatDate(event.start_date)}
									</div>
									<div class="flex items-center">
										<Clock class="h-4 w-4 mr-2" />
										{formatTime(event.start_date)}
									</div>
									{#if event.location}
										<div class="flex items-center">
											<MapPin class="h-4 w-4 mr-2" />
											{event.location}
										</div>
									{/if}
									{#if event.max_participants}
										<div class="flex items-center">
											<Users class="h-4 w-4 mr-2" />
											{event.max_participants} participants max
										</div>
									{/if}
								</div>
								
								<div class="pt-3 border-t border-gray-200">
									<button class="btn-primary w-full">
										Register
									</button>
								</div>
							</div>
						</div>
					{/each}
				</div>
			</div>
		{/if}

		<!-- Past Events -->
		{#if pastEvents.length > 0}
			<div class="space-y-4 mt-8">
				<h2 class="text-xl font-semibold text-gray-900">Past Events</h2>
				<div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
					{#each pastEvents as event}
						<div class="card hover:shadow-lg transition-shadow duration-200 border-l-4 border-gray-300 opacity-75">
							<div class="space-y-3">
								<h3 class="text-lg font-semibold text-gray-900">{event.title}</h3>
								<p class="text-gray-600 text-sm line-clamp-2">{event.description}</p>
								
								<div class="space-y-2 text-sm text-gray-600">
									<div class="flex items-center">
										<Calendar class="h-4 w-4 mr-2" />
										{formatDate(event.start_date)}
									</div>
									<div class="flex items-center">
										<Clock class="h-4 w-4 mr-2" />
										{formatTime(event.start_date)}
									</div>
									{#if event.location}
										<div class="flex items-center">
											<MapPin class="h-4 w-4 mr-2" />
											{event.location}
										</div>
									{/if}
								</div>
								
								<div class="pt-3 border-t border-gray-200">
									<button class="text-primary-600 hover:text-primary-500 text-sm font-medium">
										View Details
									</button>
								</div>
							</div>
						</div>
					{/each}
				</div>
			</div>
		{/if}
		
		<!-- Results Summary -->
		<div class="text-center text-sm text-gray-600 mt-6">
			Showing {filteredEvents.length} of {events.length} events
		</div>
	{/if}
</div>

