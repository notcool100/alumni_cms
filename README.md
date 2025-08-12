# Alumni Network Frontend

A modern, responsive web application built with SvelteKit for the Alumni Management System. Features a beautiful UI with Tailwind CSS and seamless integration with the Rust backend API.

## Features

- 🎨 **Modern UI/UX** - Beautiful, responsive design with Tailwind CSS
- 🔐 **Authentication** - Secure login and registration system
- 📱 **Mobile Responsive** - Optimized for all device sizes
- ⚡ **Fast Performance** - Built with SvelteKit for optimal performance
- 🎯 **User Dashboard** - Personalized dashboard with alumni network insights
- 🔍 **Search & Filter** - Advanced search capabilities for alumni and events
- 📊 **Real-time Data** - Live updates from the backend API
- 🎨 **Custom Components** - Reusable UI components with consistent styling

## Tech Stack

- **Framework**: SvelteKit 2.0
- **Language**: TypeScript
- **Styling**: Tailwind CSS
- **Icons**: Lucide Svelte
- **Build Tool**: Vite
- **Package Manager**: pnpm

## Prerequisites

- Node.js 18+ 
- pnpm (recommended) or npm
- Backend API running on `http://localhost:3000`

## Setup

1. **Install dependencies:**
   ```bash
   pnpm install
   ```

2. **Start the development server:**
   ```bash
   pnpm dev
   ```

3. **Open your browser:**
   Navigate to `http://localhost:5173`

## Development

### Available Scripts

- `pnpm dev` - Start development server
- `pnpm build` - Build for production
- `pnpm preview` - Preview production build
- `pnpm check` - Type-check the project
- `pnpm check:watch` - Type-check in watch mode

### Project Structure

```
src/
├── app.css              # Global styles and Tailwind imports
├── app.html             # HTML template
├── app.d.ts             # TypeScript declarations
├── routes/              # SvelteKit routes
│   ├── +layout.svelte   # Root layout
│   ├── +page.svelte     # Homepage
│   ├── auth/            # Authentication pages
│   │   ├── login/
│   │   └── register/
│   └── dashboard/       # User dashboard
├── lib/                 # Shared components and utilities
└── static/              # Static assets
```

### Key Components

#### Authentication Pages
- **Login** (`/auth/login`) - User authentication with email/password
- **Register** (`/auth/register`) - New user registration with validation

#### Dashboard
- **Main Dashboard** (`/dashboard`) - User overview with stats and recent activity
- **Navigation** - Responsive navigation with user profile
- **Stats Cards** - Key metrics and network insights

#### Styling
- **Tailwind CSS** - Utility-first CSS framework
- **Custom Components** - Reusable button, card, and form components
- **Responsive Design** - Mobile-first approach

## API Integration

The frontend communicates with the Rust backend API:

- **Base URL**: `http://localhost:3000`
- **Authentication**: JWT token-based
- **Endpoints**:
  - `POST /api/auth/login` - User login
  - `POST /api/auth/register` - User registration
  - `GET /api/alumni` - List alumni profiles
  - `GET /api/events` - List events

## Environment Configuration

Create a `.env` file in the `fe` directory:

```env
PUBLIC_API_URL=http://localhost:3000
PUBLIC_APP_NAME=Alumni Network
```

## Building for Production

1. **Build the application:**
   ```bash
   pnpm build
   ```

2. **Preview the build:**
   ```bash
   pnpm preview
   ```

3. **Deploy:**
   The built files will be in the `build` directory, ready for deployment.

## Customization

### Colors
The color scheme can be customized in `tailwind.config.js`:

```javascript
theme: {
  extend: {
    colors: {
      primary: {
        50: '#eff6ff',
        // ... other shades
        900: '#1e3a8a',
      }
    }
  }
}
```

### Components
Custom components are defined in `src/app.css` using Tailwind's `@layer components`:

```css
@layer components {
  .btn-primary {
    @apply bg-primary-600 hover:bg-primary-700 text-white font-medium py-2 px-4 rounded-lg;
  }
}
```

## Browser Support

- Chrome (latest)
- Firefox (latest)
- Safari (latest)
- Edge (latest)

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## License

This project is part of the Alumni Network Management System.
