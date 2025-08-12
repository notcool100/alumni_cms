<script lang="ts">
	import { onMount } from 'svelte';
	import { apiService, type Alumni } from '$lib/api';
	import { Search, MapPin, Building, GraduationCap, ExternalLink, Filter } from 'lucide-svelte';

	let alumni: Alumni[] = [];
	let filteredAlumni: Alumni[] = [];
	let loading = true;
	let error = '';
	
	// Search and filter state
	let searchTerm = '';
	let selectedYear = '';
	let selectedMajor = '';
	let selectedLocation = '';
	
	// Get unique values for filters
	$: graduationYears = [...new Set(alumni.map(a => a.graduation_year))].sort((a, b) => b - a);
	$: majors = [...new Set(alumni.map(a => a.major).filter(Boolean))].sort();
	$: locations = [...new Set(alumni.map(a => a.location).filter(Boolean))].sort();

	onMount(async () => {
		try {
			const response = await apiService.getAlumni();
			if (response.success && response.data) {
				alumni = response.data.filter(a => a.is_public);
				filteredAlumni = alumni;
			}
		} catch (err) {
			error = 'Failed to load alumni directory';
			console.error('Error loading alumni:', err);
		} finally {
			loading = false;
		}
	});

	// Filter alumni based on search and filters
	$: {
		filteredAlumni = alumni.filter(alum => {
			const matchesSearch = !searchTerm || 
				alum.current_company?.toLowerCase().includes(searchTerm.toLowerCase()) ||
				alum.current_position?.toLowerCase().includes(searchTerm.toLowerCase()) ||
				alum.major?.toLowerCase().includes(searchTerm.toLowerCase()) ||
				alum.location?.toLowerCase().includes(searchTerm.toLowerCase());
			
			const matchesYear = !selectedYear || alum.graduation_year.toString() === selectedYear;
			const matchesMajor = !selectedMajor || alum.major === selectedMajor;
			const matchesLocation = !selectedLocation || alum.location === selectedLocation;
			
			return matchesSearch && matchesYear && matchesMajor && matchesLocation;
		});
	}

	function clearFilters() {
		searchTerm = '';
		selectedYear = '';
		selectedMajor = '';
		selectedLocation = '';
	}
</script>

<svelte:head>
	<title>Alumni Directory - Alumni Network</title>
	<meta name="description" content="Browse our alumni directory to connect with graduates from various years, majors, and locations." />
</svelte:head>

