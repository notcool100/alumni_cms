<script lang="ts">
	import { onMount } from 'svelte';
	import { apiService } from '$lib/api';
	import { 
		Search, 
		Plus, 
		Edit, 
		Trash2, 
		Eye,
		Calendar,
		MapPin,
		Users,
		ChevronLeft,
		ChevronRight,
		Filter
	} from 'lucide-svelte';
	
	let events: any[] = [];
	let filteredEvents: any[] = [];
	let loading = true;
	let searchTerm = '';
	let selectedStatus = '';
	let currentPage = 1;
	let itemsPerPage = 10;
	let showDeleteModal = false;
	let eventToDelete: any = null;
	
	const statusOptions = [
		{ value: '', label: 'All Events' },
		{ value: 'upcoming', label: 'Upcoming' },
		{ value: 'ongoing', label: 'Ongoing' },
		{ value: 'past', label: 'Past' }
	];
	
	onMount(() => {
		loadEvents();
	});
	
	async function loadEvents() {
		try {
			const response = await apiService.getEvents();
			if (response.success && response.data) {
				events = response.data;
				filteredEvents = response.data;
			}
		} catch (error) {
			console.error('Error loading events:', error);
		} finally {
			loading = false;
		}
	}
	
	function filterEvents() {
		filteredEvents = events.filter(event => {
			const matchesSearch = !searchTerm || 
				event.title.toLowerCase().includes(searchTerm.toLowerCase()) ||
				event.description.toLowerCase().includes(searchTerm.toLowerCase()) ||
				(event.location && event.location.toLowerCase().includes(searchTerm.toLowerCase()));
			
			const now = new Date();
			const startDate = new Date(event.start_date);
			const endDate = new Date(event.end_date);
			
			let matchesStatus = true;
			if (selectedStatus === 'upcoming') {
				matchesStatus = startDate > now;
			} else if (selectedStatus === 'ongoing') {
				matchesStatus = startDate <= now && endDate >= now;
			} else if (selectedStatus === 'past') {
				matchesStatus = endDate < now;
			}
			
			return matchesSearch && matchesStatus;
		});
		currentPage = 1;
	}
	
	function getPaginatedEvents() {
		const start = (currentPage - 1) * itemsPerPage;
		const end = start + itemsPerPage;
		return filteredEvents.slice(start, end);
	}
	
	function getTotalPages() {
		return Math.ceil(filteredEvents.length / itemsPerPage);
	}
	
	function deleteEvent(event: any) {
		eventToDelete = event;
		showDeleteModal = true;
	}
	
	async function confirmDelete() {
		if (!eventToDelete) return;
		
		try {
			await apiService.deleteEvent(eventToDelete.id);
			await loadEvents();
			filterEvents();
		} catch (error) {
			console.error('Error deleting event:', error);
		} finally {
			showDeleteModal = false;
			eventToDelete = null;
		}
	}
	
	function formatDate(dateString: string) {
		return new Date(dateString).toLocaleDateString('en-US', {
			year: 'numeric',
			month: 'short',
			day: 'numeric',
			hour: '2-digit',
			minute: '2-digit'
		});
	}
	
	function getEventStatus(event: any) {
		const now = new Date();
		const startDate = new Date(event.start_date);
		const endDate = new Date(event.end_date);
		
		if (startDate > now) {
			return { status: 'upcoming', label: 'Upcoming', color: 'bg-blue-100 text-blue-800' };
		} else if (startDate <= now && endDate >= now) {
			return { status: 'ongoing', label: 'Ongoing', color: 'bg-green-100 text-green-800' };
		} else {
			return { status: 'past', label: 'Past', color: 'bg-gray-100 text-gray-800' };
		}
	}
	
	$: {
		filterEvents();
	}
</script>

<svelte:head>
	<title>Event Management - Admin CMS</title>
</svelte:head>

<!-- Page Header -->
<div class="mb-8">
	<div class="flex items-center justify-between">
		<div>
			<h1 class="text-3xl font-bold text-gray-900">Event Management</h1>
			<p class="text-gray-600 mt-2">Manage events and networking opportunities</p>
		</div>
		<a href="/admin/events/new" class="btn-primary flex items-center">
			<Plus class="h-5 w-5 mr-2" />
			Create Event
		</a>
	</div>
