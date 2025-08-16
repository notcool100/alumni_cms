const fetch = require('node-fetch');

async function testBackendConnection() {
    const baseUrl = 'http://localhost:5037';
    
    console.log('Testing backend connection...\n');
    
    try {
        // Test health endpoint
        console.log('1. Testing health endpoint...');
        const healthResponse = await fetch(`${baseUrl}/health`);
        const healthData = await healthResponse.json();
        console.log('Health check response:', healthData);
        
        // Test auth endpoints
        console.log('\n2. Testing auth endpoints...');
        
        // Test register endpoint
        const registerData = {
            firstName: 'Test',
            lastName: 'User',
            email: 'test@example.com',
            password: 'TestPassword123!'
        };
        
        const registerResponse = await fetch(`${baseUrl}/api/auth/register`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(registerData)
        });
        
        if (registerResponse.ok) {
            const registerResult = await registerResponse.json();
            console.log('Register response:', registerResult);
        } else {
            console.log('Register failed:', registerResponse.status, registerResponse.statusText);
        }
        
        // Test login endpoint
        const loginData = {
            email: 'test@example.com',
            password: 'TestPassword123!'
        };
        
        const loginResponse = await fetch(`${baseUrl}/api/auth/login`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(loginData)
        });
        
        if (loginResponse.ok) {
            const loginResult = await loginResponse.json();
            console.log('Login response:', loginResult);
        } else {
            console.log('Login failed:', loginResponse.status, loginResponse.statusText);
        }
        
        // Test alumni endpoints
        console.log('\n3. Testing alumni endpoints...');
        const alumniResponse = await fetch(`${baseUrl}/api/alumni`);
        if (alumniResponse.ok) {
            const alumniData = await alumniResponse.json();
            console.log('Alumni list response:', alumniData);
        } else {
            console.log('Alumni list failed:', alumniResponse.status, alumniResponse.statusText);
        }
        
    } catch (error) {
        console.error('Error testing backend:', error.message);
    }
}

testBackendConnection();
