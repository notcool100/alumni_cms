<script lang="ts">
	import { onMount } from 'svelte';
	import { goto } from '$app/navigation';
	import { apiService } from '$lib/api';
	import { 
		Save, 
		X, 
		User, 
		GraduationCap, 
		Building, 
		MapPin,
		Globe,
		Link,
		Upload
	} from 'lucide-svelte';
	
	let loading = false;
	let saving = false;
	let formData = {
		graduation_year: new Date().getFullYear(),
		degree: '',
		major: '',
		current_company: '',
		current_position: '',
		location: '',
		bio: '',
		linkedin_url: '',
		github_url: '',
		website_url: '',
		is_public: true
	};
	
	const degrees = ['Bachelor', 'Master', 'PhD', 'Associate', 'Certificate', 'Diploma'];
	const majors = [
		'Computer Science', 'Engineering', 'Business Administration', 'Arts', 'Sciences',
		'Medicine', 'Law', 'Education', 'Social Sciences', 'Humanities', 'Other'
	];
	
	onMount(() => {
		// Check if user is authenticated and is admin
		if (!apiService.isAuthenticated()) {
			goto('/auth/login');
			return;
		}
		
		const user = apiService.getCurrentUserFromStorage();
		if (user?.roleName !== 'Admin') {
			goto('/dashboard');
			return;
		}
	});
	
	async function handleSubmit() {
		saving = true;
		
		try {
			const response = await apiService.createAlumni(formData);
			if (response.success) {
				goto('/admin/alumni');
			} else {
				alert('Error creating alumni profile: ' + response.message);
			}
		} catch (error) {
			console.error('Error creating alumni:', error);
			alert('Error creating alumni profile. Please try again.');
		} finally {
			saving = false;
		}
	}
	
	function handleCancel() {
		goto('/admin/alumni');
	}
</script>

