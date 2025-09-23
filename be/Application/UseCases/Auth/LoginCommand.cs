using MediatR;
using Alumni.Application.DTOs;
using Alumni.Application.Interfaces;
using Alumni.Domain.Interfaces;

namespace Alumni.Application.UseCases.Auth;

public record LoginCommand : IRequest<ApiResponse<AuthResponse>>
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, ApiResponse<AuthResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly IJwtService _jwtService;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IPasswordService passwordService,
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _jwtService = jwtService;
    }

    public async Task<ApiResponse<AuthResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // Find user by email with role included
        var user = await _userRepository.GetByEmailWithRoleAsync(request.Email);
        if (user == null)
        {
            return new ApiResponse<AuthResponse>
            {
                Success = false,
                Message = "Invalid email or password"
            };
        }

        // Verify password
        if (!_passwordService.VerifyPassword(request.Password, user.PasswordHash))
        {
            return new ApiResponse<AuthResponse>
            {
                Success = false,
                Message = "Invalid email or password"
            };
        }

        // Check if user has a role
        if (user.Role == null)
        {
            return new ApiResponse<AuthResponse>
            {
                Success = false,
                Message = "User role not found"
            };
        }

        // Generate response
        var userResponse = new UserResponse
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            RoleId = user.RoleId,
            RoleName = user.Role.Name
        };

        var token = _jwtService.GenerateToken(userResponse);

        return new ApiResponse<AuthResponse>
        {
            Success = true,
            Data = new AuthResponse
            {
                Token = token,
                User = userResponse
            }
        };
    }
}
