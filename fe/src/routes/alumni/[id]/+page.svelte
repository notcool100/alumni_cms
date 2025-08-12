<script lang="ts">
	import { onMount } from 'svelte';
	import { page } from '$app/stores';
	import { apiService, type Alumni } from '$lib/api';
	import { MapPin, Building, GraduationCap, ExternalLink, Calendar, ArrowLeft, Mail, Phone } from 'lucide-svelte';

	let alumni: Alumni | null = null;
	let loading = true;
	let error = '';

	$: alumniId = $page.params.id;

	onMount(async () => {
		try {
			const response = await apiService.getAlumniById(alumniId);
			if (response.success && response.data) {
				alumni = response.data;
			} else {
				error = 'Alumni profile not found';
			}
		} catch (err) {
			error = 'Failed to load alumni profile';
			console.error('Error loading alumni:', err);
		} finally {
			loading = false;
		}
	});

	function formatDate(dateString: string): string {
		const date = new Date(dateString);
		return date.toLocaleDateString('en-US', {
			year: 'numeric',
			month: 'long',
			day: 'numeric'
		});
	}
</script>

<svelte:head>
	<title>{alumni ? `${alumni.current_position || 'Alumni'} - ${alumni.current_company || 'Alumni Network'}` : 'Alumni Profile - Alumni Network'}</title>
	<meta name="description" content={alumni ? `Learn more about ${alumni.current_position || 'our alumni'} at ${alumni.current_company || 'our network'}` : 'Alumni profile page'} />
</svelte:head>

