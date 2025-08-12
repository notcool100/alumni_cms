-- Alumni Network Database Initialization Script
-- This script is automatically executed when the PostgreSQL container starts

-- Create the database if it doesn't exist
SELECT 'CREATE DATABASE alumni_db'
WHERE NOT EXISTS (SELECT FROM pg_database WHERE datname = 'alumni_db')\gexec

-- Connect to the alumni_db database
\c alumni_db;

-- Create custom types
CREATE TYPE user_role AS ENUM ('admin', 'alumni', 'moderator');
CREATE TYPE registration_status AS ENUM ('confirmed', 'pending', 'cancelled');

-- Create users table
CREATE TABLE IF NOT EXISTS users (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    email VARCHAR(255) UNIQUE NOT NULL,
    password_hash VARCHAR(255) NOT NULL,
    first_name VARCHAR(100) NOT NULL,
    last_name VARCHAR(100) NOT NULL,
    role user_role NOT NULL DEFAULT 'alumni',
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- Create alumni table
CREATE TABLE IF NOT EXISTS alumni (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id UUID NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    graduation_year INTEGER NOT NULL CHECK (graduation_year >= 1950 AND graduation_year <= 2030),
    degree VARCHAR(100) NOT NULL,
    major VARCHAR(100) NOT NULL,
    current_company VARCHAR(255),
    current_position VARCHAR(255),
    location VARCHAR(255),
    bio TEXT,
    linkedin_url VARCHAR(500),
    github_url VARCHAR(500),
    website_url VARCHAR(500),
    profile_image_url VARCHAR(500),
    is_public BOOLEAN DEFAULT true,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- Create events table
CREATE TABLE IF NOT EXISTS events (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    title VARCHAR(255) NOT NULL,
    description TEXT NOT NULL,
    location VARCHAR(255),
    start_date TIMESTAMP WITH TIME ZONE NOT NULL,
    end_date TIMESTAMP WITH TIME ZONE NOT NULL,
    max_attendees INTEGER CHECK (max_attendees > 0),
    current_attendees INTEGER DEFAULT 0,
    is_online BOOLEAN DEFAULT false,
    meeting_url VARCHAR(500),
    created_by UUID NOT NULL REFERENCES users(id),
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    CONSTRAINT valid_event_dates CHECK (end_date > start_date)
);

-- Create event_registrations table
CREATE TABLE IF NOT EXISTS event_registrations (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    event_id UUID NOT NULL REFERENCES events(id) ON DELETE CASCADE,
    user_id UUID NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    registration_date TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    status registration_status DEFAULT 'pending',
    UNIQUE(event_id, user_id)
);

-- Create indexes for better performance
CREATE INDEX IF NOT EXISTS idx_users_email ON users(email);
CREATE INDEX IF NOT EXISTS idx_alumni_user_id ON alumni(user_id);
CREATE INDEX IF NOT EXISTS idx_alumni_graduation_year ON alumni(graduation_year);
CREATE INDEX IF NOT EXISTS idx_alumni_is_public ON alumni(is_public);
CREATE INDEX IF NOT EXISTS idx_events_start_date ON events(start_date);
CREATE INDEX IF NOT EXISTS idx_events_created_by ON events(created_by);
CREATE INDEX IF NOT EXISTS idx_event_registrations_event_id ON event_registrations(event_id);
CREATE INDEX IF NOT EXISTS idx_event_registrations_user_id ON event_registrations(user_id);

-- Create updated_at trigger function
CREATE OR REPLACE FUNCTION update_updated_at_column()
RETURNS TRIGGER AS $$
BEGIN
    NEW.updated_at = NOW();
    RETURN NEW;
END;
$$ language 'plpgsql';

-- Create triggers for updated_at
DROP TRIGGER IF EXISTS update_users_updated_at ON users;
CREATE TRIGGER update_users_updated_at BEFORE UPDATE ON users
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

DROP TRIGGER IF EXISTS update_alumni_updated_at ON alumni;
CREATE TRIGGER update_alumni_updated_at BEFORE UPDATE ON alumni
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

DROP TRIGGER IF EXISTS update_events_updated_at ON events;
CREATE TRIGGER update_events_updated_at BEFORE UPDATE ON events
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

-- Insert default admin user (password: admin123)
INSERT INTO users (id, email, password_hash, first_name, last_name, role) 
VALUES (
    '550e8400-e29b-41d4-a716-446655440000',
    'admin@alumni.com',
    '$2b$12$LQv3c1yqBWVHxkd0LHAkCOYz6TtxMQJqhN8/LewdBPj4J/HS.i8mG',
    'Admin',
    'User',
    'admin'
) ON CONFLICT (email) DO NOTHING;

-- Insert sample alumni data
INSERT INTO alumni (user_id, graduation_year, degree, major, current_company, current_position, location, bio, is_public)
SELECT 
    '550e8400-e29b-41d4-a716-446655440000',
    2020,
    'Bachelor of Science',
    'Computer Science',
    'Tech Corp',
    'Senior Software Engineer',
    'San Francisco, CA',
    'Passionate software engineer with expertise in full-stack development and cloud technologies.',
    true
WHERE NOT EXISTS (SELECT 1 FROM alumni WHERE user_id = '550e8400-e29b-41d4-a716-446655440000');

-- Insert sample events
INSERT INTO events (title, description, location, start_date, end_date, max_attendees, is_online, created_by)
VALUES 
(
    'Alumni Networking Mixer',
    'Join fellow alumni for an evening of networking and professional development.',
    'San Francisco, CA',
    NOW() + INTERVAL '7 days',
    NOW() + INTERVAL '7 days' + INTERVAL '3 hours',
    50,
    false,
    '550e8400-e29b-41d4-a716-446655440000'
),
(
    'Virtual Career Fair',
    'Connect with top companies and explore career opportunities.',
    'Online',
    NOW() + INTERVAL '14 days',
    NOW() + INTERVAL '14 days' + INTERVAL '6 hours',
    100,
    true,
    '550e8400-e29b-41d4-a716-446655440000'
)
ON CONFLICT DO NOTHING;

-- Grant necessary permissions
GRANT ALL PRIVILEGES ON DATABASE alumni_db TO postgres;
GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO postgres;
GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA public TO postgres;

-- Print completion message
SELECT 'Database initialization completed successfully!' as status;
