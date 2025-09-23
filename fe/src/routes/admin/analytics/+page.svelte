<script lang="ts">
	import { onMount } from 'svelte';
	import { 
		Users, 
		Calendar, 
		TrendingUp, 
		TrendingDown,
		MapPin,
		Building,
		Globe,
		BarChart3,
		PieChart,
		Activity,
		ArrowUpRight,
		ArrowDownRight,
		Download,
		FileText
	} from 'lucide-svelte';
	
	let loading = true;
	let selectedPeriod = '30d';
	let selectedMetric = 'alumni';
	
	const periods = [
		{ value: '7d', label: 'Last 7 days' },
		{ value: '30d', label: 'Last 30 days' },
		{ value: '90d', label: 'Last 90 days' },
		{ value: '1y', label: 'Last year' }
	];
	
	const metrics = [
		{ value: 'alumni', label: 'Alumni Growth' },
		{ value: 'events', label: 'Event Engagement' },
		{ value: 'content', label: 'Content Performance' },
		{ value: 'geographic', label: 'Geographic Distribution' }
	];
	
	// Mock data for demo
	let stats = {
		totalAlumni: 2547,
		totalEvents: 156,
		totalContent: 89,
		activeUsers: 1247,
		alumniGrowth: 12.5,
		eventAttendance: 78.3,
		contentViews: 4567,
		geographicSpread: 45
	};
	
	let chartData = {
		alumniGrowth: [
			{ month: 'Jan', count: 120 },
			{ month: 'Feb', count: 135 },
			{ month: 'Mar', count: 142 },
			{ month: 'Apr', count: 158 },
			{ month: 'May', count: 167 },
			{ month: 'Jun', count: 189 }
		],
		eventAttendance: [
			{ event: 'Networking Mixer', attendees: 85, capacity: 100 },
			{ event: 'Career Workshop', attendees: 67, capacity: 80 },
			{ event: 'Alumni Reunion', attendees: 234, capacity: 250 },
			{ event: 'Tech Talk', attendees: 45, capacity: 60 }
		],
		geographicDistribution: [
			{ country: 'United States', count: 1247 },
			{ country: 'Canada', count: 234 },
			{ country: 'United Kingdom', count: 189 },
			{ country: 'Germany', count: 156 },
			{ country: 'Australia', count: 123 },
			{ country: 'Others', count: 598 }
		],
		contentPerformance: [
			{ type: 'News', views: 1247, engagement: 23.4 },
			{ type: 'Announcements', views: 892, engagement: 18.7 },
			{ type: 'Resources', views: 567, engagement: 34.2 },
			{ type: 'Articles', views: 234, engagement: 45.1 }
		]
	};
	
	onMount(() => {
		loadAnalytics();
	});
	
	async function loadAnalytics() {
		try {
			// Mock loading - replace with actual API calls
			await new Promise(resolve => setTimeout(resolve, 1000));
		} catch (error) {
			console.error('Error loading analytics:', error);
		} finally {
			loading = false;
		}
	}
	
	function getGrowthColor(value: number) {
		return value >= 0 ? 'text-green-600' : 'text-red-600';
	}
	
	function getGrowthIcon(value: number) {
		return value >= 0 ? ArrowUpRight : ArrowDownRight;
	}
	
	function formatNumber(num: number) {
		return new Intl.NumberFormat().format(num);
	}
	
	function getPercentage(value: number, total: number) {
		return ((value / total) * 100).toFixed(1);
	}
</script>

<svelte:head>
	<title>Analytics - Admin CMS</title>
</svelte:head>

<!-- Page Header -->
<div class="mb-8">
	<div class="flex items-center justify-between">
		<div>
			<h1 class="text-3xl font-bold text-gray-900">Analytics Dashboard</h1>
			<p class="text-gray-600 mt-2">Track performance and insights across your alumni network</p>
		</div>
		<button class="btn-secondary flex items-center">
			<Download class="h-5 w-5 mr-2" />
			Export Report
		</button>
	</div>
</div>

