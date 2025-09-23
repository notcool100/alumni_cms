<script lang="ts">
	import { onMount } from 'svelte';
	import { goto } from '$app/navigation';
	import { apiService } from '$lib/api';
	import { requireNavigationAccess } from '$lib/utils/routeGuards';
	import { 
		Save, 
		X, 
		FileText, 
		Image,
		Tag,
		Eye,
		EyeOff
	} from 'lucide-svelte';
	
	let loading = false;
	let saving = false;
	let formData = {
		title: '',
		content: '',
		type: 'news',
		status: 'draft',
		tags: '',
		featured_image: '',
		meta_description: ''
	};
	
	const contentTypes = [
		{ value: 'news', label: 'News' },
		{ value: 'announcement', label: 'Announcement' },
		{ value: 'resource', label: 'Resource' },
		{ value: 'article', label: 'Article' }
	];
	
	const statusOptions = [
		{ value: 'draft', label: 'Draft' },
		{ value: 'published', label: 'Published' },
		{ value: 'archived', label: 'Archived' }
	];
	
	onMount(() => {
		// Use the route guard for consistent access control
		if (!requireNavigationAccess()) {
			return;
		}
	});
	
	async function handleSubmit() {
		saving = true;
		
		try {
			const contentData = {
				...formData,
				tags: formData.tags.split(',').map(tag => tag.trim()).filter(tag => tag.length > 0)
			};
			
			// Mock API call - replace with actual API
			console.log('Creating content:', contentData);
			await new Promise(resolve => setTimeout(resolve, 1000));
			
			goto('/admin/content');
		} catch (error) {
			console.error('Error creating content:', error);
			alert('Error creating content. Please try again.');
		} finally {
			saving = false;
		}
	}
	
	function handleCancel() {
		goto('/admin/content');
	}
	
	function handleImageUpload(event: Event) {
		const input = event.target as HTMLInputElement;
		if (input.files && input.files[0]) {
			const file = input.files[0];
			// Mock file upload - replace with actual upload logic
			formData.featured_image = URL.createObjectURL(file);
		}
	}
</script>