{#if loading}
	<div class="min-h-screen bg-gray-50 flex items-center justify-center">
		<div class="text-center">
			<div class="animate-spin rounded-full h-12 w-12 border-b-2 border-primary-600 mx-auto"></div>
			<p class="mt-4 text-gray-600">Loading alumni profile...</p>
		</div>
	</div>
{:else if error}
	<div class="min-h-screen bg-gray-50 flex items-center justify-center">
		<div class="text-center">
			<div class="w-16 h-16 bg-red-100 rounded-full flex items-center justify-center mx-auto mb-4">
				<GraduationCap class="w-8 h-8 text-red-600" />
			</div>
			<h2 class="text-2xl font-bold text-gray-900 mb-2">Profile Not Found</h2>
			<p class="text-gray-600 mb-6">{error}</p>
			<a href="/alumni" class="btn-primary">
				Back to Alumni Directory
			</a>
		</div>
	</div>
{:else if alumni}
	<!-- Hero Section -->
	<section class="bg-gradient-to-br from-primary-600 to-primary-800 text-white py-16">
		<div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
			<div class="flex items-center space-x-4 mb-6">
				<a href="/alumni" class="flex items-center space-x-2 text-primary-100 hover:text-white transition-colors">
					<ArrowLeft class="w-5 h-5" />
					<span>Back to Directory</span>
				</a>
			</div>
			
			<div class="flex flex-col md:flex-row items-start space-y-6 md:space-y-0 md:space-x-8">
				<!-- Profile Image -->
				<div class="w-32 h-32 bg-white rounded-full flex items-center justify-center flex-shrink-0">
					<GraduationCap class="w-16 h-16 text-primary-600" />
				</div>
				
				<!-- Profile Info -->
				<div class="flex-1">
					<h1 class="text-3xl md:text-4xl font-bold mb-2">
						{alumni.current_position || 'Alumni Member'}
					</h1>
					{#if alumni.current_company}
						<p class="text-xl text-primary-100 mb-4 flex items-center space-x-2">
							<Building class="w-5 h-5" />
							<span>{alumni.current_company}</span>
						</p>
					{/if}
					
					<div class="flex flex-wrap gap-4 text-sm">
						<div class="flex items-center space-x-2">
							<GraduationCap class="w-4 h-4" />
							<span>{alumni.graduation_year} • {alumni.degree}</span>
						</div>
						{#if alumni.location}
							<div class="flex items-center space-x-2">
								<MapPin class="w-4 h-4" />
								<span>{alumni.location}</span>
							</div>
						{/if}
					</div>
				</div>
			</div>
		</div>
	</section>

	<!-- Profile Details -->
	<section class="py-12 bg-white">
		<div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
			<div class="grid grid-cols-1 lg:grid-cols-3 gap-8">
				<!-- Main Content -->
				<div class="lg:col-span-2">
					<!-- Bio -->
					{#if alumni.bio}
						<div class="mb-8">
							<h2 class="text-2xl font-bold text-gray-900 mb-4">About</h2>
							<p class="text-gray-600 leading-relaxed">
								{alumni.bio}
							</p>
						</div>
					{/if}

					<!-- Education -->
					<div class="mb-8">
						<h2 class="text-2xl font-bold text-gray-900 mb-4">Education</h2>
						<div class="bg-gray-50 rounded-lg p-6">
							<div class="flex items-start space-x-4">
								<div class="bg-primary-100 w-12 h-12 rounded-lg flex items-center justify-center flex-shrink-0">
									<GraduationCap class="w-6 h-6 text-primary-600" />
								</div>
								<div class="flex-1">
									<h3 class="text-lg font-semibold text-gray-900 mb-1">
										{alumni.degree} in {alumni.major}
									</h3>
									<p class="text-gray-600 mb-2">Graduated {alumni.graduation_year}</p>
									<p class="text-sm text-gray-500">
										Member since {formatDate(alumni.created_at)}
									</p>
								</div>
							</div>
						</div>
					</div>

					<!-- Professional Experience -->
					{#if alumni.current_company || alumni.current_position}
						<div class="mb-8">
							<h2 class="text-2xl font-bold text-gray-900 mb-4">Professional Experience</h2>
							<div class="bg-gray-50 rounded-lg p-6">
								<div class="flex items-start space-x-4">
									<div class="bg-primary-100 w-12 h-12 rounded-lg flex items-center justify-center flex-shrink-0">
										<Building class="w-6 h-6 text-primary-600" />
									</div>
									<div class="flex-1">
										<h3 class="text-lg font-semibold text-gray-900 mb-1">
											{alumni.current_position || 'Professional'}
										</h3>
										{#if alumni.current_company}
											<p class="text-gray-600 mb-2">{alumni.current_company}</p>
										{/if}
										{#if alumni.location}
											<p class="text-sm text-gray-500 flex items-center space-x-1">
												<MapPin class="w-4 h-4" />
												<span>{alumni.location}</span>
											</p>
										{/if}
									</div>
								</div>
							</div>
						</div>
					{/if}
				</div>

				<!-- Sidebar -->
				<div class="lg:col-span-1">
					<!-- Contact & Social Links -->
					<div class="bg-gray-50 rounded-lg p-6 mb-6">
						<h3 class="text-lg font-semibold text-gray-900 mb-4">Connect</h3>
						
						<div class="space-y-3">
							{#if alumni.linkedin_url}
								<a
									href={alumni.linkedin_url}
									target="_blank"
									rel="noopener noreferrer"
									class="flex items-center space-x-3 p-3 bg-white rounded-lg hover:bg-gray-50 transition-colors"
								>
									<ExternalLink class="w-5 h-5 text-primary-600" />
									<span class="text-gray-700">LinkedIn Profile</span>
								</a>
							{/if}
							
							{#if alumni.github_url}
								<a
									href={alumni.github_url}
									target="_blank"
									rel="noopener noreferrer"
									class="flex items-center space-x-3 p-3 bg-white rounded-lg hover:bg-gray-50 transition-colors"
								>
									<ExternalLink class="w-5 h-5 text-primary-600" />
									<span class="text-gray-700">GitHub Profile</span>
								</a>
							{/if}
							
							{#if alumni.website_url}
								<a
									href={alumni.website_url}
									target="_blank"
									rel="noopener noreferrer"
									class="flex items-center space-x-3 p-3 bg-white rounded-lg hover:bg-gray-50 transition-colors"
								>
									<ExternalLink class="w-5 h-5 text-primary-600" />
									<span class="text-gray-700">Personal Website</span>
								</a>
							{/if}
						</div>
					</div>

					<!-- Profile Stats -->
					<div class="bg-gray-50 rounded-lg p-6">
						<h3 class="text-lg font-semibold text-gray-900 mb-4">Profile Information</h3>
						
						<div class="space-y-3">
							<div class="flex justify-between">
								<span class="text-gray-600">Member Since</span>
								<span class="font-medium text-gray-900">
									{formatDate(alumni.created_at)}
								</span>
							</div>
							
							<div class="flex justify-between">
								<span class="text-gray-600">Last Updated</span>
								<span class="font-medium text-gray-900">
									{formatDate(alumni.updated_at)}
								</span>
							</div>
							
							<div class="flex justify-between">
								<span class="text-gray-600">Profile Status</span>
								<span class="font-medium text-green-600">
									{alumni.is_public ? 'Public' : 'Private'}
								</span>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</section>

	<!-- Related Alumni Section -->
	<section class="py-12 bg-gray-50">
		<div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
			<div class="text-center mb-8">
				<h2 class="text-2xl font-bold text-gray-900 mb-2">Connect with Similar Alumni</h2>
				<p class="text-gray-600">Discover other alumni from the same year or field of study</p>
			</div>
			
			<div class="text-center">
				<a href="/alumni" class="btn-primary">
					Browse All Alumni
				</a>
			</div>
		</div>
	</section>
{/if}
