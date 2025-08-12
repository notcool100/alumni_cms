<script lang="ts">
	import { onMount } from 'svelte';
	import { 
		Search, 
		Plus, 
		Edit, 
		Trash2, 
		Eye,
		Shield,
		User,
		Mail,
		Calendar,
		ChevronLeft,
		ChevronRight,
		Filter,
		MoreHorizontal
	} from 'lucide-svelte';
	
	let users: any[] = [];
	let filteredUsers: any[] = [];
	let loading = true;
	let searchTerm = '';
	let selectedRole = '';
	let selectedStatus = '';
	let currentPage = 1;
	let itemsPerPage = 10;
	let showDeleteModal = false;
	let userToDelete: any = null;
	
	const roles = [
		{ value: '', label: 'All Roles' },
		{ value: 'admin', label: 'Administrator' },
		{ value: 'moderator', label: 'Moderator' },
		{ value: 'alumni', label: 'Alumni' }
	];
	
	const statusOptions = [
		{ value: '', label: 'All Status' },
		{ value: 'active', label: 'Active' },
		{ value: 'inactive', label: 'Inactive' },
		{ value: 'suspended', label: 'Suspended' }
	];
	
	onMount(() => {
		loadUsers();
	});
	
	async function loadUsers() {
		try {
			// Mock data for demo - replace with actual API call
			users = [
				{
					id: '1',
					first_name: 'John',
					last_name: 'Doe',
					email: 'john.doe@example.com',
					role: 'admin',
					status: 'active',
					created_at: '2023-01-15T10:00:00Z',
					last_login: '2024-01-15T14:30:00Z',
					profile_complete: true
				},
				{
					id: '2',
					first_name: 'Jane',
					last_name: 'Smith',
					email: 'jane.smith@example.com',
					role: 'moderator',
					status: 'active',
					created_at: '2023-03-20T09:15:00Z',
					last_login: '2024-01-14T16:45:00Z',
					profile_complete: true
				},
				{
					id: '3',
					first_name: 'Mike',
					last_name: 'Johnson',
					email: 'mike.johnson@example.com',
					role: 'alumni',
					status: 'active',
					created_at: '2023-06-10T11:30:00Z',
					last_login: '2024-01-13T10:20:00Z',
					profile_complete: false
				},
				{
					id: '4',
					first_name: 'Sarah',
					last_name: 'Wilson',
					email: 'sarah.wilson@example.com',
					role: 'alumni',
					status: 'inactive',
					created_at: '2023-08-05T13:45:00Z',
					last_login: '2023-12-20T09:10:00Z',
					profile_complete: true
				},
				{
					id: '5',
					first_name: 'David',
					last_name: 'Brown',
					email: 'david.brown@example.com',
					role: 'moderator',
					status: 'suspended',
					created_at: '2023-04-12T15:20:00Z',
					last_login: '2024-01-10T11:30:00Z',
					profile_complete: true
				}
			];
			filteredUsers = users;
		} catch (error) {
			console.error('Error loading users:', error);
		} finally {
			loading = false;
		}
	}
	
	function filterUsers() {
		filteredUsers = users.filter(user => {
			const matchesSearch = !searchTerm || 
				user.first_name.toLowerCase().includes(searchTerm.toLowerCase()) ||
				user.last_name.toLowerCase().includes(searchTerm.toLowerCase()) ||
				user.email.toLowerCase().includes(searchTerm.toLowerCase());
			
			const matchesRole = !selectedRole || user.role === selectedRole;
			const matchesStatus = !selectedStatus || user.status === selectedStatus;
			
			return matchesSearch && matchesRole && matchesStatus;
		});
		currentPage = 1;
	}
	
	function getPaginatedUsers() {
		const start = (currentPage - 1) * itemsPerPage;
		const end = start + itemsPerPage;
		return filteredUsers.slice(start, end);
	}
	
	function getTotalPages() {
		return Math.ceil(filteredUsers.length / itemsPerPage);
	}
	
	function deleteUser(user: any) {
		userToDelete = user;
		showDeleteModal = true;
	}
	
	async function confirmDelete() {
		if (!userToDelete) return;
		
		try {
			// Mock delete - replace with actual API call
			users = users.filter(user => user.id !== userToDelete.id);
			filterUsers();
		} catch (error) {
			console.error('Error deleting user:', error);
		} finally {
			showDeleteModal = false;
			userToDelete = null;
		}
	}
	
	function formatDate(dateString: string) {
		return new Date(dateString).toLocaleDateString('en-US', {
			year: 'numeric',
			month: 'short',
			day: 'numeric'
		});
	}
	
	function getRoleColor(role: string) {
		switch (role) {
			case 'admin':
				return 'bg-red-100 text-red-800';
			case 'moderator':
				return 'bg-blue-100 text-blue-800';
			case 'alumni':
				return 'bg-green-100 text-green-800';
			default:
				return 'bg-gray-100 text-gray-800';
		}
	}
	
	function getStatusColor(status: string) {
		switch (status) {
			case 'active':
				return 'bg-green-100 text-green-800';
			case 'inactive':
				return 'bg-gray-100 text-gray-800';
			case 'suspended':
				return 'bg-red-100 text-red-800';
			default:
				return 'bg-gray-100 text-gray-800';
		}
	}
	
	$: {
		filterUsers();
	}
