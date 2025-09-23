#!/bin/bash

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# API base URL
API_URL="http://localhost:5000/api"

echo -e "${YELLOW}🧪 Testing Alumni Backend API (.NET)${NC}"
echo "=================================="

# Test health endpoint
echo -e "\n${YELLOW}1. Testing Health Check${NC}"
curl -s "$API_URL/health" | jq '.' || echo "Health check failed or jq not installed"

# Test user registration
echo -e "\n${YELLOW}2. Testing User Registration${NC}"
REGISTER_RESPONSE=$(curl -s -X POST "$API_URL/auth/register" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "test@example.com",
    "password": "password123",
    "firstName": "John",
    "lastName": "Doe"
  }')

echo "$REGISTER_RESPONSE" | jq '.' || echo "$REGISTER_RESPONSE"

# Extract token from registration response
TOKEN=$(echo "$REGISTER_RESPONSE" | jq -r '.data.token' 2>/dev/null)

if [ "$TOKEN" != "null" ] && [ "$TOKEN" != "" ]; then
    echo -e "\n${GREEN}✅ Registration successful, token obtained${NC}"
    
    # Test user profile
    echo -e "\n${YELLOW}3. Testing Get User Profile${NC}"
    curl -s -X GET "$API_URL/auth/me" \
      -H "Authorization: Bearer $TOKEN" | jq '.' || echo "Profile request failed"
    
    # Test create alumni profile
    echo -e "\n${YELLOW}4. Testing Create Alumni Profile${NC}"
    ALUMNI_RESPONSE=$(curl -s -X POST "$API_URL/alumni" \
      -H "Content-Type: application/json" \
      -H "Authorization: Bearer $TOKEN" \
      -d '{
        "graduationYear": 2020,
        "degree": "Bachelor of Science",
        "major": "Computer Science",
        "currentCompany": "Tech Corp",
        "currentPosition": "Software Engineer",
        "location": "San Francisco, CA",
        "bio": "Passionate software engineer with expertise in web development.",
        "linkedinUrl": "https://linkedin.com/in/johndoe",
        "githubUrl": "https://github.com/johndoe",
        "websiteUrl": "https://johndoe.dev",
        "isPublic": true
      }')
    
    echo "$ALUMNI_RESPONSE" | jq '.' || echo "$ALUMNI_RESPONSE"
    
    # Test create event
    echo -e "\n${YELLOW}5. Testing Create Event${NC}"
    EVENT_RESPONSE=$(curl -s -X POST "$API_URL/events" \
      -H "Content-Type: application/json" \
      -H "Authorization: Bearer $TOKEN" \
      -d '{
        "title": "Alumni Meetup 2024",
        "description": "Annual alumni networking event",
        "location": "San Francisco, CA",
        "startDate": "2024-12-15T18:00:00Z",
        "endDate": "2024-12-15T22:00:00Z",
        "maxAttendees": 50,
        "isOnline": false
      }')
    
    echo "$EVENT_RESPONSE" | jq '.' || echo "$EVENT_RESPONSE"
    
    # Extract event ID
    EVENT_ID=$(echo "$EVENT_RESPONSE" | jq -r '.data.id' 2>/dev/null)
    
    if [ "$EVENT_ID" != "null" ] && [ "$EVENT_ID" != "" ]; then
        # Test event registration
        echo -e "\n${YELLOW}6. Testing Event Registration${NC}"
        REGISTRATION_RESPONSE=$(curl -s -X POST "$API_URL/eventregistration" \
          -H "Content-Type: application/json" \
          -H "Authorization: Bearer $TOKEN" \
          -d "{
            \"eventId\": \"$EVENT_ID\"
          }")
        
        echo "$REGISTRATION_RESPONSE" | jq '.' || echo "$REGISTRATION_RESPONSE"
    fi
    
    # Test get public alumni list
    echo -e "\n${YELLOW}7. Testing Get Public Alumni List${NC}"
    curl -s "$API_URL/alumni" | jq '.' || echo "Failed to get alumni list"
    
    # Test get public events list
    echo -e "\n${YELLOW}8. Testing Get Public Events List${NC}"
    curl -s "$API_URL/events" | jq '.' || echo "Failed to get events list"
    
else
    echo -e "\n${RED}❌ Registration failed, skipping authenticated tests${NC}"
fi

echo -e "\n${GREEN}✅ API testing completed!${NC}"
echo -e "${YELLOW}📖 Check the Swagger UI at: http://localhost:5000/swagger${NC}"
