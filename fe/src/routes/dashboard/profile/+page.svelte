<script lang="ts">
	import { onMount } from 'svelte';
	import { apiService } from '$lib/api';
	import { User, Mail, Phone, MapPin, Building, GraduationCap, Edit } from 'lucide-svelte';
	
	let user: any = null;
	let loading = true;
	
	onMount(() => {
		user = apiService.getCurrentUserFromStorage();
		loading = false;
	});
</script>

<svelte:head>
	<title>My Profile - Alumni Network</title>
</svelte:head>

<div class="space-y-6">
	<!-- Header -->
	<div class="flex items-center justify-between">
		<div>
			<h1 class="text-2xl font-bold text-gray-900">My Profile</h1>
			<p class="text-gray-600 mt-1">Manage your personal information and preferences</p>
		</div>
		<button class="btn-primary flex items-center">
			<Edit class="h-4 w-4 mr-2" />
			Edit Profile
		</button>
	</div>

	{#if loading}
		<div class="flex items-center justify-center py-12">
			<div class="animate-spin rounded-full h-32 w-32 border-b-2 border-primary-600"></div>
		</div>
	{:else}
		<div class="grid grid-cols-1 lg:grid-cols-3 gap-6">
			<!-- Profile Card -->
			<div class="lg:col-span-1">
				<div class="card text-center">
					<div class="w-24 h-24 bg-primary-600 rounded-full flex items-center justify-center mx-auto mb-4">
						<span class="text-white font-bold text-2xl">
							{user?.first_name?.[0]}{user?.last_name?.[0]}
						</span>
					</div>
					<h2 class="text-xl font-semibold text-gray-900 mb-1">
						{user?.first_name} {user?.last_name}
					</h2>
					<p class="text-gray-600 mb-4 capitalize">{user?.roleName}</p>
					
					<div class="space-y-3 text-sm">
						<div class="flex items-center justify-center">
							<Mail class="h-4 w-4 mr-2 text-gray-400" />
							{user?.email}
						</div>
					</div>
				</div>
			</div>

			<!-- Profile Details -->
			<div class="lg:col-span-2">
				<div class="card">
					<h3 class="text-lg font-semibold text-gray-900 mb-4">Personal Information</h3>
					
					<div class="grid grid-cols-1 md:grid-cols-2 gap-6">
						<div>
							<label class="form-label">First Name</label>
							<p class="text-gray-900">{user?.first_name || 'Not provided'}</p>
						</div>
						<div>
							<label class="form-label">Last Name</label>
							<p class="text-gray-900">{user?.last_name || 'Not provided'}</p>
						</div>
						<div>
							<label class="form-label">Email</label>
							<p class="text-gray-900">{user?.email}</p>
						</div>
						<div>
							<label class="form-label">Role</label>
							<p class="text-gray-900 capitalize">{user?.roleName}</p>
						</div>
					</div>
				</div>

				<!-- Account Settings -->
				<div class="card mt-6">
					<h3 class="text-lg font-semibold text-gray-900 mb-4">Account Settings</h3>
					
					<div class="space-y-4">
						<div class="flex items-center justify-between">
							<div>
								<h4 class="font-medium text-gray-900">Change Password</h4>
								<p class="text-sm text-gray-600">Update your account password</p>
							</div>
							<button class="text-primary-600 hover:text-primary-500 text-sm font-medium">
								Change
							</button>
						</div>
						
						<div class="flex items-center justify-between">
							<div>
								<h4 class="font-medium text-gray-900">Email Notifications</h4>
								<p class="text-sm text-gray-600">Manage your email preferences</p>
							</div>
							<button class="text-primary-600 hover:text-primary-500 text-sm font-medium">
								Configure
							</button>
						</div>
						
						<div class="flex items-center justify-between">
							<div>
								<h4 class="font-medium text-gray-900">Privacy Settings</h4>
								<p class="text-sm text-gray-600">Control your profile visibility</p>
							</div>
							<button class="text-primary-600 hover:text-primary-500 text-sm font-medium">
								Manage
							</button>
						</div>
					</div>
				</div>
			</div>
		</div>
	{/if}
</div>

