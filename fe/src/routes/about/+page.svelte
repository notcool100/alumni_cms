<script lang="ts">
	import { Users, GraduationCap, Globe, Award, Heart, Target, Users2, Calendar, Building } from 'lucide-svelte';
</script>

<svelte:head>
	<title>About Us - Alumni Network</title>
	<meta name="description" content="Learn about our alumni network, mission, and how we connect graduates worldwide." />
</svelte:head>

<!-- Hero Section -->
<section class="bg-gradient-to-br from-primary-600 to-primary-800 text-white py-20">
	<div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
		<div class="text-center">
			<h1 class="text-4xl md:text-6xl font-bold mb-6">
				About Our
				<span class="text-yellow-300">Alumni Network</span>
			</h1>
			<p class="text-xl md:text-2xl text-primary-100 max-w-4xl mx-auto">
				Connecting graduates across the globe, fostering lifelong relationships, and building a community that supports personal and professional growth.
			</p>
		</div>
	</div>
</section>

<!-- Mission Section -->
<section class="py-16 bg-white">
	<div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
		<div class="grid grid-cols-1 lg:grid-cols-2 gap-12 items-center">
			<div>
				<h2 class="text-3xl md:text-4xl font-bold text-gray-900 mb-6">
					Our Mission
				</h2>
				<p class="text-lg text-gray-600 mb-6">
					We believe that the connections formed during your time at university are just the beginning of a lifelong journey. Our mission is to maintain and strengthen these bonds, creating a global network of alumni who support, inspire, and empower each other.
				</p>
				<p class="text-lg text-gray-600 mb-8">
					Through our platform, we facilitate meaningful connections, provide professional development opportunities, and celebrate the achievements of our diverse alumni community.
				</p>
				<div class="flex items-center space-x-4">
					<div class="flex items-center space-x-2 text-primary-600">
						<Heart class="w-5 h-5" />
						<span class="font-semibold">Lifelong Connections</span>
					</div>
					<div class="flex items-center space-x-2 text-primary-600">
						<Target class="w-5 h-5" />
						<span class="font-semibold">Professional Growth</span>
					</div>
				</div>
			</div>
			<div class="relative">
				<div class="bg-primary-100 rounded-2xl p-8">
					<div class="grid grid-cols-2 gap-6">
						<div class="text-center">
							<div class="bg-white w-16 h-16 rounded-full flex items-center justify-center mx-auto mb-4 shadow-lg">
								<Users class="w-8 h-8 text-primary-600" />
							</div>
							<h3 class="text-2xl font-bold text-gray-900 mb-2">2,500+</h3>
							<p class="text-gray-600">Active Alumni</p>
						</div>
						<div class="text-center">
							<div class="bg-white w-16 h-16 rounded-full flex items-center justify-center mx-auto mb-4 shadow-lg">
								<Globe class="w-8 h-8 text-primary-600" />
							</div>
							<h3 class="text-2xl font-bold text-gray-900 mb-2">45</h3>
							<p class="text-gray-600">Countries</p>
						</div>
						<div class="text-center">
							<div class="bg-white w-16 h-16 rounded-full flex items-center justify-center mx-auto mb-4 shadow-lg">
								<Calendar class="w-8 h-8 text-primary-600" />
							</div>
							<h3 class="text-2xl font-bold text-gray-900 mb-2">50+</h3>
							<p class="text-gray-600">Events Yearly</p>
						</div>
						<div class="text-center">
							<div class="bg-white w-16 h-16 rounded-full flex items-center justify-center mx-auto mb-4 shadow-lg">
								<Award class="w-8 h-8 text-primary-600" />
							</div>
							<h3 class="text-2xl font-bold text-gray-900 mb-2">25</h3>
							<p class="text-gray-600">Years Strong</p>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
</section>

