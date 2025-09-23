<script lang="ts">
	import { Eye, EyeOff, Mail, Lock, User, Calendar } from 'lucide-svelte';
	import { goto } from '$app/navigation';
	import { apiService } from '$lib/api';

	let firstName = '';
	let lastName = '';
	let email = '';
	let password = '';
	let confirmPassword = '';
	let graduationYear = new Date().getFullYear();
	let showPassword = false;
	let showConfirmPassword = false;
	let loading = false;
	let error = '';
	let success = '';

	// Generate graduation year options (last 50 years to next 5 years)
	const graduationYears = Array.from({ length: 55 }, (_, i) => new Date().getFullYear() - 50 + i);

	async function handleRegister() {
		// Validation
		if (!firstName || !lastName || !email || !password || !confirmPassword) {
			error = 'Please fill in all fields';
			return;
		}

		if (password !== confirmPassword) {
			error = 'Passwords do not match';
			return;
		}

		if (password.length < 8) {
			error = 'Password must be at least 8 characters long';
			return;
		}

		loading = true;
		error = '';
		success = '';

		try {
			const response = await apiService.register({
				first_name: firstName,
				last_name: lastName,
				email,
				password,
			});

			if (response.success && response.data) {
				success = 'Registration successful! Redirecting to dashboard...';
				// Store token if provided
				if (response.data.token) {
					localStorage.setItem('authToken', response.data.token);
					localStorage.setItem('user', JSON.stringify(response.data.user));
					setTimeout(() => goto('/dashboard'), 2000);
				} else {
					setTimeout(() => goto('/auth/login'), 2000);
				}
			} else {
				error = response.message || 'Registration failed';
			}
		} catch (err) {
			error = 'Network error. Please try again.';
			console.error('Registration error:', err);
		} finally {
			loading = false;
		}
	}
</script>

<svelte:head>
	<title>Register - Alumni Network</title>
</svelte:head>

<div class="min-h-screen flex items-center justify-center bg-gray-50 py-12 px-4 sm:px-6 lg:px-8">
	<div class="max-w-md w-full space-y-8">
		<div>
			<div class="mx-auto h-12 w-12 bg-primary-600 rounded-lg flex items-center justify-center">
				<svg class="h-8 w-8 text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24">
					<path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 14l9-5-9-5-9 5 9 5z"></path>
					<path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 14l6.16-3.422a12.083 12.083 0 01.665 6.479A11.952 11.952 0 0012 20.055a11.952 11.952 0 00-6.824-2.998 12.078 12.078 0 01.665-6.479L12 14z"></path>
				</svg>
			</div>
			<h2 class="mt-6 text-center text-3xl font-extrabold text-gray-900">
				Join the Alumni Network
			</h2>
			<p class="mt-2 text-center text-sm text-gray-600">
				Already have an account?
				<a href="/auth/login" class="font-medium text-primary-600 hover:text-primary-500">
					Sign in here
				</a>
			</p>
		</div>

		{#if error}
			<div class="bg-red-50 border border-red-200 text-red-700 px-4 py-3 rounded-lg">
				{error}
			</div>
		{/if}

		{#if success}
			<div class="bg-green-50 border border-green-200 text-green-700 px-4 py-3 rounded-lg">
				{success}
			</div>
		{/if}

		<form class="mt-8 space-y-6" on:submit|preventDefault={handleRegister}>
			<div class="space-y-4">
				<div class="grid grid-cols-2 gap-4">
					<div>
						<label for="firstName" class="form-label">First Name</label>
						<div class="relative">
							<div class="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
								<User class="h-5 w-5 text-gray-400" />
							</div>
							<input
								id="firstName"
								name="firstName"
								type="text"
								required
								bind:value={firstName}
								class="input-field pl-10"
								placeholder="First name"
							/>
						</div>
					</div>

					<div>
						<label for="lastName" class="form-label">Last Name</label>
						<div class="relative">
							<div class="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
								<User class="h-5 w-5 text-gray-400" />
							</div>
							<input
								id="lastName"
								name="lastName"
								type="text"
								required
								bind:value={lastName}
								class="input-field pl-10"
								placeholder="Last name"
							/>
						</div>
					</div>
				</div>

				<div>
					<label for="email" class="form-label">Email address</label>
					<div class="relative">
						<div class="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
							<Mail class="h-5 w-5 text-gray-400" />
						</div>
						<input
							id="email"
							name="email"
							type="email"
							autocomplete="email"
							required
							bind:value={email}
							class="input-field pl-10"
							placeholder="Enter your email"
						/>
					</div>
				</div>

				<div>
					<label for="graduationYear" class="form-label">Graduation Year</label>
					<div class="relative">
						<div class="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
							<Calendar class="h-5 w-5 text-gray-400" />
						</div>
						<select
							id="graduationYear"
							name="graduationYear"
							bind:value={graduationYear}
							class="input-field pl-10"
						>
							{#each graduationYears as year}
								<option value={year}>{year}</option>
							{/each}
						</select>
					</div>
				</div>

				<div>
					<label for="password" class="form-label">Password</label>
					<div class="relative">
						<div class="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
							<Lock class="h-5 w-5 text-gray-400" />
						</div>
						<input
							id="password"
							name="password"
							type={showPassword ? 'text' : 'password'}
							autocomplete="new-password"
							required
							bind:value={password}
							class="input-field pl-10 pr-10"
							placeholder="Create a password"
						/>
						<button
							type="button"
							class="absolute inset-y-0 right-0 pr-3 flex items-center"
							on:click={() => showPassword = !showPassword}
						>
							{#if showPassword}
								<EyeOff class="h-5 w-5 text-gray-400" />
							{:else}
								<Eye class="h-5 w-5 text-gray-400" />
							{/if}
						</button>
					</div>
					<p class="mt-1 text-sm text-gray-500">Must be at least 8 characters long</p>
				</div>

				<div>
					<label for="confirmPassword" class="form-label">Confirm Password</label>
					<div class="relative">
						<div class="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
							<Lock class="h-5 w-5 text-gray-400" />
						</div>
						<input
							id="confirmPassword"
							name="confirmPassword"
							type={showConfirmPassword ? 'text' : 'password'}
							autocomplete="new-password"
							required
							bind:value={confirmPassword}
							class="input-field pl-10 pr-10"
							placeholder="Confirm your password"
						/>
						<button
							type="button"
							class="absolute inset-y-0 right-0 pr-3 flex items-center"
							on:click={() => showConfirmPassword = !showConfirmPassword}
						>
							{#if showConfirmPassword}
								<EyeOff class="h-5 w-5 text-gray-400" />
							{:else}
								<Eye class="h-5 w-5 text-gray-400" />
							{/if}
						</button>
					</div>
				</div>
			</div>

			<div>
				<button
					type="submit"
					disabled={loading}
					class="group relative w-full flex justify-center py-3 px-4 border border-transparent text-sm font-medium rounded-lg text-white bg-primary-600 hover:bg-primary-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-primary-500 disabled:opacity-50 disabled:cursor-not-allowed"
				>
					{#if loading}
						<svg class="animate-spin -ml-1 mr-3 h-5 w-5 text-white" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
							<circle class="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" stroke-width="4"></circle>
							<path class="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
						</svg>
						Creating account...
					{:else}
						Create Account
					{/if}
				</button>
			</div>
		</form>
	</div>
</div>
