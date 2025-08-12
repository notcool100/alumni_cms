<script lang="ts">
	import { onMount } from 'svelte';
	import { page } from '$app/stores';
	import { apiService, type Event } from '$lib/api';
	import { Calendar, MapPin, Clock, Users, ExternalLink, ArrowLeft, Building } from 'lucide-svelte';

	let event: Event | null = null;
	let loading = true;
	let error = '';

	$: eventId = $page.params.id;

	onMount(async () => {
		try {
			const response = await apiService.getEventById(eventId);
			if (response.success && response.data) {
				event = response.data;
			} else {
				error = 'Event not found';
			}
		} catch (err) {
			error = 'Failed to load event';
			console.error('Error loading event:', err);
		} finally {
			loading = false;
		}
	});

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
		return new Date(event.start_date) > new Date();
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

	function getRegistrationStatus(event: Event): { text: string; class: string; disabled: boolean } {
		if (!isEventUpcoming(event)) {
			return { text: 'Event Ended', class: 'bg-gray-100 text-gray-600', disabled: true };
		}
		
		if (event.max_attendees && event.current_attendees >= event.max_attendees) {
			return { text: 'Fully Booked', class: 'bg-red-100 text-red-800', disabled: true };
		}
		
		return { text: 'Register Now', class: 'bg-primary-100 text-primary-800', disabled: false };
	}
</script>

<svelte:head>
	<title>{event ? event.title : 'Event Details - Alumni Network'}</title>
	<meta name="description" content={event ? event.description : 'Event details page'} />
</svelte:head>

