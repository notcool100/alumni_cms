<script lang="ts">
	import { Settings, Bell, Shield, Palette, Globe } from 'lucide-svelte';
</script>

<svelte:head>
	<title>Settings - Alumni Network</title>
</svelte:head>

<div class="space-y-6">
	<!-- Header -->
	<div>
		<h1 class="text-2xl font-bold text-gray-900">Settings</h1>
		<p class="text-gray-600 mt-1">Manage your account preferences and settings</p>
	</div>

	<!-- Settings Sections -->
	<div class="grid grid-cols-1 lg:grid-cols-2 gap-6">
		<!-- Account Settings -->
		<div class="card">
			<h3 class="text-lg font-semibold text-gray-900 mb-4">Account Settings</h3>
			<div class="space-y-4">
				<div class="flex items-center justify-between">
					<div>
						<h4 class="font-medium text-gray-900">Profile Information</h4>
						<p class="text-sm text-gray-600">Update your personal details</p>
					</div>
					<button class="text-primary-600 hover:text-primary-500 text-sm font-medium">
						Edit
					</button>
				</div>
				
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
						<h4 class="font-medium text-gray-900">Email Preferences</h4>
						<p class="text-sm text-gray-600">Manage email notifications</p>
					</div>
					<button class="text-primary-600 hover:text-primary-500 text-sm font-medium">
						Configure
					</button>
				</div>
			</div>
		</div>

		<!-- Privacy Settings -->
		<div class="card">
			<h3 class="text-lg font-semibold text-gray-900 mb-4">Privacy & Security</h3>
			<div class="space-y-4">
				<div class="flex items-center justify-between">
					<div>
						<h4 class="font-medium text-gray-900">Privacy Settings</h4>
						<p class="text-sm text-gray-600">Control your profile visibility</p>
					</div>
					<button class="text-primary-600 hover:text-primary-500 text-sm font-medium">
						Manage
					</button>
				</div>
				
				<div class="flex items-center justify-between">
					<div>
						<h4 class="font-medium text-gray-900">Two-Factor Authentication</h4>
						<p class="text-sm text-gray-600">Add extra security to your account</p>
					</div>
					<button class="text-primary-600 hover:text-primary-500 text-sm font-medium">
						Enable
					</button>
				</div>
				
				<div class="flex items-center justify-between">
					<div>
						<h4 class="font-medium text-gray-900">Data Export</h4>
						<p class="text-sm text-gray-600">Download your personal data</p>
					</div>
					<button class="text-primary-600 hover:text-primary-500 text-sm font-medium">
						Export
					</button>
				</div>
			</div>
		</div>

		<!-- Appearance -->
		<div class="card">
			<h3 class="text-lg font-semibold text-gray-900 mb-4">Appearance</h3>
			<div class="space-y-4">
				<div class="flex items-center justify-between">
					<div>
						<h4 class="font-medium text-gray-900">Theme</h4>
						<p class="text-sm text-gray-600">Choose your preferred theme</p>
					</div>
					<select class="input-field text-sm">
						<option>Light</option>
						<option>Dark</option>
						<option>System</option>
					</select>
				</div>
				
				<div class="flex items-center justify-between">
					<div>
						<h4 class="font-medium text-gray-900">Language</h4>
						<p class="text-sm text-gray-600">Select your preferred language</p>
					</div>
					<select class="input-field text-sm">
						<option>English</option>
						<option>Spanish</option>
						<option>French</option>
					</select>
				</div>
			</div>
		</div>

		<!-- Notifications -->
		<div class="card">
			<h3 class="text-lg font-semibold text-gray-900 mb-4">Notifications</h3>
			<div class="space-y-4">
				<div class="flex items-center justify-between">
					<div>
						<h4 class="font-medium text-gray-900">Event Reminders</h4>
						<p class="text-sm text-gray-600">Get notified about upcoming events</p>
					</div>
					<input type="checkbox" checked class="rounded border-gray-300 text-primary-600 focus:ring-primary-500">
				</div>
				
				<div class="flex items-center justify-between">
					<div>
						<h4 class="font-medium text-gray-900">Alumni Updates</h4>
						<p class="text-sm text-gray-600">Receive updates from your network</p>
					</div>
					<input type="checkbox" checked class="rounded border-gray-300 text-primary-600 focus:ring-primary-500">
				</div>
				
				<div class="flex items-center justify-between">
					<div>
						<h4 class="font-medium text-gray-900">Newsletter</h4>
						<p class="text-sm text-gray-600">Subscribe to our newsletter</p>
					</div>
					<input type="checkbox" class="rounded border-gray-300 text-primary-600 focus:ring-primary-500">
				</div>
			</div>
		</div>
	</div>
</div>

