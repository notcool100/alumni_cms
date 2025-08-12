<script lang="ts">
	import { Mail, Phone, MapPin, Clock, Send, CheckCircle } from 'lucide-svelte';

	let formData = {
		name: '',
		email: '',
		subject: '',
		message: ''
	};
	
	let isSubmitting = false;
	let isSubmitted = false;
	let error = '';

	async function handleSubmit() {
		if (!formData.name || !formData.email || !formData.subject || !formData.message) {
			error = 'Please fill in all fields';
			return;
		}

		if (!isValidEmail(formData.email)) {
			error = 'Please enter a valid email address';
			return;
		}

		isSubmitting = true;
		error = '';

		try {
			// Simulate form submission
			await new Promise(resolve => setTimeout(resolve, 1000));
			
			// In a real application, you would send this to your backend
			console.log('Form submitted:', formData);
			
			isSubmitted = true;
			formData = { name: '', email: '', subject: '', message: '' };
		} catch (err) {
			error = 'Failed to send message. Please try again.';
		} finally {
			isSubmitting = false;
		}
	}

	function isValidEmail(email: string): boolean {
		const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
		return emailRegex.test(email);
	}
</script>

<svelte:head>
	<title>Contact Us - Alumni Network</title>
	<meta name="description" content="Get in touch with our alumni network team. We're here to help you connect and stay engaged with your alma mater." />
</svelte:head>

<!-- Hero Section -->
<section class="bg-gradient-to-br from-primary-600 to-primary-800 text-white py-16">
	<div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
		<div class="text-center">
			<h1 class="text-4xl md:text-5xl font-bold mb-4">Contact Us</h1>
			<p class="text-xl text-primary-100 max-w-3xl mx-auto">
				Have questions or want to get involved? We'd love to hear from you. Reach out to our team and let's connect.
			</p>
		</div>
	</div>
</section>

