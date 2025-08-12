<script lang="ts">
	import { onMount } from 'svelte';
	import { apiService, type Event } from '$lib/api';
	import { Calendar, MapPin, Clock, Users, ExternalLink, Filter, Search } from 'lucide-svelte';

	let events: Event[] = [];
	let filteredEvents: Event[] = [];
	let loading = true;
	let error = '';
	
	// Filter state
	let searchTerm = '';
	let selectedType = '';
	let showPastEvents = false;
	
	// Get current date for filtering
	const now = new Date();

	onMount(async () => {
		try {
			const response = await apiService.getEvents();
			if (response.success && response.data) {
				events = response.data;
				filteredEvents = events;
			}
		} catch (err) {
			error = 'Failed to load events';
			console.error('Error loading events:', err);
		} finally {
			loading = false;
		}
	});

	// Filter events based on search and filters
	$: {
		filteredEvents = events.filter(event => {
			const matchesSearch = !searchTerm || 
				event.title.toLowerCase().includes(searchTerm.toLowerCase()) ||
				event.description.toLowerCase().includes(searchTerm.toLowerCase()) ||
				event.location?.toLowerCase().includes(searchTerm.toLowerCase());
			
			const matchesType = !selectedType || 
				(selectedType === 'online' && event.is_online) ||
				(selectedType === 'in-person' && !event.is_online);
			
			const eventDate = new Date(event.start_date);
			const matchesTimeFilter = showPastEvents || eventDate >= now;
			
			return matchesSearch && matchesType && matchesTimeFilter;
		}).sort((a, b) => new Date(a.start_date).getTime() - new Date(b.start_date).getTime());
	}

	function clearFilters() {
		searchTerm = '';
		selectedType = '';
		showPastEvents = false;
	}

	function formatDate(dateString: string): string {
		const date = new Date(dateString);
		return date.toLocaleDateString('en-US', {
			weekday: 'long',
			year: 'numeric',
			month: 'long',
			day: 'numeric'
		});
	}

	function formatTime(dateString: string): string {
		const date = new Date(dateString);
		return date.toLocaleTimeString('en-US', {
			hour: 'numeric',
			minute: '2-digit',
			hour12: true
		});
	}

	function isEventUpcoming(event: Event): boolean {
		return new Date(event.start_date) > now;
	}

	function isEventToday(event: Event): boolean {
		const eventDate = new Date(event.start_date);
		const today = new Date();
		return eventDate.toDateString() === today.toDateString();
	}

	function getEventStatus(event: Event): { text: string; class: string } {
		if (isEventToday(event)) {
			return { text: 'Today', class: 'bg-green-100 text-green-800' };
		} else if (isEventUpcoming(event)) {
			return { text: 'Upcoming', class: 'bg-blue-100 text-blue-800' };
		} else {
			return { text: 'Past', class: 'bg-gray-100 text-gray-800' };
		}
	}
</script>

<svelte:head>
	<title>Events - Alumni Network</title>
	<meta name="description" content="Discover upcoming alumni events, reunions, and networking opportunities." />
</svelte:head>

<!-- Hero Section -->
<section class="bg-gradient-to-br from-primary-600 to-primary-800 text-white py-16">
	<div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
		<div class="text-center">
			<h1 class="text-4xl md:text-5xl font-bold mb-4">Alumni Events</h1>
			<p class="text-xl text-primary-100 max-w-3xl mx-auto">
				Stay connected with your alma mater through our exciting events, reunions, and networking opportunities.
			</p>
		</div>
	</div>
</section>

<!-- Search and Filters -->
<section class="bg-white border-b border-gray-200 py-8">
	<div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
		<div class="space-y-6">
			<!-- Search Bar -->
			<div class="relative">
				<Search class="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 w-5 h-5" />
				<input
					type="text"
					placeholder="Search events by title, description, or location..."
					bind:value={searchTerm}
					class="w-full pl-10 pr-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-primary-500 focus:border-primary-500"
				/>
			</div>

			<!-- Filters -->
			<div class="grid grid-cols-1 md:grid-cols-3 gap-4">
				<div>
					<label for="type-filter" class="block text-sm font-medium text-gray-700 mb-2">Event Type</label>
					<select
						id="type-filter"
						bind:value={selectedType}
						class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-primary-500 focus:border-primary-500"
					>
						<option value="">All Events</option>
						<option value="online">Online Events</option>
						<option value="in-person">In-Person Events</option>
					</select>
				</div>

				<div class="flex items-end">
					<label class="flex items-center space-x-2">
						<input
							type="checkbox"
							bind:checked={showPastEvents}
							class="rounded border-gray-300 text-primary-600 focus:ring-primary-500"
						/>
						<span class="text-sm text-gray-700">Show past events</span>
					</label>
				</div>

				<div class="flex items-end">
					<button
						onclick={clearFilters}
						class="w-full px-4 py-2 bg-gray-100 text-gray-700 rounded-lg hover:bg-gray-200 transition-colors flex items-center justify-center space-x-2"
					>
						<Filter class="w-4 h-4" />
						<span>Clear Filters</span>
					</button>
				</div>
			</div>
		</div>
	</div>
</section>

