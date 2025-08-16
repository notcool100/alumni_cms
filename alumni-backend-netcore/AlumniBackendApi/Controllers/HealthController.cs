using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace AlumniBackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly ILogger<HealthController> _logger;

        public HealthController(ILogger<HealthController> logger)
        {
            _logger = logger;
        }

        // GET: api/health
        [HttpGet]
        public ActionResult<object> HealthCheck()
        {
            _logger.LogInformation("🏥 Health check endpoint called");
            
            var response = new
            {
                status = "healthy",
                timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                service = "alumni-backend-netcore"
            };
            
            _logger.LogInformation("✅ Health check completed successfully");
            return Ok(response);
        }
    }
}