<!-- Values Section -->
<section class="py-16 bg-gray-50">
	<div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
		<div class="text-center mb-12">
			<h2 class="text-3xl md:text-4xl font-bold text-gray-900 mb-4">
				Our Core Values
			</h2>
			<p class="text-xl text-gray-600 max-w-3xl mx-auto">
				These principles guide everything we do and shape the culture of our alumni community.
			</p>
		</div>
		
		<div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
			<div class="bg-white rounded-lg p-8 shadow-md">
				<div class="bg-primary-100 w-12 h-12 rounded-lg flex items-center justify-center mb-6">
					<Users2 class="w-6 h-6 text-primary-600" />
				</div>
				<h3 class="text-xl font-semibold text-gray-900 mb-4">Community First</h3>
				<p class="text-gray-600">
					We prioritize building strong, supportive relationships within our alumni community, fostering a sense of belonging and mutual support.
				</p>
			</div>
			
			<div class="bg-white rounded-lg p-8 shadow-md">
				<div class="bg-primary-100 w-12 h-12 rounded-lg flex items-center justify-center mb-6">
					<GraduationCap class="w-6 h-6 text-primary-600" />
				</div>
				<h3 class="text-xl font-semibold text-gray-900 mb-4">Lifelong Learning</h3>
				<p class="text-gray-600">
					We believe in continuous growth and development, providing opportunities for alumni to learn, grow, and stay current in their fields.
				</p>
			</div>
			
			<div class="bg-white rounded-lg p-8 shadow-md">
				<div class="bg-primary-100 w-12 h-12 rounded-lg flex items-center justify-center mb-6">
					<Globe class="w-6 h-6 text-primary-600" />
				</div>
				<h3 class="text-xl font-semibold text-gray-900 mb-4">Global Perspective</h3>
				<p class="text-gray-600">
					We embrace diversity and global connections, recognizing the value of different perspectives and experiences in our community.
				</p>
			</div>
			
			<div class="bg-white rounded-lg p-8 shadow-md">
				<div class="bg-primary-100 w-12 h-12 rounded-lg flex items-center justify-center mb-6">
					<Heart class="w-6 h-6 text-primary-600" />
				</div>
				<h3 class="text-xl font-semibold text-gray-900 mb-4">Mentorship</h3>
				<p class="text-gray-600">
					We encourage alumni to give back through mentorship, helping current students and fellow alumni navigate their career paths.
				</p>
			</div>
			
			<div class="bg-white rounded-lg p-8 shadow-md">
				<div class="bg-primary-100 w-12 h-12 rounded-lg flex items-center justify-center mb-6">
					<Building class="w-6 h-6 text-primary-600" />
				</div>
				<h3 class="text-xl font-semibold text-gray-900 mb-4">Professional Excellence</h3>
				<p class="text-gray-600">
					We support alumni in achieving their professional goals through networking, career development, and industry connections.
				</p>
			</div>
			
			<div class="bg-white rounded-lg p-8 shadow-md">
				<div class="bg-primary-100 w-12 h-12 rounded-lg flex items-center justify-center mb-6">
					<Award class="w-6 h-6 text-primary-600" />
				</div>
				<h3 class="text-xl font-semibold text-gray-900 mb-4">Innovation</h3>
				<p class="text-gray-600">
					We embrace new ideas and technologies, continuously evolving our platform to better serve our alumni community.
				</p>
			</div>
		</div>
	</div>
</section>

<!-- History Section -->
<section class="py-16 bg-white">
	<div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
		<div class="text-center mb-12">
			<h2 class="text-3xl md:text-4xl font-bold text-gray-900 mb-4">
				Our History
			</h2>
			<p class="text-xl text-gray-600 max-w-3xl mx-auto">
				From humble beginnings to a global network, our journey has been marked by growth, innovation, and unwavering commitment to our alumni community.
			</p>
		</div>
		
		<div class="relative">
			<!-- Timeline -->
			<div class="space-y-12">
				<div class="flex flex-col md:flex-row items-center">
					<div class="md:w-1/2 md:pr-8 mb-6 md:mb-0">
						<div class="bg-primary-100 rounded-lg p-6">
							<h3 class="text-xl font-semibold text-gray-900 mb-2">1999 - Foundation</h3>
							<p class="text-gray-600">
								Our alumni network was established with a simple mission: to keep graduates connected to their alma mater and each other.
							</p>
						</div>
					</div>
					<div class="md:w-1/2 md:pl-8">
						<div class="bg-gray-100 rounded-lg p-6">
							<h3 class="text-xl font-semibold text-gray-900 mb-2">2005 - Digital Transformation</h3>
							<p class="text-gray-600">
								Launched our first online platform, making it easier for alumni to connect regardless of their location.
							</p>
						</div>
					</div>
				</div>
				
				<div class="flex flex-col md:flex-row items-center">
					<div class="md:w-1/2 md:pr-8 mb-6 md:mb-0">
						<div class="bg-gray-100 rounded-lg p-6">
							<h3 class="text-xl font-semibold text-gray-900 mb-2">2012 - Global Expansion</h3>
							<p class="text-gray-600">
								Reached 1,000 alumni members across 20 countries, establishing our first international chapters.
							</p>
						</div>
					</div>
					<div class="md:w-1/2 md:pl-8">
						<div class="bg-primary-100 rounded-lg p-6">
							<h3 class="text-xl font-semibold text-gray-900 mb-2">2018 - Modern Platform</h3>
							<p class="text-gray-600">
								Introduced our current platform with advanced networking features, event management, and professional development tools.
							</p>
						</div>
					</div>
				</div>
				
				<div class="flex flex-col md:flex-row items-center">
					<div class="md:w-1/2 md:pr-8 mb-6 md:mb-0">
						<div class="bg-primary-100 rounded-lg p-6">
							<h3 class="text-xl font-semibold text-gray-900 mb-2">2024 - Today</h3>
							<p class="text-gray-600">
								Over 2,500 active alumni across 45 countries, with comprehensive digital tools and a thriving community.
							</p>
						</div>
					</div>
					<div class="md:w-1/2 md:pl-8">
						<div class="bg-gray-100 rounded-lg p-6">
							<h3 class="text-xl font-semibold text-gray-900 mb-2">Future Vision</h3>
							<p class="text-gray-600">
								Continuing to innovate and expand, with plans for AI-powered networking and enhanced global collaboration tools.
							</p>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