</div>

{#if loading}
	<div class="flex items-center justify-center h-64">
		<div class="animate-spin rounded-full h-12 w-12 border-b-2 border-primary-600"></div>
	</div>
{:else}
	<!-- Filters and Search -->
	<div class="card mb-6">
		<div class="grid grid-cols-1 md:grid-cols-3 gap-4">
			<div class="relative">
				<Search class="absolute left-3 top-1/2 transform -translate-y-1/2 h-5 w-5 text-gray-400" />
				<input
					type="text"
					placeholder="Search events..."
					bind:value={searchTerm}
					class="input-field pl-10"
				/>
			</div>
			
			<select bind:value={selectedStatus} class="input-field">
				{#each statusOptions as option}
					<option value={option.value}>{option.label}</option>
				{/each}
			</select>
			
			<button class="btn-secondary flex items-center justify-center">
				<Filter class="h-5 w-5 mr-2" />
				Advanced Filters
			</button>
		</div>
	</div>

	<!-- Events Table -->
	<div class="card">
		<div class="overflow-x-auto">
			<table class="min-w-full divide-y divide-gray-200">
				<thead class="bg-gray-50">
					<tr>
						<th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
							Event
						</th>
						<th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
							Date & Time
						</th>
						<th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
							Location
						</th>
						<th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
							Attendance
						</th>
						<th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
							Status
						</th>
						<th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
							Actions
						</th>
					</tr>
				</thead>
				<tbody class="bg-white divide-y divide-gray-200">
					{#each getPaginatedEvents() as event}
						{@const eventStatus = getEventStatus(event)}
						<tr class="hover:bg-gray-50">
							<td class="px-6 py-4 whitespace-nowrap">
								<div class="flex items-center">
									<div class="w-10 h-10 bg-primary-600 rounded-lg flex items-center justify-center">
										<Calendar class="h-5 w-5 text-white" />
									</div>
									<div class="ml-4">
										<div class="text-sm font-medium text-gray-900">
											{event.title}
										</div>
										<div class="text-sm text-gray-500 max-w-xs truncate">
											{event.description}
										</div>
									</div>
								</div>
							</td>
							<td class="px-6 py-4 whitespace-nowrap">
								<div class="text-sm text-gray-900">{formatDate(event.start_date)}</div>
								<div class="text-sm text-gray-500">to {formatDate(event.end_date)}</div>
							</td>
							<td class="px-6 py-4 whitespace-nowrap">
								{#if event.is_online}
									<div class="flex items-center">
										<div class="w-2 h-2 bg-green-400 rounded-full mr-2"></div>
										<span class="text-sm text-gray-900">Online</span>
									</div>
									{#if event.meeting_url}
										<div class="text-sm text-gray-500 truncate max-w-xs">
											{event.meeting_url}
										</div>
									{/if}
								{:else if event.location}
									<div class="flex items-center">
										<MapPin class="h-4 w-4 text-gray-400 mr-1" />
										<span class="text-sm text-gray-900">{event.location}</span>
									</div>
								{:else}
									<span class="text-sm text-gray-400">TBD</span>
								{/if}
							</td>
							<td class="px-6 py-4 whitespace-nowrap">
								<div class="flex items-center">
									<Users class="h-4 w-4 text-gray-400 mr-1" />
									<span class="text-sm text-gray-900">{event.current_attendees}</span>
									{#if event.max_attendees}
										<span class="text-sm text-gray-500">/ {event.max_attendees}</span>
									{/if}
								</div>
								{#if event.max_attendees}
									<div class="w-full bg-gray-200 rounded-full h-2 mt-1">
										<div 
											class="bg-primary-600 h-2 rounded-full" 
											style="width: {(event.current_attendees / event.max_attendees) * 100}%"
										></div>
									</div>
								{/if}
							</td>
							<td class="px-6 py-4 whitespace-nowrap">
								<span class="inline-flex px-2 py-1 text-xs font-semibold rounded-full {eventStatus.color}">
									{eventStatus.label}
								</span>
							</td>
							<td class="px-6 py-4 whitespace-nowrap text-sm font-medium">
								<div class="flex items-center space-x-2">
									<button class="text-blue-600 hover:text-blue-900">
										<Eye class="h-4 w-4" />
									</button>
									<a href="/admin/events/{event.id}/edit" class="text-green-600 hover:text-green-900">
										<Edit class="h-4 w-4" />
									</a>
									<button on:click={() => deleteEvent(event)} class="text-red-600 hover:text-red-900">
										<Trash2 class="h-4 w-4" />
									</button>
								</div>
							</td>
						</tr>
					{/each}
				</tbody>
			</table>
		</div>
		
		<!-- Pagination -->
		{#if getTotalPages() > 1}
			<div class="flex items-center justify-between px-6 py-4 border-t border-gray-200">
				<div class="text-sm text-gray-700">
					Showing {((currentPage - 1) * itemsPerPage) + 1} to {Math.min(currentPage * itemsPerPage, filteredEvents.length)} of {filteredEvents.length} results
				</div>
				<div class="flex items-center space-x-2">
					<button 
						on:click={() => currentPage = Math.max(1, currentPage - 1)}
						disabled={currentPage === 1}
						class="p-2 text-gray-400 hover:text-gray-600 disabled:opacity-50 disabled:cursor-not-allowed"
					>
						<ChevronLeft class="h-5 w-5" />
					</button>
					
					{#each Array.from({ length: getTotalPages() }, (_, i) => i + 1) as page}
						<button 
							on:click={() => currentPage = page}
							class="px-3 py-2 text-sm font-medium rounded-lg {currentPage === page ? 'bg-primary-600 text-white' : 'text-gray-700 hover:bg-gray-100'}"
						>
							{page}
						</button>
					{/each}
					
					<button 
						on:click={() => currentPage = Math.min(getTotalPages(), currentPage + 1)}
						disabled={currentPage === getTotalPages()}
						class="p-2 text-gray-400 hover:text-gray-600 disabled:opacity-50 disabled:cursor-not-allowed"
					>
						<ChevronRight class="h-5 w-5" />
					</button>
				</div>
			</div>
		{/if}
	</div>
{/if}

<!-- Delete Confirmation Modal -->
{#if showDeleteModal}
	<div class="fixed inset-0 z-50 overflow-y-auto">
		<div class="flex items-center justify-center min-h-screen pt-4 px-4 pb-20 text-center sm:block sm:p-0">
			<div class="fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity"></div>
			
			<div class="inline-block align-bottom bg-white rounded-lg text-left overflow-hidden shadow-xl transform transition-all sm:my-8 sm:align-middle sm:max-w-lg sm:w-full">
				<div class="bg-white px-4 pt-5 pb-4 sm:p-6 sm:pb-4">
					<div class="sm:flex sm:items-start">
						<div class="mx-auto flex-shrink-0 flex items-center justify-center h-12 w-12 rounded-full bg-red-100 sm:mx-0 sm:h-10 sm:w-10">
							<Trash2 class="h-6 w-6 text-red-600" />
						</div>
						<div class="mt-3 text-center sm:mt-0 sm:ml-4 sm:text-left">
							<h3 class="text-lg leading-6 font-medium text-gray-900">Delete Event</h3>
							<div class="mt-2">
								<p class="text-sm text-gray-500">
									Are you sure you want to delete "{eventToDelete?.title}"? This action cannot be undone.
								</p>
							</div>
						</div>
					</div>
				</div>
				<div class="bg-gray-50 px-4 py-3 sm:px-6 sm:flex sm:flex-row-reverse">
					<button 
						on:click={confirmDelete}
						class="w-full inline-flex justify-center rounded-md border border-transparent shadow-sm px-4 py-2 bg-red-600 text-base font-medium text-white hover:bg-red-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-red-500 sm:ml-3 sm:w-auto sm:text-sm"
					>
						Delete
					</button>
					<button 
						on:click={() => { showDeleteModal = false; eventToDelete = null; }}
						class="mt-3 w-full inline-flex justify-center rounded-md border border-gray-300 shadow-sm px-4 py-2 bg-white text-base font-medium text-gray-700 hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-primary-500 sm:mt-0 sm:ml-3 sm:w-auto sm:text-sm"
					>
						Cancel
					</button>
				</div>
			</div>
		</div>
	</div>
{/if}
