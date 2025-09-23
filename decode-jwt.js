// Simple JWT decoder (without signature verification)
function decodeJWT(token) {
    const parts = token.split('.');
    if (parts.length !== 3) {
        console.log('Invalid JWT token format');
        return;
    }

    try {
        const header = JSON.parse(Buffer.from(parts[0], 'base64').toString());
        const payload = JSON.parse(Buffer.from(parts[1], 'base64').toString());
        
        console.log('JWT Header:', header);
        console.log('JWT Payload:', payload);
        
        // Check for specific claims
        console.log('\nClaims Analysis:');
        console.log('NameIdentifier (user ID):', payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']);
        console.log('Email:', payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress']);
        console.log('Role:', payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']);
        console.log('roleId:', payload['roleId']);
        console.log('firstName:', payload['firstName']);
        console.log('lastName:', payload['lastName']);
        console.log('Expiration:', new Date(payload.exp * 1000));
        console.log('Issuer:', payload.iss);
        console.log('Audience:', payload.aud);
        
        // Check if token is expired
        const now = Math.floor(Date.now() / 1000);
        if (payload.exp < now) {
            console.log('❌ Token is EXPIRED!');
        } else {
            console.log('✅ Token is valid');
        }
        
    } catch (error) {
        console.log('Error decoding JWT:', error.message);
    }
}

// Your token from the login response
const token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImExMTExMTExLTExMTEtMTExMS0xMTExLTExMTExMTExMTExMSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6ImFkbWluQGFsdW1uaS5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJTdXBlckFkbWluIiwicm9sZUlkIjoiMTExMTExMTEtMTExMS0xMTExLTExMTEtMTExMTExMTExMTExIiwiZmlyc3ROYW1lIjoiQWRtaW4iLCJsYXN0TmFtZSI6IlVzZXIiLCJleHAiOjE3NTUzNzQxNTQsImlzcyI6IllvdXJJc3N1ZXIiLCJhdWQiOiJZb3VyQXVkaWVuY2UifQ.a_3ptoEpm0IzS_Yr-RUnOQ3KtZK7oXL_F7qD8KIrSi0';

console.log('Decoding JWT Token...\n');
decodeJWT(token);