{#if loading}
	<div class="flex items-center justify-center h-64">
		<div class="animate-spin rounded-full h-12 w-12 border-b-2 border-primary-600"></div>
	</div>
{:else}
	<!-- Filters -->
	<div class="card mb-8">
		<div class="flex items-center space-x-4">
			<label for="time-period" class="text-sm font-medium text-gray-700">Time Period:</label>
			<select id="time-period" bind:value={selectedPeriod} class="input-field w-48">
				{#each periods as period}
					<option value={period.value}>{period.label}</option>
				{/each}
			</select>

			<label for="metric-select" class="text-sm font-medium text-gray-700 ml-4">Metric:</label>
			<select id="metric-select" bind:value={selectedMetric} class="input-field w-48">
				{#each metrics as metric}
					<option value={metric.value}>{metric.label}</option>
				{/each}
			</select>
		</div>
	</div>

	<!-- Key Metrics -->
	<div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
		<div class="card">
			<div class="flex items-center justify-between">
				<div>
					<p class="text-sm font-medium text-gray-600">Total Alumni</p>
					<p class="text-3xl font-bold text-gray-900">{formatNumber(stats.totalAlumni)}</p>
					<p class="text-sm {getGrowthColor(stats.alumniGrowth)} flex items-center mt-1">
						<svelte:component this={getGrowthIcon(stats.alumniGrowth)} class="h-4 w-4 mr-1" />
						{stats.alumniGrowth}% from last month
					</p>
				</div>
				<div class="bg-primary-100 p-3 rounded-lg">
					<Users class="h-8 w-8 text-primary-600" />
				</div>
			</div>
		</div>
		
		<div class="card">
			<div class="flex items-center justify-between">
				<div>
					<p class="text-sm font-medium text-gray-600">Total Events</p>
					<p class="text-3xl font-bold text-gray-900">{formatNumber(stats.totalEvents)}</p>
					<p class="text-sm text-green-600 flex items-center mt-1">
						<ArrowUpRight class="h-4 w-4 mr-1" />
						{stats.eventAttendance}% avg attendance
					</p>
				</div>
				<div class="bg-green-100 p-3 rounded-lg">
					<Calendar class="h-8 w-8 text-green-600" />
				</div>
			</div>
		</div>
		
		<div class="card">
			<div class="flex items-center justify-between">
				<div>
					<p class="text-sm font-medium text-gray-600">Content Views</p>
					<p class="text-3xl font-bold text-gray-900">{formatNumber(stats.contentViews)}</p>
					<p class="text-sm text-green-600 flex items-center mt-1">
						<ArrowUpRight class="h-4 w-4 mr-1" />
						+15.3% from last month
					</p>
				</div>
				<div class="bg-yellow-100 p-3 rounded-lg">
					<FileText class="h-8 w-8 text-yellow-600" />
				</div>
			</div>
		</div>
		
		<div class="card">
			<div class="flex items-center justify-between">
				<div>
					<p class="text-sm font-medium text-gray-600">Active Users</p>
					<p class="text-3xl font-bold text-gray-900">{formatNumber(stats.activeUsers)}</p>
					<p class="text-sm text-green-600 flex items-center mt-1">
						<ArrowUpRight class="h-4 w-4 mr-1" />
						+8.7% from last month
					</p>
				</div>
				<div class="bg-purple-100 p-3 rounded-lg">
					<Activity class="h-8 w-8 text-purple-600" />
				</div>
			</div>
		</div>
	</div>

	<!-- Charts Section -->
	<div class="grid grid-cols-1 lg:grid-cols-2 gap-8 mb-8">
		<!-- Alumni Growth Chart -->
		<div class="card">
			<h3 class="text-lg font-semibold text-gray-900 mb-4">Alumni Growth Trend</h3>
			<div class="h-64 flex items-end justify-between space-x-2">
				{#each chartData.alumniGrowth as item, index}
					<div class="flex-1 flex flex-col items-center">
						<div 
							class="w-full bg-primary-600 rounded-t"
							style="height: {(item.count / 200) * 100}%"
						></div>
						<span class="text-xs text-gray-500 mt-2">{item.month}</span>
					</div>
				{/each}
			</div>
			<div class="mt-4 text-center">
				<p class="text-sm text-gray-600">Monthly new alumni registrations</p>
			</div>
		</div>
		
		<!-- Event Attendance -->
		<div class="card">
			<h3 class="text-lg font-semibold text-gray-900 mb-4">Event Attendance</h3>
			<div class="space-y-4">
				{#each chartData.eventAttendance as event}
					<div>
						<div class="flex items-center justify-between mb-2">
							<span class="text-sm font-medium text-gray-900">{event.event}</span>
							<span class="text-sm text-gray-500">{event.attendees}/{event.capacity}</span>
						</div>
						<div class="w-full bg-gray-200 rounded-full h-2">
							<div 
								class="bg-green-600 h-2 rounded-full" 
								style="width: {(event.attendees / event.capacity) * 100}%"
							></div>
						</div>
					</div>
				{/each}
			</div>
		</div>
	</div>

	<!-- Geographic Distribution -->
	<div class="card mb-8">
		<h3 class="text-lg font-semibold text-gray-900 mb-4">Geographic Distribution</h3>
		<div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
			{#each chartData.geographicDistribution as country}
				<div class="flex items-center justify-between p-4 bg-gray-50 rounded-lg">
					<div class="flex items-center">
						<MapPin class="h-5 w-5 text-gray-400 mr-3" />
						<span class="text-sm font-medium text-gray-900">{country.country}</span>
					</div>
					<div class="text-right">
						<p class="text-sm font-bold text-gray-900">{formatNumber(country.count)}</p>
						<p class="text-xs text-gray-500">{getPercentage(country.count, stats.totalAlumni)}%</p>
					</div>
				</div>
			{/each}
		</div>
	</div>

	<!-- Content Performance -->
	<div class="card">
		<h3 class="text-lg font-semibold text-gray-900 mb-4">Content Performance</h3>
		<div class="overflow-x-auto">
			<table class="min-w-full divide-y divide-gray-200">
				<thead class="bg-gray-50">
					<tr>
						<th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
							Content Type
						</th>
						<th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
							Views
						</th>
						<th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
							Engagement Rate
						</th>
						<th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
							Trend
						</th>
					</tr>
				</thead>
				<tbody class="bg-white divide-y divide-gray-200">
					{#each chartData.contentPerformance as content}
						<tr>
							<td class="px-6 py-4 whitespace-nowrap">
								<div class="flex items-center">
									<div class="w-8 h-8 bg-primary-100 rounded-lg flex items-center justify-center">
										<FileText class="h-4 w-4 text-primary-600" />
									</div>
									<span class="ml-3 text-sm font-medium text-gray-900">{content.type}</span>
								</div>
							</td>
							<td class="px-6 py-4 whitespace-nowrap">
								<span class="text-sm text-gray-900">{formatNumber(content.views)}</span>
							</td>
							<td class="px-6 py-4 whitespace-nowrap">
								<span class="text-sm text-gray-900">{content.engagement}%</span>
							</td>
							<td class="px-6 py-4 whitespace-nowrap">
								<span class="text-green-600 flex items-center">
									<ArrowUpRight class="h-4 w-4 mr-1" />
									<span class="text-sm">+12.5%</span>
								</span>
							</td>
						</tr>
					{/each}
				</tbody>
			</table>
		</div>
	</div>
{/if}