<!-- Events Section -->
<section class="py-12">
	<div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
		{#if loading}
			<div class="text-center py-12">
				<div class="animate-spin rounded-full h-12 w-12 border-b-2 border-primary-600 mx-auto"></div>
				<p class="mt-4 text-gray-600">Loading events...</p>
			</div>
		{:else if error}
			<div class="text-center py-12">
				<p class="text-red-600">{error}</p>
			</div>
		{:else}
			<!-- Results Header -->
			<div class="flex justify-between items-center mb-8">
				<h2 class="text-2xl font-bold text-gray-900">
					{filteredEvents.length} Events Found
				</h2>
				{#if searchTerm || selectedType || showPastEvents}
					<p class="text-sm text-gray-600">
						Showing filtered results
					</p>
				{/if}
			</div>

			{#if filteredEvents.length === 0}
				<div class="text-center py-12">
					<div class="w-16 h-16 bg-gray-100 rounded-full flex items-center justify-center mx-auto mb-4">
						<Calendar class="w-8 h-8 text-gray-400" />
					</div>
					<h3 class="text-lg font-medium text-gray-900 mb-2">No events found</h3>
					<p class="text-gray-600 mb-4">
						Try adjusting your search criteria or filters to find more events.
					</p>
					<button
						onclick={clearFilters}
						class="btn-primary"
					>
						Clear All Filters
					</button>
				</div>
			{:else}
				<!-- Events Grid -->
				<div class="space-y-6">
					{#each filteredEvents as event}
						{@const status = getEventStatus(event)}
						<div class="bg-white rounded-lg shadow-md hover:shadow-lg transition-shadow border border-gray-200">
							<div class="p-6">
								<div class="flex flex-col lg:flex-row lg:items-start lg:justify-between">
									<!-- Event Info -->
									<div class="flex-1">
										<div class="flex items-start justify-between mb-4">
											<div class="flex-1">
												<h3 class="text-xl font-semibold text-gray-900 mb-2">
													{event.title}
												</h3>
												<p class="text-gray-600 mb-4 line-clamp-2">
													{event.description}
												</p>
											</div>
											<span class="ml-4 px-3 py-1 rounded-full text-xs font-medium {status.class}">
												{status.text}
											</span>
										</div>

										<!-- Event Details -->
										<div class="grid grid-cols-1 md:grid-cols-2 gap-4 mb-4">
											<div class="flex items-center space-x-2 text-sm text-gray-600">
												<Calendar class="w-4 h-4" />
												<span>{formatDate(event.start_date)}</span>
											</div>
											<div class="flex items-center space-x-2 text-sm text-gray-600">
												<Clock class="w-4 h-4" />
												<span>{formatTime(event.start_date)} - {formatTime(event.end_date)}</span>
											</div>
											
											{#if event.location && !event.is_online}
												<div class="flex items-center space-x-2 text-sm text-gray-600">
													<MapPin class="w-4 h-4" />
													<span>{event.location}</span>
												</div>
											{:else if event.is_online}
												<div class="flex items-center space-x-2 text-sm text-gray-600">
													<ExternalLink class="w-4 h-4" />
													<span>Online Event</span>
												</div>
											{/if}
											
											<div class="flex items-center space-x-2 text-sm text-gray-600">
												<Users class="w-4 h-4" />
												<span>
													{event.current_attendees}
													{#if event.max_attendees}
														/ {event.max_attendees}
													{/if}
													attendees
												</span>
											</div>
										</div>

										<!-- Meeting URL for online events -->
										{#if event.is_online && event.meeting_url}
											<div class="mb-4">
												<a
													href={event.meeting_url}
													target="_blank"
													rel="noopener noreferrer"
													class="inline-flex items-center space-x-2 text-primary-600 hover:text-primary-700 text-sm font-medium"
												>
													<ExternalLink class="w-4 h-4" />
													<span>Join Meeting</span>
												</a>
											</div>
										{/if}
									</div>

									<!-- Action Buttons -->
									<div class="mt-4 lg:mt-0 lg:ml-6 flex flex-col space-y-2">
										{#if isEventUpcoming(event)}
											<button class="btn-primary whitespace-nowrap">
												Register for Event
											</button>
										{/if}
										<a
											href="/events/{event.id}"
											class="btn-secondary whitespace-nowrap"
										>
											View Details
										</a>
									</div>
								</div>
							</div>
						</div>
					{/each}
				</div>
			{/if}
		{/if}
	</div>
</section>

<!-- CTA Section -->
<section class="bg-gray-50 py-16">
	<div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 text-center">
		<h2 class="text-3xl font-bold text-gray-900 mb-4">
			Want to Stay Updated with Events?
		</h2>
		<p class="text-xl text-gray-600 mb-8 max-w-2xl mx-auto">
			Join our alumni network to receive event notifications, register for events, and connect with fellow alumni.
		</p>
		<div class="flex flex-col sm:flex-row gap-4 justify-center">
			<a href="/auth/register" class="btn-primary text-lg px-8 py-3">
				Join the Network
			</a>
			<a href="/auth/login" class="btn-secondary text-lg px-8 py-3">
				Sign In
			</a>
		</div>
	</div>
</section>

<style>
	.line-clamp-2 {
		display: -webkit-box;
		-webkit-line-clamp: 2;
		-webkit-box-orient: vertical;
		overflow: hidden;
	}
</style>
