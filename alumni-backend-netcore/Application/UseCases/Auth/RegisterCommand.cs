using MediatR;
using Alumni.Application.DTOs;
using Alumni.Application.Interfaces;
using Alumni.Domain.Interfaces;
using Alumni.Domain.Entities;

namespace Alumni.Application.UseCases.Auth;

public record RegisterCommand : IRequest<ApiResponse<AuthResponse>>
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
}

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ApiResponse<AuthResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly IJwtService _jwtService;

    public RegisterCommandHandler(
        IUserRepository userRepository,
        IPasswordService passwordService,
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _jwtService = jwtService;
    }

    public async Task<ApiResponse<AuthResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // Check if user already exists
        if (await _userRepository.EmailExistsAsync(request.Email))
        {
            return new ApiResponse<AuthResponse>
            {
                Success = false,
                Message = "User with this email already exists"
            };
        }

        // Create new user
        var passwordHash = _passwordService.HashPassword(request.Password);
        var user = new User(request.Email, passwordHash, request.FirstName, request.LastName);

        await _userRepository.AddAsync(user);

        // Generate response
        var userResponse = new UserResponse
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Role = user.Role
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
