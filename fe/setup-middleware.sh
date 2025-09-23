#!/bin/bash

# Frontend Middleware Setup Script
echo "🚀 Setting up Frontend Middleware System..."

# Install dependencies
echo "📦 Installing dependencies..."
pnpm install

# Check if .env file exists
if [ ! -f .env ]; then
    echo "📝 Creating .env file..."
    cat > .env << EOF
# Frontend Environment Variables

# JWT Secret (must match backend)
JWT_SECRET=your-super-secret-jwt-key-here

# API Configuration
VITE_API_BASE_URL=http://localhost:5037

# Development Settings
VITE_DEV_MODE=true
VITE_DEBUG_MIDDLEWARE=false

# Security Settings
VITE_COOKIE_SECURE=false  # Set to true in production
VITE_COOKIE_SAMESITE=Strict
EOF
    echo "✅ .env file created. Please update JWT_SECRET to match your backend."
else
    echo "✅ .env file already exists."
fi

# Check if hooks.server.ts exists
if [ -f src/hooks.server.ts ]; then
    echo "✅ Server hooks already configured."
else
    echo "❌ Server hooks not found. Please ensure hooks.server.ts is created."
fi

# Check if middleware directory exists
if [ -d src/lib/middleware ]; then
    echo "✅ Client middleware already configured."
else
    echo "❌ Client middleware not found. Please ensure middleware files are created."
fi

echo ""
echo "🎉 Middleware setup complete!"
echo ""
echo "📋 Next steps:"
echo "1. Update JWT_SECRET in .env to match your backend"
echo "2. Ensure your backend is running on http://localhost:5037"
echo "3. Test the middleware by accessing protected routes"
echo ""
echo "📚 Documentation:"
echo "- See MIDDLEWARE_README.md for detailed information"
echo "- Check src/hooks.server.ts for server-side configuration"
echo "- Check src/lib/middleware/ for client-side utilities"
echo ""
echo "🧪 Testing:"
echo "- Try accessing /admin without authentication"
echo "- Try accessing /dashboard without authentication"
echo "- Login and verify role-based redirects work"
echo ""
echo "🔧 Troubleshooting:"
echo "- Check browser console for errors"
echo "- Verify JWT_SECRET matches backend"
echo "- Ensure all dependencies are installed"

