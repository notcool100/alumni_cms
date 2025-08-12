#!/bin/bash

# Alumni Network Setup Script
echo "🚀 Setting up Alumni Network Management System..."

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# Function to print colored output
print_status() {
    echo -e "${BLUE}[INFO]${NC} $1"
}

print_success() {
    echo -e "${GREEN}[SUCCESS]${NC} $1"
}

print_warning() {
    echo -e "${YELLOW}[WARNING]${NC} $1"
}

print_error() {
    echo -e "${RED}[ERROR]${NC} $1"
}

# Check if required tools are installed
check_requirements() {
    print_status "Checking system requirements..."
    
    # Check Rust
    if ! command -v rustc &> /dev/null; then
        print_error "Rust is not installed. Please install Rust from https://rustup.rs/"
        exit 1
    fi
    
    # Check Node.js
    if ! command -v node &> /dev/null; then
        print_error "Node.js is not installed. Please install Node.js from https://nodejs.org/"
        exit 1
    fi
    
    # Check PostgreSQL
    if ! command -v psql &> /dev/null; then
        print_error "PostgreSQL is not installed. Please install PostgreSQL from https://www.postgresql.org/download/"
        exit 1
    fi
    
    # Check pnpm
    if ! command -v pnpm &> /dev/null; then
        print_warning "pnpm is not installed. Installing pnpm..."
        npm install -g pnpm
    fi
    
    print_success "All requirements are met!"
}

# Setup database
setup_database() {
    print_status "Setting up PostgreSQL database..."
    
    # Check if database exists
    if psql -lqt | cut -d \| -f 1 | grep -qw alumni_db; then
        print_warning "Database 'alumni_db' already exists"
    else
        createdb alumni_db
        print_success "Database 'alumni_db' created successfully"
    fi
}

# Setup backend
setup_backend() {
    print_status "Setting up Rust backend..."
    
    cd be
    
    # Create .env file
    if [ ! -f .env ]; then
        cat > .env << EOF
DATABASE_URL=postgresql://postgres:yourdad@localhost:5432/alumni_db
JWT_SECRET=your-super-secret-jwt-key-change-in-production
PORT=3000
ENVIRONMENT=development
RUST_LOG=info
EOF
        print_success "Created .env file for backend"
    else
        print_warning ".env file already exists in backend"
    fi
    
    # Install SQLx CLI
    if ! command -v sqlx &> /dev/null; then
        print_status "Installing SQLx CLI..."
        cargo install sqlx-cli
    fi
    
    # Run database migrations
    print_status "Running database migrations..."
    sqlx migrate run
    
    print_success "Backend setup completed!"
    cd ..
}

# Setup frontend
setup_frontend() {
    print_status "Setting up Svelte frontend..."
    
    cd fe
    
    # Install dependencies
    print_status "Installing frontend dependencies..."
    pnpm install
    
    # Create .env file
    if [ ! -f .env ]; then
        cat > .env << EOF
PUBLIC_API_URL=http://localhost:3000
PUBLIC_APP_NAME=Alumni Network
EOF
        print_success "Created .env file for frontend"
    else
        print_warning ".env file already exists in frontend"
    fi
    
    print_success "Frontend setup completed!"
    cd ..
}

# Main setup function
main() {
    echo "🎓 Alumni Network Management System Setup"
    echo "=========================================="
    
    check_requirements
    setup_database
    setup_backend
    setup_frontend
    
    echo ""
    echo "🎉 Setup completed successfully!"
    echo ""
    echo "Next steps:"
    echo "1. Start the backend: cd be && cargo run"
    echo "2. Start the frontend: cd fe && pnpm dev"
    echo "3. Open http://localhost:5173 in your browser"
    echo ""
    echo "Default admin credentials:"
    echo "Email: admin@alumni.com"
    echo "Password: admin123"
    echo ""
}

# Run main function
main
