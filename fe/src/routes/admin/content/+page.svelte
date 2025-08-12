<script lang="ts">
	import { onMount } from 'svelte';
	import { 
		Search, 
		Plus, 
		Edit, 
		Trash2, 
		Eye,
		FileText,
		Image,
		Video,
		Link,
		ChevronLeft,
		ChevronRight,
		Filter,
		Calendar,
		User
	} from 'lucide-svelte';
	
	let content: any[] = [];
	let filteredContent: any[] = [];
	let loading = true;
	let searchTerm = '';
	let selectedType = '';
	let selectedStatus = '';
	let currentPage = 1;
	let itemsPerPage = 10;
	let showDeleteModal = false;
	let contentToDelete: any = null;
	
	const contentTypes = [
		{ value: '', label: 'All Types' },
		{ value: 'news', label: 'News' },
		{ value: 'announcement', label: 'Announcement' },
		{ value: 'resource', label: 'Resource' },
		{ value: 'article', label: 'Article' }
	];
	
	const statusOptions = [
		{ value: '', label: 'All Status' },
		{ value: 'published', label: 'Published' },
		{ value: 'draft', label: 'Draft' },
		{ value: 'archived', label: 'Archived' }
	];
	
	onMount(() => {
		loadContent();
	});
	
	async function loadContent() {
		try {
			// Mock data for demo - replace with actual API call
			content = [
				{
					id: '1',
					title: 'Alumni Networking Event Success',
					content: 'Our recent networking event brought together over 200 alumni from various graduating classes...',
					type: 'news',
					status: 'published',
					author: 'Admin User',
					created_at: '2024-01-15T10:00:00Z',
					updated_at: '2024-01-15T10:00:00Z',
					featured_image: null,
					tags: ['networking', 'events', 'success']
				},
				{
					id: '2',
					title: 'New Career Development Resources Available',
					content: 'We are excited to announce the launch of our new career development portal...',
					type: 'announcement',
					status: 'published',
					author: 'Career Services',
					created_at: '2024-01-14T14:30:00Z',
					updated_at: '2024-01-14T14:30:00Z',
					featured_image: null,
					tags: ['career', 'resources', 'development']
				},
				{
					id: '3',
					title: 'Alumni Spotlight: John Smith',
					content: 'This month we feature John Smith, a 2015 graduate who has made significant contributions...',
					type: 'article',
					status: 'draft',
					author: 'Content Team',
					created_at: '2024-01-13T09:15:00Z',
					updated_at: '2024-01-13T09:15:00Z',
					featured_image: '/images/john-smith.jpg',
					tags: ['spotlight', 'alumni', 'success-story']
				},
				{
					id: '4',
					title: 'Mentorship Program Guidelines',
					content: 'Complete guidelines for participating in our alumni mentorship program...',
					type: 'resource',
					status: 'published',
					author: 'Program Director',
					created_at: '2024-01-12T16:45:00Z',
					updated_at: '2024-01-12T16:45:00Z',
					featured_image: null,
					tags: ['mentorship', 'guidelines', 'program']
				}
			];
			filteredContent = content;
		} catch (error) {
			console.error('Error loading content:', error);
		} finally {
			loading = false;
		}
	}
	
	function filterContent() {
		filteredContent = content.filter(item => {
			const matchesSearch = !searchTerm || 
				item.title.toLowerCase().includes(searchTerm.toLowerCase()) ||
				item.content.toLowerCase().includes(searchTerm.toLowerCase()) ||
				item.author.toLowerCase().includes(searchTerm.toLowerCase());
			
			const matchesType = !selectedType || item.type === selectedType;
			const matchesStatus = !selectedStatus || item.status === selectedStatus;
			
			return matchesSearch && matchesType && matchesStatus;
		});
		currentPage = 1;
	}
	
	function getPaginatedContent() {
		const start = (currentPage - 1) * itemsPerPage;
		const end = start + itemsPerPage;
		return filteredContent.slice(start, end);
	}
	
	function getTotalPages() {
		return Math.ceil(filteredContent.length / itemsPerPage);
	}
	
	function deleteContent(item: any) {
		contentToDelete = item;
		showDeleteModal = true;
	}
	
	async function confirmDelete() {
		if (!contentToDelete) return;
		
		try {
			// Mock delete - replace with actual API call
			content = content.filter(item => item.id !== contentToDelete.id);
			filterContent();
		} catch (error) {
			console.error('Error deleting content:', error);
		} finally {
			showDeleteModal = false;
			contentToDelete = null;
		}
	}
	
	function formatDate(dateString: string) {
		return new Date(dateString).toLocaleDateString('en-US', {
			year: 'numeric',
			month: 'short',
			day: 'numeric'
		});
	}
	
	function getContentIcon(type: string) {
		switch (type) {
			case 'news':
				return FileText;
			case 'announcement':
				return Calendar;
			case 'resource':
				return Link;
			case 'article':
				return Image;
			default:
				return FileText;
		}
	}
	
	function getStatusColor(status: string) {
		switch (status) {
			case 'published':
				return 'bg-green-100 text-green-800';
			case 'draft':
				return 'bg-yellow-100 text-yellow-800';
			case 'archived':
				return 'bg-gray-100 text-gray-800';
			default:
				return 'bg-gray-100 text-gray-800';
		}
	}
	
	$: {
		filterContent();
	}
</script>

<svelte:head>
	<title>Content Management - Admin CMS</title>
</svelte:head>

