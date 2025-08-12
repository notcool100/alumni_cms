<script lang="ts">
	import { Users, Calendar, MapPin, GraduationCap, Building, Globe } from 'lucide-svelte';
</script>

<svelte:head>
	<title>Alumni Network - Connect with Your Community</title>
	<meta name="description" content="Connect with fellow alumni, discover opportunities, and stay updated with your alma mater." />
</svelte:head>

<!-- Hero Section -->
<section class="relative bg-gradient-to-br from-primary-600 to-primary-800 text-white">
	<div class="absolute inset-0 bg-black/20"></div>
	<div class="relative max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-24">
		<div class="text-center">
			<h1 class="text-4xl md:text-6xl font-bold mb-6">
				Welcome to Your
				<span class="text-yellow-300">Alumni Network</span>
			</h1>
			<p class="text-xl md:text-2xl mb-8 text-primary-100 max-w-3xl mx-auto">
				Connect with fellow graduates, discover career opportunities, and stay updated with your alma mater's latest events and achievements.
			</p>
			<div class="flex flex-col sm:flex-row gap-4 justify-center">
				<a href="/auth/login" class="btn-primary text-lg px-8 py-3">
					Sign In
				</a>
				<a href="/auth/register" class="btn-secondary text-lg px-8 py-3">
					Join Network
				</a>
			</div>
		</div>
	</div>
</section>

<!-- Stats Section -->
<section class="py-16 bg-white">
	<div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
		<div class="grid grid-cols-1 md:grid-cols-3 gap-8">
			<div class="text-center">
				<div class="bg-primary-100 w-16 h-16 rounded-full flex items-center justify-center mx-auto mb-4">
					<Users class="w-8 h-8 text-primary-600" />
				</div>
				<h3 class="text-3xl font-bold text-gray-900 mb-2">2,500+</h3>
				<p class="text-gray-600">Active Alumni</p>
			</div>
			<div class="text-center">
				<div class="bg-primary-100 w-16 h-16 rounded-full flex items-center justify-center mx-auto mb-4">
					<Calendar class="w-8 h-8 text-primary-600" />
				</div>
				<h3 class="text-3xl font-bold text-gray-900 mb-2">50+</h3>
				<p class="text-gray-600">Events This Year</p>
			</div>
			<div class="text-center">
				<div class="bg-primary-100 w-16 h-16 rounded-full flex items-center justify-center mx-auto mb-4">
					<Globe class="w-8 h-8 text-primary-600" />
				</div>
				<h3 class="text-3xl font-bold text-gray-900 mb-2">45</h3>
				<p class="text-gray-600">Countries Represented</p>
			</div>
		</div>
	</div>
</section>

<!-- Features Section -->
<section class="py-16 bg-gray-50">
	<div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
		<div class="text-center mb-12">
			<h2 class="text-3xl md:text-4xl font-bold text-gray-900 mb-4">
				Why Join Our Alumni Network?
			</h2>
			<p class="text-xl text-gray-600 max-w-3xl mx-auto">
				Stay connected with your alma mater and fellow graduates through our comprehensive platform.
			</p>
		</div>
		
		<div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
			<div class="card">
				<div class="bg-primary-100 w-12 h-12 rounded-lg flex items-center justify-center mb-4">
					<Users class="w-6 h-6 text-primary-600" />
				</div>
				<h3 class="text-xl font-semibold text-gray-900 mb-3">Network & Connect</h3>
				<p class="text-gray-600">
					Connect with fellow alumni across the globe. Share experiences, mentor others, and build lasting relationships.
				</p>
			</div>
			
			<div class="card">
				<div class="bg-primary-100 w-12 h-12 rounded-lg flex items-center justify-center mb-4">
					<Building class="w-6 h-6 text-primary-600" />
				</div>
				<h3 class="text-xl font-semibold text-gray-900 mb-3">Career Opportunities</h3>
				<p class="text-gray-600">
					Discover job opportunities, internships, and career advice from successful alumni in your field.
				</p>
			</div>
			
			<div class="card">
				<div class="bg-primary-100 w-12 h-12 rounded-lg flex items-center justify-center mb-4">
					<Calendar class="w-6 h-6 text-primary-600" />
				</div>
				<h3 class="text-xl font-semibold text-gray-900 mb-3">Events & Reunions</h3>
				<p class="text-gray-600">
					Stay updated with upcoming events, reunions, and networking opportunities in your area.
				</p>
			</div>
			
			<div class="card">
				<div class="bg-primary-100 w-12 h-12 rounded-lg flex items-center justify-center mb-4">
					<GraduationCap class="w-6 h-6 text-primary-600" />
				</div>
				<h3 class="text-xl font-semibold text-gray-900 mb-3">Lifelong Learning</h3>
				<p class="text-gray-600">
					Access exclusive workshops, webinars, and educational resources to continue your professional development.
				</p>
			</div>
			
			<div class="card">
				<div class="bg-primary-100 w-12 h-12 rounded-lg flex items-center justify-center mb-4">
					<MapPin class="w-6 h-6 text-primary-600" />
				</div>
				<h3 class="text-xl font-semibold text-gray-900 mb-3">Local Chapters</h3>
				<p class="text-gray-600">
					Join local alumni chapters and participate in regional events and networking activities.
				</p>
			</div>
			
			<div class="card">
				<div class="bg-primary-100 w-12 h-12 rounded-lg flex items-center justify-center mb-4">
					<Globe class="w-6 h-6 text-primary-600" />
				</div>
				<h3 class="text-xl font-semibold text-gray-900 mb-3">Global Community</h3>
				<p class="text-gray-600">
					Be part of a diverse global community representing various industries and backgrounds.
				</p>
			</div>
		</div>
	</div>
</section>

<!-- CTA Section -->
<section class="py-16 bg-primary-600 text-white">
	<div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 text-center">
		<h2 class="text-3xl md:text-4xl font-bold mb-4">
			Ready to Join Your Alumni Network?
		</h2>
		<p class="text-xl mb-8 text-primary-100 max-w-2xl mx-auto">
			Create your profile today and start connecting with fellow alumni from around the world.
		</p>
		<a href="/auth/register" class="btn-primary text-lg px-8 py-3 bg-white text-primary-600 hover:bg-gray-100">
			Get Started Now
		</a>
	</div>
</section>
