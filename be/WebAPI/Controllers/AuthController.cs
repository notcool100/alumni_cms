using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using Alumni.Application.UseCases.Auth;
using Alumni.Application.DTOs;

namespace Alumni.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse<AuthResponse>>> Register([FromBody] RegisterRequest request)
    {
        var command = new RegisterCommand
        {
            Email = request.Email,
            Password = request.Password,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<AuthResponse>>> Login([FromBody] LoginRequest request)
    {
        var command = new LoginCommand
        {
            Email = request.Email,
            Password = request.Password
        };

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<UserResponse>>> GetCurrentUser()
    {
        var userId = GetCurrentUserId();
        // For now, return basic user info from claims
        // In a real implementation, you'd fetch from database
        var user = new UserResponse
        {
            Id = userId,
            Email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ?? "",
            FirstName = User.FindFirst(System.Security.Claims.ClaimTypes.GivenName)?.Value ?? "",
            LastName = User.FindFirst(System.Security.Claims.ClaimTypes.Surname)?.Value ?? "",
            RoleId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value ?? Guid.Empty.ToString()),
            RoleName = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value ?? "User"
        };

        return Ok(new ApiResponse<UserResponse>
        {
            Success = true,
            Data = user
        });
    }

    private Guid GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            throw new UnauthorizedAccessException("User not authenticated");
        }
        return userId;
    }
}