<!-- Page Header -->
<div class="mb-8">
	<div class="flex items-center justify-between">
		<div>
			<h1 class="text-3xl font-bold text-gray-900">Content Management</h1>
			<p class="text-gray-600 mt-2">Manage news, announcements, and resources</p>
		</div>
		<a href="/admin/content/new" class="btn-primary flex items-center">
			<Plus class="h-5 w-5 mr-2" />
			Create Content
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
		<div class="grid grid-cols-1 md:grid-cols-4 gap-4">
			<div class="relative">
				<Search class="absolute left-3 top-1/2 transform -translate-y-1/2 h-5 w-5 text-gray-400" />
				<input
					type="text"
					placeholder="Search content..."
					bind:value={searchTerm}
					class="input-field pl-10"
				/>
			</div>
			
			<select bind:value={selectedType} class="input-field">
				{#each contentTypes as type}
					<option value={type.value}>{type.label}</option>
				{/each}
			</select>
			
			<select bind:value={selectedStatus} class="input-field">
				{#each statusOptions as status}
					<option value={status.value}>{status.label}</option>
				{/each}
			</select>
			
			<button class="btn-secondary flex items-center justify-center">
				<Filter class="h-5 w-5 mr-2" />
				Advanced
			</button>
		</div>
	</div>

	<!-- Content Grid -->
	<div class="grid grid-cols-1 lg:grid-cols-2 xl:grid-cols-3 gap-6">
		{#each getPaginatedContent() as item}
			{@const IconComponent = getContentIcon(item.type)}
			<div class="card hover:shadow-lg transition-shadow duration-200">
				<div class="flex items-start justify-between mb-4">
					<div class="flex items-center">
						<div class="w-10 h-10 bg-primary-100 rounded-lg flex items-center justify-center">
							<svelte:component this={IconComponent} class="h-5 w-5 text-primary-600" />
						</div>
						<div class="ml-3">
							<span class="inline-flex px-2 py-1 text-xs font-semibold rounded-full bg-gray-100 text-gray-800 uppercase">
								{item.type}
							</span>
						</div>
					</div>
					<span class="inline-flex px-2 py-1 text-xs font-semibold rounded-full {getStatusColor(item.status)}">
						{item.status}
					</span>
				</div>
				
				<h3 class="text-lg font-semibold text-gray-900 mb-2 line-clamp-2">
					{item.title}
				</h3>
				
				<p class="text-sm text-gray-600 mb-4 line-clamp-3">
					{item.content}
				</p>
				
				{#if item.featured_image}
					<div class="mb-4">
						<img 
							src={item.featured_image} 
							alt={item.title}
							class="w-full h-32 object-cover rounded-lg"
						/>
					</div>
				{/if}
				
				<div class="flex items-center justify-between text-sm text-gray-500 mb-4">
					<div class="flex items-center">
						<User class="h-4 w-4 mr-1" />
						{item.author}
					</div>
					<div class="flex items-center">
						<Calendar class="h-4 w-4 mr-1" />
						{formatDate(item.created_at)}
					</div>
				</div>
				
				{#if item.tags && item.tags.length > 0}
					<div class="flex flex-wrap gap-1 mb-4">
						{#each item.tags.slice(0, 3) as tag}
							<span class="inline-flex px-2 py-1 text-xs bg-gray-100 text-gray-700 rounded-full">
								{tag}
							</span>
						{/each}
						{#if item.tags.length > 3}
							<span class="inline-flex px-2 py-1 text-xs bg-gray-100 text-gray-700 rounded-full">
								+{item.tags.length - 3}
							</span>
						{/if}
					</div>
				{/if}
				
				<div class="flex items-center justify-between pt-4 border-t border-gray-200">
					<div class="flex items-center space-x-2">
						<button class="text-blue-600 hover:text-blue-900 p-1">
							<Eye class="h-4 w-4" />
						</button>
						<a href="/admin/content/{item.id}/edit" class="text-green-600 hover:text-green-900 p-1">
							<Edit class="h-4 w-4" />
						</a>
						<button on:click={() => deleteContent(item)} class="text-red-600 hover:text-red-900 p-1">
							<Trash2 class="h-4 w-4" />
						</button>
					</div>
					<button class="text-sm text-primary-600 hover:text-primary-500 font-medium">
						Read More
					</button>
				</div>
			</div>
		{/each}
	</div>
	
	<!-- Pagination -->
	{#if getTotalPages() > 1}
		<div class="flex items-center justify-between mt-8">
			<div class="text-sm text-gray-700">
				Showing {((currentPage - 1) * itemsPerPage) + 1} to {Math.min(currentPage * itemsPerPage, filteredContent.length)} of {filteredContent.length} results
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
							<h3 class="text-lg leading-6 font-medium text-gray-900">Delete Content</h3>
							<div class="mt-2">
								<p class="text-sm text-gray-500">
									Are you sure you want to delete "{contentToDelete?.title}"? This action cannot be undone.
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
						on:click={() => { showDeleteModal = false; contentToDelete = null; }}
						class="mt-3 w-full inline-flex justify-center rounded-md border border-gray-300 shadow-sm px-4 py-2 bg-white text-base font-medium text-gray-700 hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-primary-500 sm:mt-0 sm:ml-3 sm:w-auto sm:text-sm"
					>
						Cancel
					</button>
				</div>
			</div>
		</div>
	</div>
{/if}

<style>
	.line-clamp-2 {
		display: -webkit-box;
		-webkit-line-clamp: 2;
		-webkit-box-orient: vertical;
		overflow: hidden;
	}
	
	.line-clamp-3 {
		display: -webkit-box;
		-webkit-line-clamp: 3;
		-webkit-box-orient: vertical;
		overflow: hidden;
	}
</style>
