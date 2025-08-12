<script lang="ts">
	import { onMount } from 'svelte';
	import { goto } from '$app/navigation';
	import { apiService } from '$lib/api';
	import { 
		Search, 
		Plus, 
		Filter, 
		MoreHorizontal, 
		Edit, 
		Trash2, 
		Eye,
		Download,
		ChevronLeft,
		ChevronRight
	} from 'lucide-svelte';
	
	let alumni: any[] = [];
	let filteredAlumni: any[] = [];
	let loading = true;
	let searchTerm = '';
	let selectedYear = '';
	let selectedDegree = '';
	let currentPage = 1;
	let itemsPerPage = 10;
	let showDeleteModal = false;
	let alumniToDelete: any = null;
	
	const graduationYears = Array.from({ length: 20 }, (_, i) => new Date().getFullYear() - i);
	const degrees = ['Bachelor', 'Master', 'PhD', 'Associate', 'Certificate'];
	
	onMount(() => {
		loadAlumni();
	});
	
	async function loadAlumni() {
		try {
			const response = await apiService.getAlumni();
			if (response.success && response.data) {
				alumni = response.data;
				filteredAlumni = response.data;
			}
		} catch (error) {
			console.error('Error loading alumni:', error);
		} finally {
			loading = false;
		}
	}
	
	function filterAlumni() {
		filteredAlumni = alumni.filter(alum => {
			const matchesSearch = !searchTerm || 
				alum.graduation_year.toString().includes(searchTerm) ||
				alum.degree.toLowerCase().includes(searchTerm.toLowerCase()) ||
				alum.major.toLowerCase().includes(searchTerm.toLowerCase()) ||
				(alum.current_company && alum.current_company.toLowerCase().includes(searchTerm.toLowerCase()));
			
			const matchesYear = !selectedYear || alum.graduation_year.toString() === selectedYear;
			const matchesDegree = !selectedDegree || alum.degree === selectedDegree;
			
			return matchesSearch && matchesYear && matchesDegree;
		});
		currentPage = 1;
	}
	
	function getPaginatedAlumni() {
		const start = (currentPage - 1) * itemsPerPage;
		const end = start + itemsPerPage;
		return filteredAlumni.slice(start, end);
	}
	
	function getTotalPages() {
		return Math.ceil(filteredAlumni.length / itemsPerPage);
	}
	
	function deleteAlumni(alum: any) {
		alumniToDelete = alum;
		showDeleteModal = true;
	}
	
	async function confirmDelete() {
		if (!alumniToDelete) return;
		
		try {
			await apiService.deleteAlumni(alumniToDelete.id);
			await loadAlumni();
			filterAlumni();
		} catch (error) {
			console.error('Error deleting alumni:', error);
		} finally {
			showDeleteModal = false;
			alumniToDelete = null;
		}
	}
	
	function formatDate(dateString: string) {
		return new Date(dateString).toLocaleDateString('en-US', {
			year: 'numeric',
			month: 'short',
			day: 'numeric'
		});
	}
	
	$: {
		filterAlumni();
	}
</script>

<svelte:head>
	<title>Alumni Management - Admin CMS</title>
</svelte:head>

