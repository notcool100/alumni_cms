using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Alumni.Application.UseCases.Navigation;
using Alumni.Application.DTOs;
using System.Security.Claims;

namespace Alumni.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NavigationController : ControllerBase
{
    private readonly IMediator _mediator;

    public NavigationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetUserNavigation()
    {
        try
        {
            // Debug: Log all claims for troubleshooting
            var allClaims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            
            // Check if user is authenticated
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "User is not authenticated",
                    isAuthenticated = User.Identity?.IsAuthenticated,
                    authenticationType = User.Identity?.AuthenticationType,
                    availableClaims = allClaims
                });
            }
            
            // Get user ID and role ID from claims
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var roleIdClaim = User.FindFirst("roleId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(roleIdClaim))
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "User information not found",
                    availableClaims = allClaims,
                    userIdClaim = userIdClaim,
                    roleIdClaim = roleIdClaim
                });
            }

            if (!Guid.TryParse(userIdClaim, out var userId) || !Guid.TryParse(roleIdClaim, out var roleId))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid user or role ID",
                    userIdClaim = userIdClaim,
                    roleIdClaim = roleIdClaim
                });
            }

            var query = new GetUserNavigationQuery
            {
                UserId = userId,
                RoleId = roleId
            };

            var result = await _mediator.Send(query);

            return Ok(new
            {
                success = true,
                data = result,
                message = "Navigation retrieved successfully"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                success = false,
                message = "An error occurred while retrieving navigation",
                error = ex.Message
            });
        }
    }

    [HttpGet("test-auth")]
    [Authorize]
    public IActionResult TestAuth()
    {
        var allClaims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
        var isAuthenticated = User.Identity?.IsAuthenticated;
        var authenticationType = User.Identity?.AuthenticationType;
        
        return Ok(new
        {
            success = true,
            message = "Authentication test",
            isAuthenticated = isAuthenticated,
            authenticationType = authenticationType,
            claims = allClaims
        });
    }

    [HttpGet("role/{roleId}")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> GetNavigationByRole(Guid roleId)
    {
        try
        {
            var query = new GetUserNavigationQuery
            {
                UserId = Guid.Empty, // Not needed for role-based navigation
                RoleId = roleId
            };

            var result = await _mediator.Send(query);

            return Ok(new
            {
                success = true,
                data = result,
                message = "Navigation retrieved successfully"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                success = false,
                message = "An error occurred while retrieving navigation",
                error = ex.Message
            });
        }
    }
}
