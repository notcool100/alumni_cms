<script>
	import '../app.css';
	import { onMount } from 'svelte';
	import { authStore } from '$lib/stores/auth';

	onMount(() => {
		authStore.init();
	});
</script>

<div class="min-h-screen bg-gray-50">
	<slot />
</div>
