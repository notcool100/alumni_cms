using Microsoft.AspNetCore.Mvc;

namespace Alumni.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public ActionResult<object> Get()
    {
        return Ok(new
        {
            status = "healthy",
            timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
            service = "Alumni Backend API"
        });
    }
}