<svelte:head>
	<title>Add New Alumni - Admin CMS</title>
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
				<h1 class="text-3xl font-bold text-gray-900">Add New Alumni</h1>
				<p class="text-gray-600 mt-2">Create a new alumni profile</p>
			</div>
			<div class="flex items-center space-x-3">
				<button on:click={handleCancel} class="btn-secondary flex items-center">
					<X class="h-5 w-5 mr-2" />
					Cancel
				</button>
				<button on:click={handleSubmit} disabled={saving} class="btn-primary flex items-center">
					<Save class="h-5 w-5 mr-2" />
					{saving ? 'Saving...' : 'Save Alumni'}
				</button>
			</div>
		</div>
	</div>

	<!-- Form -->
	<form on:submit|preventDefault={handleSubmit} class="space-y-8">
		<!-- Basic Information -->
		<div class="card">
			<h2 class="text-lg font-semibold text-gray-900 mb-6 flex items-center">
				<User class="h-5 w-5 mr-2" />
				Basic Information
			</h2>
			
			<div class="grid grid-cols-1 md:grid-cols-2 gap-6">
				<div>
					<label for="graduation_year" class="form-label">Graduation Year</label>
					<input
						id="graduation_year"
						type="number"
						bind:value={formData.graduation_year}
						min="1950"
						max={new Date().getFullYear() + 5}
						class="input-field"
						required
					/>
				</div>
				
				<div>
					<label for="degree" class="form-label">Degree</label>
					<select id="degree" bind:value={formData.degree} class="input-field" required>
						<option value="">Select Degree</option>
						{#each degrees as degree}
							<option value={degree}>{degree}</option>
						{/each}
					</select>
				</div>
				
				<div class="md:col-span-2">
					<label for="major" class="form-label">Major/Field of Study</label>
					<select id="major" bind:value={formData.major} class="input-field" required>
						<option value="">Select Major</option>
						{#each majors as major}
							<option value={major}>{major}</option>
						{/each}
					</select>
				</div>
			</div>
		</div>

		<!-- Professional Information -->
		<div class="card">
			<h2 class="text-lg font-semibold text-gray-900 mb-6 flex items-center">
				<Building class="h-5 w-5 mr-2" />
				Professional Information
			</h2>
			
			<div class="grid grid-cols-1 md:grid-cols-2 gap-6">
				<div>
					<label for="current_company" class="form-label">Current Company</label>
					<input
						id="current_company"
						type="text"
						bind:value={formData.current_company}
						class="input-field"
						placeholder="Company name"
					/>
				</div>
				
				<div>
					<label for="current_position" class="form-label">Current Position</label>
					<input
						id="current_position"
						type="text"
						bind:value={formData.current_position}
						class="input-field"
						placeholder="Job title"
					/>
				</div>
				
				<div class="md:col-span-2">
					<label for="location" class="form-label">Location</label>
					<div class="relative">
						<MapPin class="absolute left-3 top-1/2 transform -translate-y-1/2 h-5 w-5 text-gray-400" />
						<input
							id="location"
							type="text"
							bind:value={formData.location}
							class="input-field pl-10"
							placeholder="City, Country"
						/>
					</div>
				</div>
			</div>
		</div>

		<!-- Bio and Description -->
		<div class="card">
			<h2 class="text-lg font-semibold text-gray-900 mb-6 flex items-center">
				<User class="h-5 w-5 mr-2" />
				Bio & Description
			</h2>
			
			<div>
				<label for="bio" class="form-label">Bio</label>
				<textarea
					id="bio"
					bind:value={formData.bio}
					rows="4"
					class="input-field"
					placeholder="Tell us about the alumni's background, achievements, and interests..."
				></textarea>
				<p class="text-sm text-gray-500 mt-1">Provide a brief description of the alumni's background and achievements.</p>
			</div>
		</div>

		<!-- Social Links -->
		<div class="card">
			<h2 class="text-lg font-semibold text-gray-900 mb-6 flex items-center">
				<Globe class="h-5 w-5 mr-2" />
				Social Links & Websites
			</h2>
			
			<div class="space-y-4">
				<div>
					<label for="linkedin_url" class="form-label">LinkedIn Profile</label>
					<div class="relative">
						<Link class="absolute left-3 top-1/2 transform -translate-y-1/2 h-5 w-5 text-gray-400" />
						<input
							id="linkedin_url"
							type="url"
							bind:value={formData.linkedin_url}
							class="input-field pl-10"
							placeholder="https://linkedin.com/in/username"
						/>
					</div>
				</div>
				
				<div>
					<label for="github_url" class="form-label">GitHub Profile</label>
					<div class="relative">
						<Link class="absolute left-3 top-1/2 transform -translate-y-1/2 h-5 w-5 text-gray-400" />
						<input
							id="github_url"
							type="url"
							bind:value={formData.github_url}
							class="input-field pl-10"
							placeholder="https://github.com/username"
						/>
					</div>
				</div>
				
				<div>
					<label for="website_url" class="form-label">Personal Website</label>
					<div class="relative">
						<Globe class="absolute left-3 top-1/2 transform -translate-y-1/2 h-5 w-5 text-gray-400" />
						<input
							id="website_url"
							type="url"
							bind:value={formData.website_url}
							class="input-field pl-10"
							placeholder="https://example.com"
						/>
					</div>
				</div>
			</div>
		</div>

		<!-- Settings -->
		<div class="card">
			<h2 class="text-lg font-semibold text-gray-900 mb-6">Settings</h2>
			
			<div class="flex items-center">
				<input
					id="is_public"
					type="checkbox"
					bind:checked={formData.is_public}
					class="h-4 w-4 text-primary-600 focus:ring-primary-500 border-gray-300 rounded"
				/>
				<label for="is_public" class="ml-2 block text-sm text-gray-900">
					Make profile public
				</label>
			</div>
			<p class="text-sm text-gray-500 mt-1">Public profiles will be visible to other alumni in the network.</p>
		</div>

		<!-- Form Actions -->
		<div class="flex items-center justify-end space-x-3 pt-6 border-t border-gray-200">
			<button type="button" on:click={handleCancel} class="btn-secondary">
				Cancel
			</button>
			<button type="submit" disabled={saving} class="btn-primary">
				{saving ? 'Saving...' : 'Create Alumni Profile'}
			</button>
		</div>
	</form>
{/if}
