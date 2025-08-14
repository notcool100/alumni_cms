using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System;
using System.Threading.Tasks;
using AlumniBackendApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using BCrypt.Net;

public class AuthService : IAuthService
{
    private readonly AlumniContext _context;
    private readonly AppSettings Configuration;
    public AuthService(AlumniContext _context, IOptions<AppSettings> _options)
    {
        this._context = _context;
        Configuration = _options.Value;
    }

    public async Task<ApiResponse<AuthResponse>> Register(RegisterRequest registerRequest)
    {
        var existingUser = await _context.Users.AnyAsync(u => u.Email == registerRequest.Email);
        if (existingUser)
        {
            return new ApiResponse<AuthResponse>
            {
                Success = false,
                Message = "User with this email already exists"
            };
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = registerRequest.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password),
            FirstName = registerRequest.FirstName,
            LastName = registerRequest.LastName,
            Role = "alumni",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var userResponse = new UserResponse
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Role = user.Role
        };

        return new ApiResponse<AuthResponse>
        {
            Success = true,
            Data = new AuthResponse
            {
                Token = GenerateJwtToken(user),
                User = userResponse
            }
        };
    }

    public async Task<ApiResponse<AuthResponse>> Login(LoginRequest loginRequest)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginRequest.Email);
        if (user == null)
        {
            return new ApiResponse<AuthResponse>
            {
                Success = false,
                Message = "Invalid email or password"
            };
        }

        if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
        {
            return new ApiResponse<AuthResponse>
            {
                Success = false,
                Message = "Invalid email or password"
            };
        }

        var userResponse = new UserResponse
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Role = user.Role
        };

        return new ApiResponse<AuthResponse>
        {
            Success = true,
            Data = new AuthResponse
            {
                Token = GenerateJwtToken(user),
                User = userResponse
            }
        };
    }

    private string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.JWT.Secret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: Configuration.JWT.Issuer,
            audience: Configuration.JWT.Audience,
            claims: new[]
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, user.Id.ToString()),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, user.Email),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, user.Role),
                new System.Security.Claims.Claim("firstName", user.FirstName),
                new System.Security.Claims.Claim("lastName", user.LastName)
            },
            expires: DateTime.Now.AddHours(Configuration.JWT.ExpiryInHours),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<ApiResponse<UserResponse>> GetProfile(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return new ApiResponse<UserResponse>
                {
                    Success = false,
                    Message = "Invalid or expired token"
                };
            }

            var userId = Guid.Parse(userIdClaim.Value);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return new ApiResponse<UserResponse>
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            var userResponse = new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role
            };

            return new ApiResponse<UserResponse>
            {
                Success = true,
                Data = userResponse
            };
        }
        catch (Exception)
        {
            return new ApiResponse<UserResponse>
            {
                Success = false,
                Message = "Invalid or expired token"
            };
        }
    }
}

public interface IAuthService
{
    Task<ApiResponse<AuthResponse>> Register(RegisterRequest registerRequest);
    Task<ApiResponse<AuthResponse>> Login(LoginRequest loginRequest);
    Task<ApiResponse<UserResponse>> GetProfile(string token);
}