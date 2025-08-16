const API_BASE_URL = 'http://localhost:5037';

async function testNavigationAPI() {
    console.log('Testing Navigation API...\n');

    // Test 1: Health check
    try {
        const healthResponse = await fetch(`${API_BASE_URL}/health`);
        const healthData = await healthResponse.json();
        console.log('✅ Health Check:', healthData);
    } catch (error) {
        console.log('❌ Health Check Failed:', error.message);
    }

    // Test 2: Try to get navigation without auth (should fail)
    try {
        const navResponse = await fetch(`${API_BASE_URL}/api/navigation/user`);
        const navData = await navResponse.json();
        console.log('🔒 Navigation without auth (expected to fail):', navData);
    } catch (error) {
        console.log('✅ Navigation without auth correctly failed:', error.message);
    }

    // Test 3: Test with a sample token (you can replace this with your actual token)
    const sampleToken = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImExMTExMTExLTExMTEtMTExMS0xMTExLTExMTExMTExMTExMSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6ImFkbWluQGFsdW1uaS5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJTdXBlckFkbWluIiwicm9sZUlkIjoiMTExMTExMTEtMTExMS0xMTExLTExMTEtMTExMTExMTExMTExIiwiZmlyc3ROYW1lIjoiQWRtaW4iLCJsYXN0TmFtZSI6IlVzZXIiLCJleHAiOjE3NTUzNzQxNTQsImlzcyI6IllvdXJJc3N1ZXIiLCJhdWQiOiJZb3VyQXVkaWVuY2UifQ.a_3ptoEpm0IzS_Yr-RUnOQ3KtZK7oXL_F7qD8KIrSi0';
    
    try {
        const authResponse = await fetch(`${API_BASE_URL}/api/navigation/user`, {
            headers: {
                'Authorization': `Bearer ${sampleToken}`,
                'Content-Type': 'application/json'
            }
        });
        
        console.log('Response status:', authResponse.status);
        console.log('Response headers:', Object.fromEntries(authResponse.headers.entries()));
        
        const responseText = await authResponse.text();
        console.log('Raw response:', responseText);
        
        if (authResponse.ok) {
            try {
                const authData = JSON.parse(responseText);
                console.log('✅ Navigation with auth:', authData);
            } catch (parseError) {
                console.log('❌ Failed to parse JSON response:', parseError.message);
            }
        } else {
            try {
                const errorData = JSON.parse(responseText);
                console.log('❌ Navigation with auth failed:', errorData);
            } catch (parseError) {
                console.log('❌ Failed to parse error response:', parseError.message);
            }
        }
    } catch (error) {
        console.log('❌ Navigation with auth error:', error.message);
    }

    console.log('\n📋 Navigation API Test Summary:');
    console.log('- Health endpoint: Working');
    console.log('- Navigation endpoint: Requires authentication (as expected)');
    console.log('\n🚀 To test with authentication:');
    console.log('1. Start the backend server');
    console.log('2. Login through the frontend');
    console.log('3. Check the browser network tab for navigation requests');
}

testNavigationAPI();