<!-- Contact Information -->
<section class="py-16 bg-white">
	<div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
		<div class="grid grid-cols-1 lg:grid-cols-2 gap-12">
			<!-- Contact Form -->
			<div>
				<h2 class="text-3xl font-bold text-gray-900 mb-6">Send us a Message</h2>
				
				{#if isSubmitted}
					<div class="bg-green-50 border border-green-200 rounded-lg p-6 mb-6">
						<div class="flex items-center space-x-3">
							<CheckCircle class="w-6 h-6 text-green-600" />
							<div>
								<h3 class="text-lg font-semibold text-green-800">Message Sent!</h3>
								<p class="text-green-700">Thank you for reaching out. We'll get back to you within 24 hours.</p>
							</div>
						</div>
					</div>
				{/if}

				{#if error}
					<div class="bg-red-50 border border-red-200 rounded-lg p-4 mb-6">
						<p class="text-red-700">{error}</p>
					</div>
				{/if}

				<form on:submit|preventDefault={handleSubmit} class="space-y-6">
					<div class="grid grid-cols-1 md:grid-cols-2 gap-6">
						<div>
							<label for="name" class="block text-sm font-medium text-gray-700 mb-2">
								Full Name *
							</label>
							<input
								type="text"
								id="name"
								bind:value={formData.name}
								required
								class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-primary-500 focus:border-primary-500"
								placeholder="Enter your full name"
							/>
						</div>
						<div>
							<label for="email" class="block text-sm font-medium text-gray-700 mb-2">
								Email Address *
							</label>
							<input
								type="email"
								id="email"
								bind:value={formData.email}
								required
								class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-primary-500 focus:border-primary-500"
								placeholder="Enter your email"
							/>
						</div>
					</div>

					<div>
						<label for="subject" class="block text-sm font-medium text-gray-700 mb-2">
							Subject *
						</label>
						<input
							type="text"
							id="subject"
							bind:value={formData.subject}
							required
							class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-primary-500 focus:border-primary-500"
							placeholder="What is this regarding?"
						/>
					</div>

					<div>
						<label for="message" class="block text-sm font-medium text-gray-700 mb-2">
							Message *
						</label>
						<textarea
							id="message"
							bind:value={formData.message}
							required
							rows="6"
							class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-primary-500 focus:border-primary-500"
							placeholder="Tell us more about your inquiry..."
						></textarea>
					</div>

					<button
						type="submit"
						disabled={isSubmitting}
						class="w-full btn-primary py-3 flex items-center justify-center space-x-2 disabled:opacity-50 disabled:cursor-not-allowed"
					>
						{#if isSubmitting}
							<div class="animate-spin rounded-full h-5 w-5 border-b-2 border-white"></div>
							<span>Sending...</span>
						{:else}
							<Send class="w-5 h-5" />
							<span>Send Message</span>
						{/if}
					</button>
				</form>
			</div>

			<!-- Contact Information -->
			<div>
				<h2 class="text-3xl font-bold text-gray-900 mb-6">Get in Touch</h2>
				<p class="text-lg text-gray-600 mb-8">
					We're here to help you stay connected with your alma mater and fellow alumni. Reach out to us through any of the channels below.
				</p>

				<div class="space-y-6">
					<div class="flex items-start space-x-4">
						<div class="bg-primary-100 w-12 h-12 rounded-lg flex items-center justify-center flex-shrink-0">
							<Mail class="w-6 h-6 text-primary-600" />
						</div>
						<div>
							<h3 class="text-lg font-semibold text-gray-900 mb-1">Email Us</h3>
							<p class="text-gray-600 mb-2">alumni@university.edu</p>
							<p class="text-sm text-gray-500">We typically respond within 24 hours</p>
						</div>
					</div>

					<div class="flex items-start space-x-4">
						<div class="bg-primary-100 w-12 h-12 rounded-lg flex items-center justify-center flex-shrink-0">
							<Phone class="w-6 h-6 text-primary-600" />
						</div>
						<div>
							<h3 class="text-lg font-semibold text-gray-900 mb-1">Call Us</h3>
							<p class="text-gray-600 mb-2">+1 (555) 123-4567</p>
							<p class="text-sm text-gray-500">Monday - Friday, 9:00 AM - 5:00 PM EST</p>
						</div>
					</div>

					<div class="flex items-start space-x-4">
						<div class="bg-primary-100 w-12 h-12 rounded-lg flex items-center justify-center flex-shrink-0">
							<MapPin class="w-6 h-6 text-primary-600" />
						</div>
						<div>
							<h3 class="text-lg font-semibold text-gray-900 mb-1">Visit Us</h3>
							<p class="text-gray-600 mb-2">
								123 University Avenue<br>
								City, State 12345<br>
								United States
							</p>
							<p class="text-sm text-gray-500">Alumni Relations Office, Main Campus</p>
						</div>
					</div>

					<div class="flex items-start space-x-4">
						<div class="bg-primary-100 w-12 h-12 rounded-lg flex items-center justify-center flex-shrink-0">
							<Clock class="w-6 h-6 text-primary-600" />
						</div>
						<div>
							<h3 class="text-lg font-semibold text-gray-900 mb-1">Office Hours</h3>
							<p class="text-gray-600 mb-2">
								Monday - Friday: 9:00 AM - 5:00 PM EST<br>
								Saturday: 10:00 AM - 2:00 PM EST<br>
								Sunday: Closed
							</p>
							<p class="text-sm text-gray-500">Holiday hours may vary</p>
						</div>
					</div>
				</div>

				<!-- Quick Links -->
				<div class="mt-8 p-6 bg-gray-50 rounded-lg">
					<h3 class="text-lg font-semibold text-gray-900 mb-4">Quick Links</h3>
					<div class="space-y-2">
						<a href="/auth/register" class="block text-primary-600 hover:text-primary-700">
							→ Join the Alumni Network
						</a>
						<a href="/events" class="block text-primary-600 hover:text-primary-700">
							→ View Upcoming Events
						</a>
						<a href="/alumni" class="block text-primary-600 hover:text-primary-700">
							→ Browse Alumni Directory
						</a>
						<a href="/about" class="block text-primary-600 hover:text-primary-700">
							→ Learn More About Us
						</a>
					</div>
				</div>
			</div>
		</div>
	</div>
</section>

<!-- FAQ Section -->
<section class="py-16 bg-gray-50">
	<div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
		<div class="text-center mb-12">
			<h2 class="text-3xl md:text-4xl font-bold text-gray-900 mb-4">
				Frequently Asked Questions
			</h2>
			<p class="text-xl text-gray-600 max-w-3xl mx-auto">
				Find answers to common questions about our alumni network and services.
			</p>
		</div>

		<div class="grid grid-cols-1 md:grid-cols-2 gap-8">
			<div class="bg-white rounded-lg p-6 shadow-md">
				<h3 class="text-lg font-semibold text-gray-900 mb-3">How do I join the alumni network?</h3>
				<p class="text-gray-600">
					Simply click the "Join Network" button and complete the registration form. You'll need to provide your graduation details and create an account.
				</p>
			</div>

			<div class="bg-white rounded-lg p-6 shadow-md">
				<h3 class="text-lg font-semibold text-gray-900 mb-3">Is there a membership fee?</h3>
				<p class="text-gray-600">
					Basic membership is free for all alumni. We offer premium features and exclusive events for paid members.
				</p>
			</div>

			<div class="bg-white rounded-lg p-6 shadow-md">
				<h3 class="text-lg font-semibold text-gray-900 mb-3">How can I update my profile?</h3>
				<p class="text-gray-600">
					Once logged in, you can update your profile information, including your current job, location, and contact details.
				</p>
			</div>

			<div class="bg-white rounded-lg p-6 shadow-md">
				<h3 class="text-lg font-semibold text-gray-900 mb-3">Can I organize an event?</h3>
				<p class="text-gray-600">
					Yes! We encourage alumni to organize events. Contact us to discuss your event idea and get support.
				</p>
			</div>

			<div class="bg-white rounded-lg p-6 shadow-md">
				<h3 class="text-lg font-semibold text-gray-900 mb-3">How do I connect with other alumni?</h3>
				<p class="text-gray-600">
					Browse our alumni directory, attend events, or join our online community forums to connect with fellow graduates.
				</p>
			</div>

			<div class="bg-white rounded-lg p-6 shadow-md">
				<h3 class="text-lg font-semibold text-gray-900 mb-3">What if I forgot my password?</h3>
				<p class="text-gray-600">
					Use the "Forgot Password" link on the login page to reset your password. You'll receive instructions via email.
				</p>
			</div>
		</div>
	</div>
</section>

<!-- CTA Section -->
<section class="py-16 bg-primary-600 text-white">
	<div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 text-center">
		<h2 class="text-3xl md:text-4xl font-bold mb-4">
			Ready to Get Started?
		</h2>
		<p class="text-xl mb-8 text-primary-100 max-w-2xl mx-auto">
			Join thousands of alumni who are already connected and engaged with our network.
		</p>
		<div class="flex flex-col sm:flex-row gap-4 justify-center">
			<a href="/auth/register" class="btn-primary text-lg px-8 py-3 bg-white text-primary-600 hover:bg-gray-100">
				Join the Network
			</a>
			<a href="/events" class="btn-secondary text-lg px-8 py-3 border-white text-white hover:bg-white hover:text-primary-600">
				View Events
			</a>
		</div>
	</div>
</section>