<!-- Page Header -->
<div class="mb-8">
	<div class="flex items-center justify-between">
		<div>
			<h1 class="text-3xl font-bold text-gray-900">Alumni Management</h1>
			<p class="text-gray-600 mt-2">Manage alumni profiles and information</p>
		</div>
		<a href="/admin/alumni/new" class="btn-primary flex items-center">
			<Plus class="h-5 w-5 mr-2" />
			Add Alumni
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
					placeholder="Search alumni..."
					bind:value={searchTerm}
					class="input-field pl-10"
				/>
			</div>
			
			<select bind:value={selectedYear} class="input-field">
				<option value="">All Years</option>
				{#each graduationYears as year}
					<option value={year}>{year}</option>
				{/each}
			</select>
			
			<select bind:value={selectedDegree} class="input-field">
				<option value="">All Degrees</option>
				{#each degrees as degree}
					<option value={degree}>{degree}</option>
				{/each}
			</select>
			
			<button class="btn-secondary flex items-center justify-center">
				<Download class="h-5 w-5 mr-2" />
				Export
			</button>
		</div>
	</div>

	<!-- Alumni Table -->
	<div class="card">
		<div class="overflow-x-auto">
			<table class="min-w-full divide-y divide-gray-200">
				<thead class="bg-gray-50">
					<tr>
						<th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
							Alumni
						</th>
						<th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
							Graduation
						</th>
						<th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
							Current Position
						</th>
						<th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
							Location
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
					{#each getPaginatedAlumni() as alum}
						<tr class="hover:bg-gray-50">
							<td class="px-6 py-4 whitespace-nowrap">
								<div class="flex items-center">
									<div class="w-10 h-10 bg-primary-600 rounded-full flex items-center justify-center">
										<span class="text-white font-medium">
											{alum.graduation_year.toString().slice(-2)}
										</span>
									</div>
									<div class="ml-4">
										<div class="text-sm font-medium text-gray-900">
											{alum.graduation_year} Graduate
										</div>
										<div class="text-sm text-gray-500">
											{alum.degree} in {alum.major}
										</div>
									</div>
								</div>
							</td>
							<td class="px-6 py-4 whitespace-nowrap">
								<div class="text-sm text-gray-900">{alum.graduation_year}</div>
								<div class="text-sm text-gray-500">{alum.degree}</div>
							</td>
							<td class="px-6 py-4 whitespace-nowrap">
								{#if alum.current_position && alum.current_company}
									<div class="text-sm text-gray-900">{alum.current_position}</div>
									<div class="text-sm text-gray-500">{alum.current_company}</div>
								{:else}
									<span class="text-sm text-gray-400">Not specified</span>
								{/if}
							</td>
							<td class="px-6 py-4 whitespace-nowrap">
								{#if alum.location}
									<span class="text-sm text-gray-900">{alum.location}</span>
								{:else}
									<span class="text-sm text-gray-400">Not specified</span>
								{/if}
							</td>
							<td class="px-6 py-4 whitespace-nowrap">
								<span class="inline-flex px-2 py-1 text-xs font-semibold rounded-full {alum.is_public ? 'bg-green-100 text-green-800' : 'bg-gray-100 text-gray-800'}">
									{alum.is_public ? 'Public' : 'Private'}
								</span>
							</td>
							<td class="px-6 py-4 whitespace-nowrap text-sm font-medium">
								<div class="flex items-center space-x-2">
									<button class="text-blue-600 hover:text-blue-900">
										<Eye class="h-4 w-4" />
									</button>
									<a href="/admin/alumni/{alum.id}/edit" class="text-green-600 hover:text-green-900">
										<Edit class="h-4 w-4" />
									</a>
									<button on:click={() => deleteAlumni(alum)} class="text-red-600 hover:text-red-900">
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
					Showing {((currentPage - 1) * itemsPerPage) + 1} to {Math.min(currentPage * itemsPerPage, filteredAlumni.length)} of {filteredAlumni.length} results
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
							<h3 class="text-lg leading-6 font-medium text-gray-900">Delete Alumni</h3>
							<div class="mt-2">
								<p class="text-sm text-gray-500">
									Are you sure you want to delete this alumni profile? This action cannot be undone.
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
						on:click={() => { showDeleteModal = false; alumniToDelete = null; }}
						class="mt-3 w-full inline-flex justify-center rounded-md border border-gray-300 shadow-sm px-4 py-2 bg-white text-base font-medium text-gray-700 hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-primary-500 sm:mt-0 sm:ml-3 sm:w-auto sm:text-sm"
					>
						Cancel
					</button>
				</div>
			</div>
		</div>
	</div>
{/if}
