<script lang="ts">
	import { onMount } from 'svelte';
	import { apiService } from '$lib/api';
	import { Search, Plus, Filter, MapPin, Building, GraduationCap } from 'lucide-svelte';
	
	let alumni: any[] = [];
	let loading = true;
	let searchTerm = '';
	let selectedYear = '';
	let selectedMajor = '';
	
	onMount(() => {
		loadAlumni();
	});
	
	async function loadAlumni() {
		try {
			const response = await apiService.getAlumni();
			if (response.success && response.data) {
				alumni = response.data;
			}
		} catch (error) {
			console.error('Error loading alumni:', error);
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
	
	$: filteredAlumni = alumni.filter(person => {
		const matchesSearch = !searchTerm || 
			person.graduation_year?.toString().includes(searchTerm) ||
			person.major?.toLowerCase().includes(searchTerm.toLowerCase()) ||
			person.degree?.toLowerCase().includes(searchTerm.toLowerCase()) ||
			person.current_company?.toLowerCase().includes(searchTerm.toLowerCase());
		
		const matchesYear = !selectedYear || person.graduation_year?.toString() === selectedYear;
		const matchesMajor = !selectedMajor || person.major === selectedMajor;
		
		return matchesSearch && matchesYear && matchesMajor;
	});
	
	$: uniqueYears = [...new Set(alumni.map(p => p.graduation_year?.toString()).filter(Boolean))].sort((a, b) => b - a);
	$: uniqueMajors = [...new Set(alumni.map(p => p.major).filter(Boolean))].sort();
</script>

<svelte:head>
	<title>Alumni Directory - Alumni Network</title>
</svelte:head>

<div class="space-y-6">
	<!-- Header -->
	<div class="flex items-center justify-between">
		<div>
			<h1 class="text-2xl font-bold text-gray-900">Alumni Directory</h1>
			<p class="text-gray-600 mt-1">Connect with fellow alumni from your institution</p>
		</div>
		<button class="btn-primary flex items-center">
			<Plus class="h-4 w-4 mr-2" />
			Add Alumni
		</button>
	</div>

	<!-- Filters -->
	<div class="card">
		<div class="flex flex-col sm:flex-row gap-4">
			<div class="flex-1">
				<div class="relative">
					<Search class="absolute left-3 top-1/2 transform -translate-y-1/2 h-4 w-4 text-gray-400" />
					<input
						type="text"
						bind:value={searchTerm}
						placeholder="Search by year, major, degree, or company..."
						class="input-field pl-10"
					/>
				</div>
			</div>
			<select bind:value={selectedYear} class="input-field">
				<option value="">All Years</option>
				{#each uniqueYears as year}
					<option value={year}>{year}</option>
				{/each}
			</select>
			<select bind:value={selectedMajor} class="input-field">
				<option value="">All Majors</option>
				{#each uniqueMajors as major}
					<option value={major}>{major}</option>
				{/each}
			</select>
		</div>
	</div>

	<!-- Results -->
	{#if loading}
		<div class="flex items-center justify-center py-12">
			<div class="animate-spin rounded-full h-32 w-32 border-b-2 border-primary-600"></div>
		</div>
	{:else if filteredAlumni.length === 0}
		<div class="card text-center py-12">
			<GraduationCap class="h-12 w-12 text-gray-400 mx-auto mb-4" />
			<h3 class="text-lg font-medium text-gray-900 mb-2">No alumni found</h3>
			<p class="text-gray-600">Try adjusting your search criteria or filters.</p>
		</div>
	{:else}
		<div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
			{#each filteredAlumni as person}
				<div class="card hover:shadow-lg transition-shadow duration-200">
					<div class="flex items-start space-x-4">
						<div class="w-16 h-16 bg-primary-600 rounded-full flex items-center justify-center flex-shrink-0">
							<span class="text-white font-bold text-lg">
								{person.graduation_year?.toString().slice(-2)}
							</span>
						</div>
						<div class="flex-1 min-w-0">
							<h3 class="text-lg font-semibold text-gray-900 mb-1">
								{person.graduation_year} Graduate
							</h3>
							<div class="space-y-1 text-sm text-gray-600">
								<div class="flex items-center">
									<GraduationCap class="h-4 w-4 mr-2" />
									{person.degree} in {person.major}
								</div>
								{#if person.current_company}
									<div class="flex items-center">
										<Building class="h-4 w-4 mr-2" />
										{person.current_position} at {person.current_company}
									</div>
								{/if}
								{#if person.location}
									<div class="flex items-center">
										<MapPin class="h-4 w-4 mr-2" />
										{person.location}
									</div>
								{/if}
							</div>
						</div>
					</div>
					<div class="mt-4 pt-4 border-t border-gray-200">
						<button class="text-primary-600 hover:text-primary-500 text-sm font-medium">
							View Profile
						</button>
					</div>
				</div>
			{/each}
		</div>
		
		<!-- Results Summary -->
		<div class="text-center text-sm text-gray-600 mt-6">
			Showing {filteredAlumni.length} of {alumni.length} alumni
		</div>
	{/if}
</div>

