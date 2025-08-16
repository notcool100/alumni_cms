using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AlumniBackendApi.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace AlumniBackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventsController : ControllerBase
    {
        private readonly AlumniContext _context;
        private readonly ILogger<EventsController> _logger;

        public EventsController(AlumniContext context, ILogger<EventsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/events
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<List<Event>>>> GetEvents()
        {
            _logger.LogInformation("📋 Fetching list of events");
            
            var events = await _context.Events.ToListAsync();
            
            _logger.LogInformation("✅ Retrieved {Count} events", events.Count);
            return Ok(new ApiResponse<List<Event>>
            {
                Success = true,
                Data = events
            });
        }

        // GET: api/events/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<Event>>> GetEvent(Guid id)
        {
            _logger.LogInformation("🔍 Fetching event with ID: {Id}", id);
            
            var eventItem = await _context.Events.FindAsync(id);
            
            if (eventItem == null)
            {
                _logger.LogWarning("⚠️ Event not found with ID: {Id}", id);
                return NotFound(new ApiResponse<Event>
                {
                    Success = false,
                    Message = "Event not found"
                });
            }

            _logger.LogInformation("✅ Event retrieved successfully for ID: {Id}", id);
            return Ok(new ApiResponse<Event>
            {
                Success = true,
                Data = eventItem
            });
        }

        // POST: api/events
        [HttpPost]
        public async Task<ActionResult<ApiResponse<Event>>> CreateEvent([FromBody] CreateEventRequest request)
        {
            _logger.LogInformation("📝 Creating new event");
            
            var userId = User.GetUserId();
            if (!userId.HasValue)
            {
                return Unauthorized(new ApiResponse<Event>
                {
                    Success = false,
                    Message = "User not authenticated"
                });
            }

            var eventItem = new Event
            {
                Title = request.Title,
                Description = request.Description,
                Location = request.Location,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                MaxAttendees = request.MaxAttendees,
                IsOnline = request.IsOnline,
                MeetingUrl = request.MeetingUrl,
                CreatedBy = userId.Value,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Events.Add(eventItem);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("✅ Event created successfully with ID: {Id}", eventItem.Id);

            return Ok(new ApiResponse<Event>
            {
                Success = true,
                Data = eventItem
            });
        }

        // PUT: api/events/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<Event>>> UpdateEvent(Guid id, [FromBody] CreateEventRequest request)
        {
            _logger.LogInformation("✏️ Updating event with ID: {Id}", id);
            
            var userId = User.GetUserId();
            if (!userId.HasValue)
            {
                return Unauthorized(new ApiResponse<Event>
                {
                    Success = false,
                    Message = "User not authenticated"
                });
            }
            
            var eventItem = await _context.Events.FindAsync(id);
            
            if (eventItem == null)
            {
                _logger.LogWarning("⚠️ Event not found with ID: {Id}", id);
                return NotFound(new ApiResponse<Event>
                {
                    Success = false,
                    Message = "Event not found"
                });
            }

            // Check if the user is the creator or an admin
            if (eventItem.CreatedBy != userId.Value && User.GetUserRole() != "admin")
            {
                return Forbid();
            }

            eventItem.Title = request.Title;
            eventItem.Description = request.Description;
            eventItem.Location = request.Location;
            eventItem.StartDate = request.StartDate;
            eventItem.EndDate = request.EndDate;
            eventItem.MaxAttendees = request.MaxAttendees;
            eventItem.IsOnline = request.IsOnline;
            eventItem.MeetingUrl = request.MeetingUrl;
            eventItem.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            
            _logger.LogInformation("✅ Event updated successfully with ID: {Id}", id);

            return Ok(new ApiResponse<Event>
            {
                Success = true,
                Data = eventItem
            });
        }

        // DELETE: api/events/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteEvent(Guid id)
        {
            _logger.LogInformation("🗑️ Deleting event with ID: {Id}", id);
            
            var userId = User.GetUserId();
            if (!userId.HasValue)
            {
                return Unauthorized(new ApiResponse<bool>
                {
                    Success = false,
                    Message = "User not authenticated"
                });
            }
            
            var eventItem = await _context.Events.FindAsync(id);
            
            if (eventItem == null)
            {
                _logger.LogWarning("⚠️ Event not found with ID: {Id}", id);
                return NotFound(new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Event not found"
                });
            }

            // Check if the user is the creator or an admin
            if (eventItem.CreatedBy != userId.Value && User.GetUserRole() != "admin")
            {
                return Forbid();
            }

            _context.Events.Remove(eventItem);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("✅ Event deleted successfully with ID: {Id}", id);

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                Data = true,
                Message = "Event deleted successfully"
            });
        }
    }
}