<svelte:head>
	<title>Create New Content - Admin CMS</title>
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
				<h1 class="text-3xl font-bold text-gray-900">Create New Content</h1>
				<p class="text-gray-600 mt-2">Add news, announcements, or resources</p>
			</div>
			<div class="flex items-center space-x-3">
				<button on:click={handleCancel} class="btn-secondary flex items-center">
					<X class="h-5 w-5 mr-2" />
					Cancel
				</button>
				<button on:click={handleSubmit} disabled={saving} class="btn-primary flex items-center">
					<Save class="h-5 w-5 mr-2" />
					{saving ? 'Saving...' : 'Publish Content'}
				</button>
			</div>
		</div>
	</div>

	<!-- Form -->
	<form on:submit|preventDefault={handleSubmit} class="space-y-8">
		<!-- Basic Information -->
		<div class="card">
			<h2 class="text-lg font-semibold text-gray-900 mb-6 flex items-center">
				<FileText class="h-5 w-5 mr-2" />
				Content Information
			</h2>
			
			<div class="space-y-6">
				<div>
					<label for="title" class="form-label">Content Title</label>
					<input
						id="title"
						type="text"
						bind:value={formData.title}
						class="input-field"
						placeholder="Enter content title"
						required
					/>
				</div>
				
				<div class="grid grid-cols-1 md:grid-cols-2 gap-6">
					<div>
						<label for="type" class="form-label">Content Type</label>
						<select id="type" bind:value={formData.type} class="input-field" required>
							{#each contentTypes as type}
								<option value={type.value}>{type.label}</option>
							{/each}
						</select>
					</div>
					
					<div>
						<label for="status" class="form-label">Status</label>
						<select id="status" bind:value={formData.status} class="input-field" required>
							{#each statusOptions as status}
								<option value={status.value}>{status.label}</option>
							{/each}
						</select>
					</div>
				</div>
				
				<div>
					<label for="meta_description" class="form-label">Meta Description</label>
					<textarea
						id="meta_description"
						bind:value={formData.meta_description}
						rows="2"
						class="input-field"
						placeholder="Brief description for SEO..."
						maxlength="160"
					></textarea>
					<p class="text-sm text-gray-500 mt-1">Keep under 160 characters for optimal SEO</p>
				</div>
			</div>
		</div>

		<!-- Content Editor -->
		<div class="card">
			<h2 class="text-lg font-semibold text-gray-900 mb-6">Content</h2>
			
			<div>
				<label for="content" class="form-label">Content Body</label>
				<textarea
					id="content"
					bind:value={formData.content}
					rows="12"
					class="input-field"
					placeholder="Write your content here... You can use basic HTML tags like &lt;strong&gt;, &lt;em&gt;, &lt;a&gt;, etc."
					required
				></textarea>
				<p class="text-sm text-gray-500 mt-1">Supports basic HTML formatting</p>
			</div>
		</div>

		<!-- Media and Tags -->
		<div class="card">
			<h2 class="text-lg font-semibold text-gray-900 mb-6 flex items-center">
				<Image class="h-5 w-5 mr-2" />
				Media & Tags
			</h2>
			
			<div class="space-y-6">
				<div>
					<label for="featured_image" class="form-label">Featured Image</label>
					<div class="mt-1 flex justify-center px-6 pt-5 pb-6 border-2 border-gray-300 border-dashed rounded-lg">
						<div class="space-y-1 text-center">
							<Image class="mx-auto h-12 w-12 text-gray-400" />
							<div class="flex text-sm text-gray-600">
								<label for="file-upload" class="relative cursor-pointer bg-white rounded-md font-medium text-primary-600 hover:text-primary-500 focus-within:outline-none focus-within:ring-2 focus-within:ring-offset-2 focus-within:ring-primary-500">
									<span>Upload a file</span>
									<input 
										id="file-upload" 
										name="file-upload" 
										type="file" 
										class="sr-only"
										accept="image/*"
										on:change={handleImageUpload}
									/>
								</label>
								<p class="pl-1">or drag and drop</p>
							</div>
							<p class="text-xs text-gray-500">PNG, JPG, GIF up to 10MB</p>
						</div>
					</div>
					{#if formData.featured_image}
						<div class="mt-4">
							<img src={formData.featured_image} alt="Preview" class="w-32 h-32 object-cover rounded-lg" />
						</div>
					{/if}
				</div>
				
				<div>
					<label for="tags" class="form-label">Tags</label>
					<div class="relative">
						<Tag class="absolute left-3 top-1/2 transform -translate-y-1/2 h-5 w-5 text-gray-400" />
						<input
							id="tags"
							type="text"
							bind:value={formData.tags}
							class="input-field pl-10"
							placeholder="tag1, tag2, tag3"
						/>
					</div>
					<p class="text-sm text-gray-500 mt-1">Separate tags with commas</p>
				</div>
			</div>
		</div>

		<!-- Preview -->
		<div class="card">
			<h2 class="text-lg font-semibold text-gray-900 mb-6 flex items-center">
				<Eye class="h-5 w-5 mr-2" />
				Preview
			</h2>
			
			<div class="border border-gray-200 rounded-lg p-6 bg-gray-50">
				{#if formData.title}
					<h3 class="text-xl font-bold text-gray-900 mb-4">{formData.title}</h3>
				{/if}
				
				{#if formData.meta_description}
					<p class="text-gray-600 mb-4">{formData.meta_description}</p>
				{/if}
				
				{#if formData.content}
					<div class="prose max-w-none">
						{@html formData.content}
					</div>
				{:else}
					<p class="text-gray-500 italic">Content preview will appear here...</p>
				{/if}
			</div>
		</div>

		<!-- Form Actions -->
		<div class="flex items-center justify-end space-x-3 pt-6 border-t border-gray-200">
			<button type="button" on:click={handleCancel} class="btn-secondary">
				Cancel
			</button>
			<button type="submit" disabled={saving} class="btn-primary">
				{saving ? 'Saving...' : 'Publish Content'}
			</button>
		</div>
	</form>
{/if}