{#if loading}
	<div class="min-h-screen bg-gray-50 flex items-center justify-center">
		<div class="text-center">
			<div class="animate-spin rounded-full h-12 w-12 border-b-2 border-primary-600 mx-auto"></div>
			<p class="mt-4 text-gray-600">Loading event details...</p>
		</div>
	</div>
{:else if error}
	<div class="min-h-screen bg-gray-50 flex items-center justify-center">
		<div class="text-center">
			<div class="w-16 h-16 bg-red-100 rounded-full flex items-center justify-center mx-auto mb-4">
				<Calendar class="w-8 h-8 text-red-600" />
			</div>
			<h2 class="text-2xl font-bold text-gray-900 mb-2">Event Not Found</h2>
			<p class="text-gray-600 mb-6">{error}</p>
			<a href="/events" class="btn-primary">
				Back to Events
			</a>
		</div>
	</div>
{:else if event}
	<!-- Hero Section -->
	<section class="bg-gradient-to-br from-primary-600 to-primary-800 text-white py-16">
		<div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
			<div class="flex items-center space-x-4 mb-6">
				<a href="/events" class="flex items-center space-x-2 text-primary-100 hover:text-white transition-colors">
					<ArrowLeft class="w-5 h-5" />
					<span>Back to Events</span>
				</a>
			</div>
			
			<div class="max-w-4xl">
				<div class="flex items-start justify-between mb-6">
					<div class="flex-1">
						<h1 class="text-3xl md:text-4xl font-bold mb-4">
							{event.title}
						</h1>
						<p class="text-xl text-primary-100 mb-6">
							{event.description}
						</p>
					</div>
					{@const status = getEventStatus(event)}
					<span class="ml-6 px-4 py-2 rounded-full text-sm font-medium {status.class}">
						{status.text}
					</span>
				</div>
				
				<!-- Event Details -->
				<div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
					<div class="flex items-center space-x-3">
						<Calendar class="w-6 h-6 text-primary-200" />
						<div>
							<p class="text-sm text-primary-200">Date</p>
							<p class="font-semibold">{formatDate(event.start_date)}</p>
						</div>
					</div>
					
					<div class="flex items-center space-x-3">
						<Clock class="w-6 h-6 text-primary-200" />
						<div>
							<p class="text-sm text-primary-200">Time</p>
							<p class="font-semibold">{formatTime(event.start_date)} - {formatTime(event.end_date)}</p>
						</div>
					</div>
					
					{#if event.location && !event.is_online}
						<div class="flex items-center space-x-3">
							<MapPin class="w-6 h-6 text-primary-200" />
							<div>
								<p class="text-sm text-primary-200">Location</p>
								<p class="font-semibold">{event.location}</p>
							</div>
						</div>
					{:else if event.is_online}
						<div class="flex items-center space-x-3">
							<ExternalLink class="w-6 h-6 text-primary-200" />
							<div>
								<p class="text-sm text-primary-200">Format</p>
								<p class="font-semibold">Online Event</p>
							</div>
						</div>
					{/if}
					
					<div class="flex items-center space-x-3">
						<Users class="w-6 h-6 text-primary-200" />
						<div>
							<p class="text-sm text-primary-200">Attendees</p>
							<p class="font-semibold">
								{event.current_attendees}
								{#if event.max_attendees}
									/ {event.max_attendees}
								{/if}
							</p>
						</div>
					</div>
				</div>
			</div>
		</div>
	</section>

	<!-- Event Details -->
	<section class="py-12 bg-white">
		<div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
			<div class="grid grid-cols-1 lg:grid-cols-3 gap-8">
				<!-- Main Content -->
				<div class="lg:col-span-2">
					<!-- Event Description -->
					<div class="mb-8">
						<h2 class="text-2xl font-bold text-gray-900 mb-4">About This Event</h2>
						<div class="prose max-w-none">
							<p class="text-gray-600 leading-relaxed">
								{event.description}
							</p>
						</div>
					</div>

					<!-- Event Schedule -->
					<div class="mb-8">
						<h2 class="text-2xl font-bold text-gray-900 mb-4">Event Schedule</h2>
						<div class="bg-gray-50 rounded-lg p-6">
							<div class="space-y-4">
								<div class="flex items-center space-x-4">
									<div class="bg-primary-100 w-12 h-12 rounded-lg flex items-center justify-center flex-shrink-0">
										<Clock class="w-6 h-6 text-primary-600" />
									</div>
									<div class="flex-1">
										<h3 class="text-lg font-semibold text-gray-900">Event Start</h3>
										<p class="text-gray-600">{formatDate(event.start_date)} at {formatTime(event.start_date)}</p>
									</div>
								</div>
								
								<div class="flex items-center space-x-4">
									<div class="bg-primary-100 w-12 h-12 rounded-lg flex items-center justify-center flex-shrink-0">
										<Clock class="w-6 h-6 text-primary-600" />
									</div>
									<div class="flex-1">
										<h3 class="text-lg font-semibold text-gray-900">Event End</h3>
										<p class="text-gray-600">{formatDate(event.end_date)} at {formatTime(event.end_date)}</p>
									</div>
								</div>
							</div>
						</div>
					</div>

					<!-- Location Details -->
					{#if event.location && !event.is_online}
						<div class="mb-8">
							<h2 class="text-2xl font-bold text-gray-900 mb-4">Location</h2>
							<div class="bg-gray-50 rounded-lg p-6">
								<div class="flex items-start space-x-4">
									<div class="bg-primary-100 w-12 h-12 rounded-lg flex items-center justify-center flex-shrink-0">
										<MapPin class="w-6 h-6 text-primary-600" />
									</div>
									<div class="flex-1">
										<h3 class="text-lg font-semibold text-gray-900 mb-2">{event.location}</h3>
										<p class="text-gray-600">
											Please arrive 15 minutes before the event start time. Parking is available on-site.
										</p>
									</div>
								</div>
							</div>
						</div>
					{:else if event.is_online && event.meeting_url}
						<div class="mb-8">
							<h2 class="text-2xl font-bold text-gray-900 mb-4">Online Meeting</h2>
							<div class="bg-gray-50 rounded-lg p-6">
								<div class="flex items-start space-x-4">
									<div class="bg-primary-100 w-12 h-12 rounded-lg flex items-center justify-center flex-shrink-0">
										<ExternalLink class="w-6 h-6 text-primary-600" />
									</div>
									<div class="flex-1">
										<h3 class="text-lg font-semibold text-gray-900 mb-2">Virtual Meeting</h3>
										<p class="text-gray-600 mb-4">
											This is an online event. You'll receive the meeting link after registration.
										</p>
										<a
											href={event.meeting_url}
											target="_blank"
											rel="noopener noreferrer"
											class="btn-primary inline-flex items-center space-x-2"
										>
											<ExternalLink class="w-4 h-4" />
											<span>Join Meeting</span>
										</a>
									</div>
								</div>
							</div>
						</div>
					{/if}
				</div>

				<!-- Sidebar -->
				<div class="lg:col-span-1">
					<!-- Registration Card -->
					<div class="bg-gray-50 rounded-lg p-6 mb-6 sticky top-6">
						{@const regStatus = getRegistrationStatus(event)}
						<h3 class="text-lg font-semibold text-gray-900 mb-4">Registration</h3>
						
						<div class="space-y-4">
							<div class="flex justify-between items-center">
								<span class="text-gray-600">Attendees</span>
								<span class="font-semibold text-gray-900">
									{event.current_attendees}
									{#if event.max_attendees}
										/ {event.max_attendees}
									{/if}
								</span>
							</div>
							
							{#if event.max_attendees}
								<div class="w-full bg-gray-200 rounded-full h-2">
									<div 
										class="bg-primary-600 h-2 rounded-full" 
										style="width: {(event.current_attendees / event.max_attendees) * 100}%"
									></div>
								</div>
								<p class="text-sm text-gray-500">
									{event.max_attendees - event.current_attendees} spots remaining
								</p>
							{/if}
							
							<button
								class="w-full py-3 px-4 rounded-lg font-medium {regStatus.disabled ? 'bg-gray-300 text-gray-500 cursor-not-allowed' : 'btn-primary'}"
								disabled={regStatus.disabled}
							>
								{regStatus.text}
							</button>
							
							{#if isEventUpcoming(event)}
								<p class="text-sm text-gray-500 text-center">
									Registration closes 24 hours before the event
								</p>
							{/if}
						</div>
					</div>

					<!-- Event Info -->
					<div class="bg-gray-50 rounded-lg p-6">
						<h3 class="text-lg font-semibold text-gray-900 mb-4">Event Information</h3>
						
						<div class="space-y-3">
							<div class="flex justify-between">
								<span class="text-gray-600">Event Type</span>
								<span class="font-medium text-gray-900">
									{event.is_online ? 'Online' : 'In-Person'}
								</span>
							</div>
							
							<div class="flex justify-between">
								<span class="text-gray-600">Duration</span>
								<span class="font-medium text-gray-900">
									{Math.round((new Date(event.end_date).getTime() - new Date(event.start_date).getTime()) / (1000 * 60 * 60))} hours
								</span>
							</div>
							
							<div class="flex justify-between">
								<span class="text-gray-600">Created</span>
								<span class="font-medium text-gray-900">
									{formatDate(event.created_at)}
								</span>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</section>

	<!-- Related Events Section -->
	<section class="py-12 bg-gray-50">
		<div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
			<div class="text-center mb-8">
				<h2 class="text-2xl font-bold text-gray-900 mb-2">More Events</h2>
				<p class="text-gray-600">Discover other upcoming events in our alumni network</p>
			</div>
			
			<div class="text-center">
				<a href="/events" class="btn-primary">
					View All Events
				</a>
			</div>
		</div>
	</section>
{/if}