<!-- Hero Section -->
<section class="bg-gradient-to-br from-primary-600 to-primary-800 text-white py-16">
	<div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
		<div class="text-center">
			<h1 class="text-4xl md:text-5xl font-bold mb-4">Alumni Directory</h1>
			<p class="text-xl text-primary-100 max-w-3xl mx-auto">
				Discover and connect with fellow alumni from around the world. Find mentors, collaborators, and friends in your field.
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
					placeholder="Search by company, position, major, or location..."
					bind:value={searchTerm}
					class="w-full pl-10 pr-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-primary-500 focus:border-primary-500"
				/>
			</div>

			<!-- Filters -->
			<div class="grid grid-cols-1 md:grid-cols-4 gap-4">
				<div>
					<label for="year-filter" class="block text-sm font-medium text-gray-700 mb-2">Graduation Year</label>
					<select
						id="year-filter"
						bind:value={selectedYear}
						class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-primary-500 focus:border-primary-500"
					>
						<option value="">All Years</option>
						{#each graduationYears as year}
							<option value={year}>{year}</option>
						{/each}
					</select>
				</div>

				<div>
					<label for="major-filter" class="block text-sm font-medium text-gray-700 mb-2">Major</label>
					<select
						id="major-filter"
						bind:value={selectedMajor}
						class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-primary-500 focus:border-primary-500"
					>
						<option value="">All Majors</option>
						{#each majors as major}
							<option value={major}>{major}</option>
						{/each}
					</select>
				</div>

				<div>
					<label for="location-filter" class="block text-sm font-medium text-gray-700 mb-2">Location</label>
					<select
						id="location-filter"
						bind:value={selectedLocation}
						class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-primary-500 focus:border-primary-500"
					>
						<option value="">All Locations</option>
						{#each locations as location}
							<option value={location}>{location}</option>
						{/each}
					</select>
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

<!-- Results Section -->
<section class="py-12">
	<div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
		{#if loading}
			<div class="text-center py-12">
				<div class="animate-spin rounded-full h-12 w-12 border-b-2 border-primary-600 mx-auto"></div>
				<p class="mt-4 text-gray-600">Loading alumni directory...</p>
			</div>
		{:else if error}
			<div class="text-center py-12">
				<p class="text-red-600">{error}</p>
			</div>
		{:else}
			<!-- Results Header -->
			<div class="flex justify-between items-center mb-8">
				<h2 class="text-2xl font-bold text-gray-900">
					{filteredAlumni.length} Alumni Found
				</h2>
				{#if searchTerm || selectedYear || selectedMajor || selectedLocation}
					<p class="text-sm text-gray-600">
						Showing filtered results
					</p>
				{/if}
			</div>

			{#if filteredAlumni.length === 0}
				<div class="text-center py-12">
					<div class="w-16 h-16 bg-gray-100 rounded-full flex items-center justify-center mx-auto mb-4">
						<Search class="w-8 h-8 text-gray-400" />
					</div>
					<h3 class="text-lg font-medium text-gray-900 mb-2">No alumni found</h3>
					<p class="text-gray-600 mb-4">
						Try adjusting your search criteria or filters to find more results.
					</p>
					<button
						onclick={clearFilters}
						class="btn-primary"
					>
						Clear All Filters
					</button>
				</div>
			{:else}
				<!-- Alumni Grid -->
				<div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
					{#each filteredAlumni as alum}
						<div class="bg-white rounded-lg shadow-md hover:shadow-lg transition-shadow border border-gray-200">
							<div class="p-6">
								<!-- Profile Header -->
								<div class="flex items-start space-x-4 mb-4">
									<div class="w-16 h-16 bg-primary-100 rounded-full flex items-center justify-center flex-shrink-0">
										<GraduationCap class="w-8 h-8 text-primary-600" />
									</div>
									<div class="flex-1 min-w-0">
										<h3 class="text-lg font-semibold text-gray-900 truncate">
											{alum.current_position || 'Alumni'}
										</h3>
										{#if alum.current_company}
											<p class="text-gray-600 flex items-center space-x-1">
												<Building class="w-4 h-4" />
												<span class="truncate">{alum.current_company}</span>
											</p>
										{/if}
									</div>
								</div>

								<!-- Education Info -->
								<div class="mb-4">
									<div class="flex items-center space-x-2 text-sm text-gray-600 mb-1">
										<GraduationCap class="w-4 h-4" />
										<span>{alum.graduation_year} • {alum.degree}</span>
									</div>
									<p class="text-sm text-gray-700">{alum.major}</p>
								</div>

								<!-- Location -->
								{#if alum.location}
									<div class="flex items-center space-x-2 text-sm text-gray-600 mb-4">
										<MapPin class="w-4 h-4" />
										<span>{alum.location}</span>
									</div>
								{/if}

								<!-- Bio -->
								{#if alum.bio}
									<p class="text-sm text-gray-600 mb-4 line-clamp-3">
										{alum.bio}
									</p>
								{/if}

								<!-- Action Buttons -->
								<div class="flex justify-between items-center">
									<!-- Social Links -->
									<div class="flex space-x-2">
										{#if alum.linkedin_url}
											<a
												href={alum.linkedin_url}
												target="_blank"
												rel="noopener noreferrer"
												class="text-primary-600 hover:text-primary-700 flex items-center space-x-1 text-sm"
											>
												<ExternalLink class="w-4 h-4" />
												<span>LinkedIn</span>
											</a>
										{/if}
										{#if alum.github_url}
											<a
												href={alum.github_url}
												target="_blank"
												rel="noopener noreferrer"
												class="text-primary-600 hover:text-primary-700 flex items-center space-x-1 text-sm"
											>
												<ExternalLink class="w-4 h-4" />
												<span>GitHub</span>
											</a>
										{/if}
										{#if alum.website_url}
											<a
												href={alum.website_url}
												target="_blank"
												rel="noopener noreferrer"
												class="text-primary-600 hover:text-primary-700 flex items-center space-x-1 text-sm"
											>
												<ExternalLink class="w-4 h-4" />
												<span>Website</span>
											</a>
										{/if}
									</div>

									<!-- View Profile Button -->
									<a
										href="/alumni/{alum.id}"
										class="btn-primary text-sm px-4 py-2"
									>
										View Profile
									</a>
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
			Want to Connect with Fellow Alumni?
		</h2>
		<p class="text-xl text-gray-600 mb-8 max-w-2xl mx-auto">
			Join our alumni network to access the full directory, connect with others, and stay updated with events and opportunities.
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
	.line-clamp-3 {
		display: -webkit-box;
		-webkit-line-clamp: 3;
		-webkit-box-orient: vertical;
		overflow: hidden;
	}
</style>
