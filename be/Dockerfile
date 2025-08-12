# Use the official Rust image as a base
FROM rust:1.75-slim as builder

# Set working directory
WORKDIR /app

# Copy the manifests
COPY Cargo.toml Cargo.lock ./

# Create a dummy main.rs to build dependencies
RUN mkdir src && echo "fn main() {}" > src/main.rs

# Build dependencies
RUN cargo build --release

# Remove the dummy main.rs
RUN rm src/main.rs

# Copy the real source code
COPY . .

# Build the application
RUN cargo build --release

# Runtime stage
FROM debian:bookworm-slim

# Install runtime dependencies
RUN apt-get update && apt-get install -y \
    ca-certificates \
    && rm -rf /var/lib/apt/lists/*

# Create a non-root user
RUN useradd -m -u 1001 -U app

# Set working directory
WORKDIR /app

# Copy the binary from builder stage
COPY --from=builder /app/target/release/alumni-backend /app/alumni-backend

# Copy migrations
COPY --from=builder /app/migrations /app/migrations

# Change ownership to the app user
RUN chown -R app:app /app

# Switch to the app user
USER app

# Expose port
EXPOSE 3000

# Run the binary
CMD ["./alumni-backend"]
