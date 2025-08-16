using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class EventRegistrationController : ControllerBase
    {
        private readonly AlumniContext _context;
        private readonly ILogger<EventRegistrationController> _logger;

        public EventRegistrationController(AlumniContext context, ILogger<EventRegistrationController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/eventregistration/event/{eventId}
        [HttpGet("event/{eventId}")]
        public async Task<ActionResult<ApiResponse<List<EventRegistration>>>> GetEventRegistrations(Guid eventId)
        {
            _logger.LogInformation("📋 Fetching registrations for event: {EventId}", eventId);
            
            var registrations = await _context.EventRegistrations
                .Where(er => er.EventId == eventId)
                .ToListAsync();
            
            _logger.LogInformation("✅ Retrieved {Count} registrations for event {EventId}", registrations.Count, eventId);
            return Ok(new ApiResponse<List<EventRegistration>>
            {
                Success = true,
                Data = registrations
            });
        }

        // GET: api/eventregistration/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<ApiResponse<List<EventRegistration>>>> GetUserRegistrations(Guid userId)
        {
            _logger.LogInformation("📋 Fetching registrations for user: {UserId}", userId);
            
            var registrations = await _context.EventRegistrations
                .Where(er => er.UserId == userId)
                .ToListAsync();
            
            _logger.LogInformation("✅ Retrieved {Count} registrations for user {UserId}", registrations.Count, userId);
            return Ok(new ApiResponse<List<EventRegistration>>
            {
                Success = true,
                Data = registrations
            });
        }

        // POST: api/eventregistration
        [HttpPost]
        public async Task<ActionResult<ApiResponse<EventRegistration>>> RegisterForEvent([FromBody] CreateRegistrationRequest request)
        {
            var userId = User.GetUserId();
            if (!userId.HasValue)
            {
                return Unauthorized(new ApiResponse<EventRegistration>
                {
                    Success = false,
                    Message = "User not authenticated"
                });
            }
            
            _logger.LogInformation("📝 Creating event registration for event: {EventId}, user: {UserId}", request.EventId, userId.Value);
            
            // Check if event exists
            var eventItem = await _context.Events.FindAsync(request.EventId);
            if (eventItem == null)
            {
                _logger.LogWarning("⚠️ Event not found with ID: {EventId}", request.EventId);
                return NotFound(new ApiResponse<EventRegistration>
                {
                    Success = false,
                    Message = "Event not found"
                });
            }

            // Check if user is already registered
            var existingRegistration = await _context.EventRegistrations
                .FirstOrDefaultAsync(er => er.EventId == request.EventId && er.UserId == userId.Value);
            
            if (existingRegistration != null)
            {
                _logger.LogWarning("⚠️ User {UserId} is already registered for event {EventId}", userId.Value, request.EventId);
                return BadRequest(new ApiResponse<EventRegistration>
                {
                    Success = false,
                    Message = "User is already registered for this event"
                });
            }

            // Check if event is full
            if (eventItem.MaxAttendees.HasValue && eventItem.CurrentAttendees >= eventItem.MaxAttendees.Value)
            {
                _logger.LogWarning("⚠️ Event {EventId} is full", request.EventId);
                return BadRequest(new ApiResponse<EventRegistration>
                {
                    Success = false,
                    Message = "Event is full"
                });
            }

            var registration = new EventRegistration
            {
                EventId = request.EventId,
                UserId = userId.Value,
                RegistrationDate = DateTime.UtcNow,
                Status = RegistrationStatus.Pending
            };

            _context.EventRegistrations.Add(registration);
            
            // Update event attendee count
            eventItem.CurrentAttendees++;
            
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("✅ Event registration created successfully for event: {EventId}, user: {UserId}", request.EventId, userId.Value);

            return Ok(new ApiResponse<EventRegistration>
            {
                Success = true,
                Data = registration
            });
        }

        // PUT: api/eventregistration/{id}/status
        [HttpPut("{id}/status")]
        public async Task<ActionResult<ApiResponse<EventRegistration>>> UpdateRegistrationStatus(Guid id, [FromBody] UpdateRegistrationStatusRequest request)
        {
            _logger.LogInformation("✏️ Updating registration status for ID: {Id} to {Status}", id, request.Status);
            
            var registration = await _context.EventRegistrations.FindAsync(id);
            if (registration == null)
            {
                _logger.LogWarning("⚠️ Registration not found with ID: {Id}", id);
                return NotFound(new ApiResponse<EventRegistration>
                {
                    Success = false,
                    Message = "Registration not found"
                });
            }

            registration.Status = request.Status;
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("✅ Registration status updated successfully for ID: {Id}", id);

            return Ok(new ApiResponse<EventRegistration>
            {
                Success = true,
                Data = registration
            });
        }

        // DELETE: api/eventregistration/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> CancelRegistration(Guid id)
        {
            _logger.LogInformation("🗑️ Cancelling registration with ID: {Id}", id);
            
            var registration = await _context.EventRegistrations.FindAsync(id);
            if (registration == null)
            {
                _logger.LogWarning("⚠️ Registration not found with ID: {Id}", id);
                return NotFound(new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Registration not found"
                });
            }

            // Update event attendee count
            var eventItem = await _context.Events.FindAsync(registration.EventId);
            if (eventItem != null)
            {
                eventItem.CurrentAttendees = Math.Max(0, eventItem.CurrentAttendees - 1);
            }

            _context.EventRegistrations.Remove(registration);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("✅ Registration cancelled successfully for ID: {Id}", id);

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                Data = true,
                Message = "Registration cancelled successfully"
            });
        }
    }

    public class CreateRegistrationRequest
    {
        public Guid EventId { get; set; }
    }

    public class UpdateRegistrationStatusRequest
    {
        public string Status { get; set; }
    }
}
