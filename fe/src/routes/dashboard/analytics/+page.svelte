<script lang="ts">
	import { BarChart3, Users, Calendar, TrendingUp } from 'lucide-svelte';
</script>

<svelte:head>
	<title>Analytics - Alumni Network</title>
</svelte:head>

<div class="space-y-6">
	<!-- Header -->
	<div>
		<h1 class="text-2xl font-bold text-gray-900">Analytics</h1>
		<p class="text-gray-600 mt-1">View insights and statistics about your alumni network</p>
	</div>

	<!-- Stats Grid -->
	<div class="grid grid-cols-1 md:grid-cols-4 gap-6">
		<div class="card">
			<div class="flex items-center">
				<div class="bg-primary-100 p-3 rounded-lg">
					<Users class="h-6 w-6 text-primary-600" />
				</div>
				<div class="ml-4">
					<p class="text-sm font-medium text-gray-600">Total Alumni</p>
					<p class="text-2xl font-bold text-gray-900">2,547</p>
				</div>
			</div>
		</div>
		
		<div class="card">
			<div class="flex items-center">
				<div class="bg-green-100 p-3 rounded-lg">
					<Calendar class="h-6 w-6 text-green-600" />
				</div>
				<div class="ml-4">
					<p class="text-sm font-medium text-gray-600">Active Events</p>
					<p class="text-2xl font-bold text-gray-900">12</p>
				</div>
			</div>
		</div>
		
		<div class="card">
			<div class="flex items-center">
				<div class="bg-yellow-100 p-3 rounded-lg">
					<TrendingUp class="h-6 w-6 text-yellow-600" />
				</div>
				<div class="ml-4">
					<p class="text-sm font-medium text-gray-600">Engagement Rate</p>
					<p class="text-2xl font-bold text-gray-900">78%</p>
				</div>
			</div>
		</div>
		
		<div class="card">
			<div class="flex items-center">
				<div class="bg-purple-100 p-3 rounded-lg">
					<BarChart3 class="h-6 w-6 text-purple-600" />
				</div>
				<div class="ml-4">
					<p class="text-sm font-medium text-gray-600">Growth Rate</p>
					<p class="text-2xl font-bold text-gray-900">+15%</p>
				</div>
			</div>
		</div>
	</div>

	<!-- Placeholder Content -->
	<div class="card text-center py-12">
		<BarChart3 class="h-12 w-12 text-gray-400 mx-auto mb-4" />
		<h3 class="text-lg font-medium text-gray-900 mb-2">Analytics Dashboard</h3>
		<p class="text-gray-600">Detailed analytics and insights will be available here.</p>
	</div>
</div>

