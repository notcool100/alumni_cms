<script lang="ts">
	import { onMount } from 'svelte';
	import { goto } from '$app/navigation';
	import { apiService } from '$lib/api';
	import { 
		Save, 
		X, 
		Calendar, 
		MapPin, 
		Users,
		Globe,
		Clock
	} from 'lucide-svelte';
	
	let loading = false;
	let saving = false;
	let formData = {
		title: '',
		description: '',
		location: '',
		start_date: '',
		end_date: '',
		max_attendees: '',
		is_online: false,
		meeting_url: ''
	};
	
	onMount(() => {
		// Check if user is authenticated and is admin
		if (!apiService.isAuthenticated()) {
			goto('/auth/login');
			return;
		}
		
		const user = apiService.getCurrentUserFromStorage();
		if (user?.role !== 'admin') {
			goto('/dashboard');
			return;
		}
		
		// Set default dates
		const now = new Date();
		const tomorrow = new Date(now);
		tomorrow.setDate(tomorrow.getDate() + 1);
		
		formData.start_date = tomorrow.toISOString().slice(0, 16);
		formData.end_date = tomorrow.toISOString().slice(0, 16);
	});
	
	async function handleSubmit() {
		saving = true;
		
		try {
			const eventData = {
				...formData,
				max_attendees: formData.max_attendees ? parseInt(formData.max_attendees) : undefined
			};
			
			const response = await apiService.createEvent(eventData);
			if (response.success) {
				goto('/admin/events');
			} else {
				alert('Error creating event: ' + response.message);
			}
		} catch (error) {
			console.error('Error creating event:', error);
			alert('Error creating event. Please try again.');
		} finally {
			saving = false;
		}
	}
	
	function handleCancel() {
		goto('/admin/events');
	}
	
	function updateEndDate() {
		if (formData.start_date && !formData.end_date) {
			const startDate = new Date(formData.start_date);
			formData.end_date = startDate.toISOString().slice(0, 16);
		}
	}
</script>

<svelte:head>
	<title>Create New Event - Admin CMS</title>
</svelte:head>

{#if loading}
	<div class="flex items-center justify-center h-64">
		<div class="animate-spin rounded-full h-12 w-12 border-b-2 border-primary-600"></div>
	</div>
{:else}
	<!-- Page Header -->
	<div class="mb-8">
		<div class="flex items-center justify-between">
			<div>
				<h1 class="text-3xl font-bold text-gray-900">Create New Event</h1>
				<p class="text-gray-600 mt-2">Add a new event to the alumni network</p>
			</div>
			<div class="flex items-center space-x-3">
				<button on:click={handleCancel} class="btn-secondary flex items-center">
					<X class="h-5 w-5 mr-2" />
					Cancel
				</button>
				<button on:click={handleSubmit} disabled={saving} class="btn-primary flex items-center">
					<Save class="h-5 w-5 mr-2" />
					{saving ? 'Saving...' : 'Create Event'}
				</button>
			</div>
		</div>
	</div>

	<!-- Form -->
	<form on:submit|preventDefault={handleSubmit} class="space-y-8">
		<!-- Basic Information -->
		<div class="card">
			<h2 class="text-lg font-semibold text-gray-900 mb-6 flex items-center">
				<Calendar class="h-5 w-5 mr-2" />
				Event Information
			</h2>
			
			<div class="space-y-6">
				<div>
					<label for="title" class="form-label">Event Title</label>
					<input
						id="title"
						type="text"
						bind:value={formData.title}
						class="input-field"
						placeholder="Enter event title"
						required
					/>
				</div>
				
				<div>
					<label for="description" class="form-label">Event Description</label>
					<textarea
						id="description"
						bind:value={formData.description}
						rows="4"
						class="input-field"
						placeholder="Provide a detailed description of the event..."
						required
					></textarea>
				</div>
			</div>
		</div>

		<!-- Date and Time -->
		<div class="card">
			<h2 class="text-lg font-semibold text-gray-900 mb-6 flex items-center">
				<Clock class="h-5 w-5 mr-2" />
				Date and Time
			</h2>
			
			<div class="grid grid-cols-1 md:grid-cols-2 gap-6">
				<div>
					<label for="start_date" class="form-label">Start Date & Time</label>
					<input
						id="start_date"
						type="datetime-local"
						bind:value={formData.start_date}
						on:change={updateEndDate}
						class="input-field"
						required
					/>
				</div>
				
				<div>
					<label for="end_date" class="form-label">End Date & Time</label>
					<input
						id="end_date"
						type="datetime-local"
						bind:value={formData.end_date}
						class="input-field"
						required
					/>
				</div>
			</div>
		</div>

		<!-- Location and Attendance -->
		<div class="card">
			<h2 class="text-lg font-semibold text-gray-900 mb-6 flex items-center">
				<MapPin class="h-5 w-5 mr-2" />
				Location & Attendance
			</h2>
			
			<div class="space-y-6">
				<div class="flex items-center">
					<input
						id="is_online"
						type="checkbox"
						bind:checked={formData.is_online}
						class="h-4 w-4 text-primary-600 focus:ring-primary-500 border-gray-300 rounded"
					/>
					<label for="is_online" class="ml-2 block text-sm text-gray-900">
						This is an online event
					</label>
				</div>
				
				{#if formData.is_online}
					<div>
						<label for="meeting_url" class="form-label">Meeting URL</label>
						<div class="relative">
							<Globe class="absolute left-3 top-1/2 transform -translate-y-1/2 h-5 w-5 text-gray-400" />
							<input
								id="meeting_url"
								type="url"
								bind:value={formData.meeting_url}
								class="input-field pl-10"
								placeholder="https://zoom.us/j/123456789"
							/>
						</div>
						<p class="text-sm text-gray-500 mt-1">Provide the meeting link for online events</p>
					</div>
				{:else}
					<div>
						<label for="location" class="form-label">Event Location</label>
						<div class="relative">
							<MapPin class="absolute left-3 top-1/2 transform -translate-y-1/2 h-5 w-5 text-gray-400" />
							<input
								id="location"
								type="text"
								bind:value={formData.location}
								class="input-field pl-10"
								placeholder="Enter venue address or location"
							/>
						</div>
					</div>
				{/if}
				
				<div>
					<label for="max_attendees" class="form-label">Maximum Attendees</label>
					<div class="relative">
						<Users class="absolute left-3 top-1/2 transform -translate-y-1/2 h-5 w-5 text-gray-400" />
						<input
							id="max_attendees"
							type="number"
							bind:value={formData.max_attendees}
							class="input-field pl-10"
							placeholder="Leave empty for unlimited"
							min="1"
						/>
					</div>
					<p class="text-sm text-gray-500 mt-1">Leave empty if there's no limit on attendees</p>
				</div>
			</div>
		</div>

		<!-- Form Actions -->
		<div class="flex items-center justify-end space-x-3 pt-6 border-t border-gray-200">
			<button type="button" on:click={handleCancel} class="btn-secondary">
				Cancel
			</button>
			<button type="submit" disabled={saving} class="btn-primary">
				{saving ? 'Saving...' : 'Create Event'}
			</button>
		</div>
	</form>
{/if}
