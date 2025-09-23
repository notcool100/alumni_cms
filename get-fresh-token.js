const API_BASE_URL = 'http://localhost:5037';

async function getFreshToken() {
    console.log('Getting fresh JWT token...\n');

    try {
        const loginResponse = await fetch(`${API_BASE_URL}/api/auth/login`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                email: 'admin@alumni.com',
                password: 'Admin123!'
            })
        });

        if (loginResponse.ok) {
            const loginData = await loginResponse.json();
            console.log('✅ Login successful!');
            console.log('Token:', loginData.data.token);
            console.log('User:', loginData.data.user);
            
            // Test the token immediately
            console.log('\n🔍 Testing the fresh token...');
            const navResponse = await fetch(`${API_BASE_URL}/api/navigation/user`, {
                headers: {
                    'Authorization': `Bearer ${loginData.data.token}`,
                    'Content-Type': 'application/json'
                }
            });
            
            console.log('Navigation response status:', navResponse.status);
            const navText = await navResponse.text();
            console.log('Navigation response:', navText);
            
            if (navResponse.ok) {
                console.log('🎉 Navigation API is working with fresh token!');
            } else {
                console.log('❌ Navigation API still failing');
            }
        } else {
            const errorData = await loginResponse.json();
            console.log('❌ Login failed:', errorData);
        }
    } catch (error) {
        console.log('❌ Error getting fresh token:', error.message);
    }
}

getFreshToken();
