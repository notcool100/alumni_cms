using Alumni.Application.DTOs;

namespace Alumni.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(UserResponse user);
    UserResponse? ValidateToken(string token);
}