</script>

<svelte:head>
	<title>User Management - Admin CMS</title>
</svelte:head>

<!-- Page Header -->
<div class="mb-8">
	<div class="flex items-center justify-between">
		<div>
			<h1 class="text-3xl font-bold text-gray-900">User Management</h1>
			<p class="text-gray-600 mt-2">Manage user accounts and permissions</p>
		</div>
		<a href="/admin/users/new" class="btn-primary flex items-center">
			<Plus class="h-5 w-5 mr-2" />
			Add User
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
					placeholder="Search users..."
					bind:value={searchTerm}
					class="input-field pl-10"
				/>
			</div>
			
			<select bind:value={selectedRole} class="input-field">
				{#each roles as role}
					<option value={role.value}>{role.label}</option>
				{/each}
			</select>
			
			<select bind:value={selectedStatus} class="input-field">
				{#each statusOptions as status}
					<option value={status.value}>{status.label}</option>
				{/each}
			</select>
			
			<button class="btn-secondary flex items-center justify-center">
				<Filter class="h-5 w-5 mr-2" />
				Advanced
			</button>
		</div>
	</div>

	<!-- Users Table -->
	<div class="card">
		<div class="overflow-x-auto">
			<table class="min-w-full divide-y divide-gray-200">
				<thead class="bg-gray-50">
					<tr>
						<th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
							User
						</th>
						<th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
							Role
						</th>
						<th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
							Status
						</th>
						<th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
							Last Login
						</th>
						<th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
							Profile
						</th>
						<th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
							Actions
						</th>
					</tr>
				</thead>
				<tbody class="bg-white divide-y divide-gray-200">
					{#each getPaginatedUsers() as user}
						<tr class="hover:bg-gray-50">
							<td class="px-6 py-4 whitespace-nowrap">
								<div class="flex items-center">
									<div class="w-10 h-10 bg-primary-600 rounded-full flex items-center justify-center">
										<span class="text-white font-medium">
											{user.first_name[0]}{user.last_name[0]}
										</span>
									</div>
									<div class="ml-4">
										<div class="text-sm font-medium text-gray-900">
											{user.first_name} {user.last_name}
										</div>
										<div class="text-sm text-gray-500 flex items-center">
											<Mail class="h-4 w-4 mr-1" />
											{user.email}
										</div>
									</div>
								</div>
							</td>
							<td class="px-6 py-4 whitespace-nowrap">
								<span class="inline-flex px-2 py-1 text-xs font-semibold rounded-full {getRoleColor(user.role)}">
									{user.role}
								</span>
							</td>
							<td class="px-6 py-4 whitespace-nowrap">
								<span class="inline-flex px-2 py-1 text-xs font-semibold rounded-full {getStatusColor(user.status)}">
									{user.status}
								</span>
							</td>
							<td class="px-6 py-4 whitespace-nowrap">
								<div class="text-sm text-gray-900">{formatDate(user.last_login)}</div>
								<div class="text-sm text-gray-500">Member since {formatDate(user.created_at)}</div>
							</td>
							<td class="px-6 py-4 whitespace-nowrap">
								<span class="inline-flex items-center px-2 py-1 rounded-full text-xs font-medium {user.profile_complete ? 'bg-green-100 text-green-800' : 'bg-yellow-100 text-yellow-800'}">
									{user.profile_complete ? 'Complete' : 'Incomplete'}
								</span>
							</td>
							<td class="px-6 py-4 whitespace-nowrap text-sm font-medium">
								<div class="flex items-center space-x-2">
									<button class="text-blue-600 hover:text-blue-900">
										<Eye class="h-4 w-4" />
									</button>
									<a href="/admin/users/{user.id}/edit" class="text-green-600 hover:text-green-900">
										<Edit class="h-4 w-4" />
									</a>
									{#if user.role !== 'admin'}
										<button on:click={() => deleteUser(user)} class="text-red-600 hover:text-red-900">
											<Trash2 class="h-4 w-4" />
										</button>
									{/if}
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
					Showing {((currentPage - 1) * itemsPerPage) + 1} to {Math.min(currentPage * itemsPerPage, filteredUsers.length)} of {filteredUsers.length} results
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

	<!-- User Statistics -->
	<div class="grid grid-cols-1 md:grid-cols-3 gap-6 mt-8">
		<div class="card">
			<div class="flex items-center">
				<div class="bg-red-100 p-3 rounded-lg">
					<Shield class="h-6 w-6 text-red-600" />
				</div>
				<div class="ml-4">
					<p class="text-sm font-medium text-gray-600">Administrators</p>
					<p class="text-2xl font-bold text-gray-900">
						{users.filter(u => u.role === 'admin').length}
					</p>
				</div>
			</div>
		</div>
		
		<div class="card">
			<div class="flex items-center">
				<div class="bg-blue-100 p-3 rounded-lg">
					<User class="h-6 w-6 text-blue-600" />
				</div>
				<div class="ml-4">
					<p class="text-sm font-medium text-gray-600">Moderators</p>
					<p class="text-2xl font-bold text-gray-900">
						{users.filter(u => u.role === 'moderator').length}
					</p>
				</div>
			</div>
		</div>
		
		<div class="card">
			<div class="flex items-center">
				<div class="bg-green-100 p-3 rounded-lg">
					<User class="h-6 w-6 text-green-600" />
				</div>
				<div class="ml-4">
					<p class="text-sm font-medium text-gray-600">Alumni Users</p>
					<p class="text-2xl font-bold text-gray-900">
						{users.filter(u => u.role === 'alumni').length}
					</p>
				</div>
			</div>
		</div>
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
							<h3 class="text-lg leading-6 font-medium text-gray-900">Delete User</h3>
							<div class="mt-2">
								<p class="text-sm text-gray-500">
									Are you sure you want to delete {userToDelete?.first_name} {userToDelete?.last_name}? This action cannot be undone.
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
						on:click={() => { showDeleteModal = false; userToDelete = null; }}
						class="mt-3 w-full inline-flex justify-center rounded-md border border-gray-300 shadow-sm px-4 py-2 bg-white text-base font-medium text-gray-700 hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-primary-500 sm:mt-0 sm:ml-3 sm:w-auto sm:text-sm"
					>
						Cancel
					</button>
				</div>
			</div>
		</div>
	</div>
{/if}
