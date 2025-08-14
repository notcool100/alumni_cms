using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlumniBackendApi.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace AlumniBackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumniController : ControllerBase
    {
        private readonly AlumniContext _context;
        private readonly ILogger<AlumniController> _logger;

        public AlumniController(AlumniContext context, ILogger<AlumniController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/alumni
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<Alumni>>>> GetAlumni()
        {
            _logger.LogInformation("📋 Fetching list of alumni");
            
            var alumni = await _context.Alumni
                .Where(a => a.IsPublic)
                .ToListAsync();
            
            _logger.LogInformation("✅ Retrieved {Count} alumni records", alumni.Count);
            return Ok(new ApiResponse<List<Alumni>>
            {
                Success = true,
                Data = alumni
            });
        }

        // GET: api/alumni/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Alumni>>> GetAlumni(Guid id)
        {
            _logger.LogInformation("🔍 Fetching alumni with ID: {Id}", id);
            
            var alumni = await _context.Alumni.FindAsync(id);
            
            if (alumni == null)
            {
                _logger.LogWarning("⚠️ Alumni not found with ID: {Id}", id);
                return NotFound(new ApiResponse<Alumni>
                {
                    Success = false,
                    Message = "Alumni not found"
                });
            }

            // Only return private profiles if the user is the owner or an admin
            if (!alumni.IsPublic)
            {
                // In a real implementation, we would check if the user is authenticated
                // and if they are the owner or an admin
                // For now, we'll just return not found for private profiles
                _logger.LogWarning("🔒 Alumni profile is private for ID: {Id}", id);
                return NotFound(new ApiResponse<Alumni>
                {
                    Success = false,
                    Message = "Alumni profile is private"
                });
            }

            _logger.LogInformation("✅ Alumni profile retrieved successfully for ID: {Id}", id);
            return Ok(new ApiResponse<Alumni>
            {
                Success = true,
                Data = alumni
            });
        }

        // POST: api/alumni
        [HttpPost]
        public async Task<ActionResult<ApiResponse<Alumni>>> CreateAlumni([FromBody] CreateAlumniRequest request)
        {
            _logger.LogInformation("📝 Creating new alumni profile");
            
            // In a real implementation, we would get the user ID from the JWT token
            // For now, we'll use a placeholder
            var userId = Guid.NewGuid();

            var alumni = new Alumni
            {
                UserId = userId,
                GraduationYear = request.GraduationYear,
                Degree = request.Degree,
                Major = request.Major,
                CurrentCompany = request.CurrentCompany,
                CurrentPosition = request.CurrentPosition,
                Location = request.Location,
                Bio = request.Bio,
                LinkedinUrl = request.LinkedinUrl,
                GithubUrl = request.GithubUrl,
                WebsiteUrl = request.WebsiteUrl,
                IsPublic = request.IsPublic,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Alumni.Add(alumni);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("✅ Alumni profile created successfully with ID: {Id}", alumni.Id);

            return Ok(new ApiResponse<Alumni>
            {
                Success = true,
                Data = alumni
            });
        }

        // PUT: api/alumni/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<Alumni>>> UpdateAlumni(Guid id, [FromBody] CreateAlumniRequest request)
        {
            _logger.LogInformation("✏️ Updating alumni profile with ID: {Id}", id);
            
            var alumni = await _context.Alumni.FindAsync(id);
            
            if (alumni == null)
            {
                _logger.LogWarning("⚠️ Alumni not found with ID: {Id}", id);
                return NotFound(new ApiResponse<Alumni>
                {
                    Success = false,
                    Message = "Alumni not found"
                });
            }

            // In a real implementation, we would check if the user is the owner or an admin
            // For now, we'll allow the update

            alumni.GraduationYear = request.GraduationYear;
            alumni.Degree = request.Degree;
            alumni.Major = request.Major;
            alumni.CurrentCompany = request.CurrentCompany;
            alumni.CurrentPosition = request.CurrentPosition;
            alumni.Location = request.Location;
            alumni.Bio = request.Bio;
            alumni.LinkedinUrl = request.LinkedinUrl;
            alumni.GithubUrl = request.GithubUrl;
            alumni.WebsiteUrl = request.WebsiteUrl;
            alumni.IsPublic = request.IsPublic;
            alumni.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            
            _logger.LogInformation("✅ Alumni profile updated successfully with ID: {Id}", id);

            return Ok(new ApiResponse<Alumni>
            {
                Success = true,
                Data = alumni
            });
        }

        // DELETE: api/alumni/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteAlumni(Guid id)
        {
            _logger.LogInformation("🗑️ Deleting alumni profile with ID: {Id}", id);
            
            var alumni = await _context.Alumni.FindAsync(id);
            
            if (alumni == null)
            {
                _logger.LogWarning("⚠️ Alumni not found with ID: {Id}", id);
                return NotFound(new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Alumni not found"
                });
            }

            // In a real implementation, we would check if the user is the owner or an admin
            // For now, we'll allow the deletion

            _context.Alumni.Remove(alumni);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("✅ Alumni profile deleted successfully with ID: {Id}", id);

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                Data = true,
                Message = "Alumni deleted successfully"
            });
        }
    }
}