</section>

<!-- Team Section -->
<section class="py-16 bg-gray-50">
	<div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
		<div class="text-center mb-12">
			<h2 class="text-3xl md:text-4xl font-bold text-gray-900 mb-4">
				Our Team
			</h2>
			<p class="text-xl text-gray-600 max-w-3xl mx-auto">
				Dedicated professionals working to strengthen our alumni community and provide exceptional experiences for our members.
			</p>
		</div>
		
		<div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
			<div class="bg-white rounded-lg p-6 text-center shadow-md">
				<div class="w-24 h-24 bg-primary-100 rounded-full flex items-center justify-center mx-auto mb-4">
					<Users class="w-12 h-12 text-primary-600" />
				</div>
				<h3 class="text-xl font-semibold text-gray-900 mb-2">Sarah Johnson</h3>
				<p class="text-primary-600 font-medium mb-2">Alumni Relations Director</p>
				<p class="text-gray-600 text-sm">
					Leading our alumni engagement efforts and building meaningful connections across our global network.
				</p>
			</div>
			
			<div class="bg-white rounded-lg p-6 text-center shadow-md">
				<div class="w-24 h-24 bg-primary-100 rounded-full flex items-center justify-center mx-auto mb-4">
					<Calendar class="w-12 h-12 text-primary-600" />
				</div>
				<h3 class="text-xl font-semibold text-gray-900 mb-2">Michael Chen</h3>
				<p class="text-primary-600 font-medium mb-2">Events Coordinator</p>
				<p class="text-gray-600 text-sm">
					Organizing memorable events and creating opportunities for alumni to connect and network.
				</p>
			</div>
			
			<div class="bg-white rounded-lg p-6 text-center shadow-md">
				<div class="w-24 h-24 bg-primary-100 rounded-full flex items-center justify-center mx-auto mb-4">
					<Building class="w-12 h-12 text-primary-600" />
				</div>
				<h3 class="text-xl font-semibold text-gray-900 mb-2">Emily Rodriguez</h3>
				<p class="text-primary-600 font-medium mb-2">Career Development Manager</p>
				<p class="text-gray-600 text-sm">
					Supporting alumni career growth through mentorship programs and professional development resources.
				</p>
			</div>
		</div>
	</div>
</section>

<!-- CTA Section -->
<section class="py-16 bg-primary-600 text-white">
	<div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 text-center">
		<h2 class="text-3xl md:text-4xl font-bold mb-4">
			Join Our Growing Community
		</h2>
		<p class="text-xl mb-8 text-primary-100 max-w-2xl mx-auto">
			Become part of our vibrant alumni network and start building connections that last a lifetime.
		</p>
		<div class="flex flex-col sm:flex-row gap-4 justify-center">
			<a href="/auth/register" class="btn-primary text-lg px-8 py-3 bg-white text-primary-600 hover:bg-gray-100">
				Join the Network
			</a>
			<a href="/contact" class="btn-secondary text-lg px-8 py-3 border-white text-white hover:bg-white hover:text-primary-600">
				Contact Us
			</a>
		</div>
	</div>
</section>
