const API_BASE_URL = 'http://localhost:5037';

async function testJWTConfiguration() {
    console.log('Testing JWT Configuration...\n');

    // Test 1: Check if the backend is running
    try {
        const healthResponse = await fetch(`${API_BASE_URL}/health`);
        const healthData = await healthResponse.json();
        console.log('✅ Backend is running:', healthData);
    } catch (error) {
        console.log('❌ Backend is not running:', error.message);
        return;
    }

    // Test 2: Try to access a protected endpoint without token
    try {
        const noAuthResponse = await fetch(`${API_BASE_URL}/api/navigation/test-auth`);
        console.log('🔒 No auth response status:', noAuthResponse.status);
        const noAuthText = await noAuthResponse.text();
        console.log('🔒 No auth response:', noAuthText);
    } catch (error) {
        console.log('❌ No auth test failed:', error.message);
    }

    // Test 3: Try with an invalid token
    try {
        const invalidResponse = await fetch(`${API_BASE_URL}/api/navigation/test-auth`, {
            headers: {
                'Authorization': 'Bearer invalid-token',
                'Content-Type': 'application/json'
            }
        });
        console.log('🔒 Invalid token response status:', invalidResponse.status);
        const invalidText = await invalidResponse.text();
        console.log('🔒 Invalid token response:', invalidText);
    } catch (error) {
        console.log('❌ Invalid token test failed:', error.message);
    }

    // Test 4: Try with your actual token
    const token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImExMTExMTExLTExMTEtMTExMS0xMTExLTExMTExMTExMTExMSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6ImFkbWluQGFsdW1uaS5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJTdXBlckFkbWluIiwicm9sZUlkIjoiMTExMTExMTEtMTExMS0xMTExLTExMTEtMTExMTExMTExMTExIiwiZmlyc3ROYW1lIjoiQWRtaW4iLCJsYXN0TmFtZSI6IlVzZXIiLCJleHAiOjE3NTUzNzQxNTQsImlzcyI6IllvdXJJc3N1ZXIiLCJhdWQiOiJZb3VyQXVkaWVuY2UifQ.a_3ptoEpm0IzS_Yr-RUnOQ3KtZK7oXL_F7qD8KIrSi0';
    
    try {
        const validResponse = await fetch(`${API_BASE_URL}/api/navigation/test-auth`, {
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        });
        console.log('🔑 Valid token response status:', validResponse.status);
        const validText = await validResponse.text();
        console.log('🔑 Valid token response:', validText);
        
        if (validResponse.ok) {
            try {
                const validData = JSON.parse(validText);
                console.log('✅ JWT authentication is working!');
                console.log('Claims found:', validData.claims?.length || 0);
            } catch (parseError) {
                console.log('❌ Failed to parse response:', parseError.message);
            }
        }
    } catch (error) {
        console.log('❌ Valid token test failed:', error.message);
    }

    console.log('\n📋 JWT Configuration Test Summary:');
    console.log('- Backend is running');
    console.log('- Testing JWT authentication...');
}

testJWTConfiguration();
