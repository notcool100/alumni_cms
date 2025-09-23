<script lang="ts">
	import { onMount } from 'svelte';
	import { navigationStore } from '$lib/stores/navigation';
	import { page } from '$app/stores';
	import type { NavigationItem, NavigationGroup } from '$lib/api';
	
	// Import all possible icons
	import { 
		Home, Users, Calendar, FileText, Settings, BarChart3, 
		Shield, Mail, User, Plus, Download, Image, Bell,
		LogOut, Menu, X, Info, GraduationCap, CheckCircle,
		Newspaper, Folder, TrendingUp, Navigation
	} from 'lucide-svelte';

	export let type: 'main' | 'admin' | 'mobile' = 'main';
	export let showGroups = false;
	export let className = '';

	// Icon mapping
	const iconMap: Record<string, any> = {
		'home': Home,
		'users': Users,
		'calendar': Calendar,
		'file-text': FileText,
		'settings': Settings,
		'bar-chart': BarChart3,
		'shield': Shield,
		'mail': Mail,
		'user': User,
		'plus': Plus,
		'download': Download,
		'image': Image,
		'bell': Bell,
		'graduation-cap': GraduationCap,
		'check-circle': CheckCircle,
		'newspaper': Newspaper,
		'folder': Folder,
		'trending-up': TrendingUp,
		'navigation': Navigation
	};

	onMount(() => {
		// Load navigation when component mounts
		navigationStore.loadUserNavigation();
	});

	$: navigation = $navigationStore.navigation;
	$: loading = $navigationStore.loading;
	$: error = $navigationStore.error;

	function getIcon(iconName: string | undefined) {
		if (!iconName) return null;
		return iconMap[iconName] || null;
	}

	function isActive(item: NavigationItem): boolean {
		const path = $page.url.pathname;
		return path === item.url || path.startsWith(item.url + '/');
	}

	function getNavigationItems(): NavigationItem[] {
		if (!navigation) return [];
		
		if (type === 'main') {
			return navigation.groups
				.find(g => g.name.toLowerCase().includes('main'))
				?.navigationItems || [];
		} else if (type === 'admin') {
			return navigation.groups
				.filter(g => ['administration', 'content', 'analytics', 'settings'].some(name => 
					g.name.toLowerCase().includes(name)))
				.flatMap(g => g.navigationItems);
		}
		
		return navigation.flatItems;
	}

	function getNavigationGroups(): NavigationGroup[] {
		if (!navigation || !showGroups) return [];
		return navigation.groups.filter(g => g.navigationItems.length > 0);
	}
</script>

{#if loading}
	<div class="flex items-center justify-center p-4">
		<div class="animate-spin rounded-full h-6 w-6 border-b-2 border-primary-600"></div>
	</div>
{:else if error}
	<div class="text-red-600 text-sm p-4">
		{error}
	</div>
{:else}
	<nav class={className}>
		{#if showGroups}
			{#each getNavigationGroups() as group}
				<div class="mb-6">
					<h3 class="text-xs font-semibold text-gray-500 uppercase tracking-wider mb-3">
						{group.name}
					</h3>
					<div class="space-y-1">
						{#each group.navigationItems.filter(item => item.isActive) as item}
							{@const IconComponent = getIcon(item.icon)}
							{@const active = isActive(item)}
							
							<a 
								href={item.url}
								class="flex items-center px-3 py-2 text-sm font-medium rounded-lg transition-colors duration-200 {active ? 'bg-primary-100 text-primary-700' : 'text-gray-700 hover:bg-gray-100'}"
							>
								{#if IconComponent}
									<svelte:component this={IconComponent} class="h-5 w-5 mr-3" />
								{/if}
								{item.label}
							</a>
							
							{#if item.children && item.children.length > 0}
								<div class="ml-6 space-y-1">
									{#each item.children.filter(child => child.isActive) as child}
										{@const ChildIconComponent = getIcon(child.icon)}
										{@const childActive = isActive(child)}
										
										<a 
											href={child.url}
											class="flex items-center px-3 py-2 text-sm rounded-lg transition-colors duration-200 {childActive ? 'bg-primary-50 text-primary-600' : 'text-gray-600 hover:bg-gray-50'}"
										>
											{#if ChildIconComponent}
												<svelte:component this={ChildIconComponent} class="h-4 w-4 mr-3" />
											{/if}
											{child.label}
										</a>
									{/each}
								</div>
							{/if}
						{/each}
					</div>
				</div>
			{/each}
		{:else}
			<div class="space-y-1">
				{#each getNavigationItems().filter(item => item.isActive) as item}
					{@const IconComponent = getIcon(item.icon)}
					{@const active = isActive(item)}
					
					<a 
						href={item.url}
						class="flex items-center px-3 py-2 text-sm font-medium rounded-lg transition-colors duration-200 {active ? 'bg-primary-100 text-primary-700' : 'text-gray-700 hover:bg-gray-100'}"
					>
						{#if IconComponent}
							<svelte:component this={IconComponent} class="h-5 w-5 mr-3" />
						{/if}
						{item.label}
					</a>
					
					{#if item.children && item.children.length > 0}
						<div class="ml-6 space-y-1">
							{#each item.children.filter(child => child.isActive) as child}
								{@const ChildIconComponent = getIcon(child.icon)}
								{@const childActive = isActive(child)}
								
								<a 
									href={child.url}
									class="flex items-center px-3 py-2 text-sm rounded-lg transition-colors duration-200 {childActive ? 'bg-primary-50 text-primary-600' : 'text-gray-600 hover:bg-gray-50'}"
								>
									{#if ChildIconComponent}
										<svelte:component this={ChildIconComponent} class="h-4 w-4 mr-3" />
									{/if}
									{child.label}
								</a>
							{/each}
						</div>
					{/if}
				{/each}
			</div>
		{/if}
	</nav>
{/if}
