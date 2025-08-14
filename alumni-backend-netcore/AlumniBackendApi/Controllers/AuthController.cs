using AlumniBackendApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService _authService, ILogger<AuthController> logger)
    {
        this._authService = _authService;
        this._logger = logger;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        _logger.LogInformation("📝 User registration attempt for email: {Email}", registerRequest.Email);
        
        var response = await _authService.Register(registerRequest);
        if (!response.Success)
        {
            _logger.LogWarning("⚠️ Registration failed: {Message}", response.Message);
            return BadRequest(response);
        }
        
        _logger.LogInformation("✅ User registration successful for email: {Email}", registerRequest.Email);
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        _logger.LogInformation("🔑 User login attempt for email: {Email}", loginRequest.Email);
        
        var response = await _authService.Login(loginRequest);
        if (!response.Success)
        {
            _logger.LogWarning("❌ Login failed: {Message}", response.Message);
            return Unauthorized(response);
        }
        
        _logger.LogInformation("✅ User login successful for email: {Email}", loginRequest.Email);
        return Ok(response);
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetProfile()
    {
        _logger.LogInformation("👤 User profile request received");
        
        // Get the token from the Authorization header
        var authHeader = Request.Headers["Authorization"].FirstOrDefault();
        if (authHeader == null || !authHeader.StartsWith("Bearer "))
        {
            _logger.LogWarning("❌ Missing or invalid authorization header");
            return Unauthorized(new { Success = false, Message = "Missing or invalid authorization header" });
        }

        var token = authHeader.Substring("Bearer ".Length);
        var response = await _authService.GetProfile(token);
        if (!response.Success)
        {
            _logger.LogWarning("❌ Profile request failed: {Message}", response.Message);
            return Unauthorized(response);
        }
        
        _logger.LogInformation("✅ User profile retrieved successfully");
        return Ok(response);
    }
}