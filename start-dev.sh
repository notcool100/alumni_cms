#!/bin/bash

echo "Starting Alumni Pro Development Environment..."
echo "=============================================="

# Function to check if a port is in use
check_port() {
    if lsof -Pi :$1 -sTCP:LISTEN -t >/dev/null ; then
        echo "Port $1 is already in use"
        return 1
    else
        echo "Port $1 is available"
        return 0
    fi
}

# Check if ports are available
echo "Checking port availability..."
check_port 5037 || exit 1
check_port 5173 || exit 1

echo ""
echo "Starting .NET Backend on http://localhost:5037"
echo "Starting Svelte Frontend on http://localhost:5173"
echo ""

# Start backend in background
echo "Starting backend..."
cd alumni-backend-netcore/WebAPI
dotnet run &
BACKEND_PID=$!

# Wait a moment for backend to start
sleep 5

# Start frontend in background
echo "Starting frontend..."
cd ../../fe
npm run dev &
FRONTEND_PID=$!

echo ""
echo "Development servers started!"
echo "Backend: http://localhost:5037"
echo "Frontend: http://localhost:5173"
echo "Swagger: http://localhost:5037/swagger"
echo ""
echo "Press Ctrl+C to stop both servers"

# Function to cleanup on exit
cleanup() {
    echo ""
    echo "Stopping development servers..."
    kill $BACKEND_PID 2>/dev/null
    kill $FRONTEND_PID 2>/dev/null
    exit 0
}

# Set up signal handlers
trap cleanup SIGINT SIGTERM

# Wait for both processes
wait
