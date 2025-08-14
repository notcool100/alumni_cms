using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace AlumniBackendApi.Models
{


    [Table("users")]
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Role { get; set; } = "alumni";

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        [MinLength(1)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(1)]
        public string LastName { get; set; }
    }

    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(1)]
        public string Password { get; set; }
    }
    public class AuthResponse
    {
        public string Token { get; set; }
        public UserResponse User { get; set; }
    }

    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
    }

    [Table("alumni")]
    public class Alumni
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [Range(1950, 2030)]
        public int GraduationYear { get; set; }

        [Required]
        public string Degree { get; set; }

        [Required]
        public string Major { get; set; }

        public string CurrentCompany { get; set; }

        public string CurrentPosition { get; set; }

        public string Location { get; set; }

        public string Bio { get; set; }

        public string LinkedinUrl { get; set; }

        public string GithubUrl { get; set; }

        public string WebsiteUrl { get; set; }

        public string ProfileImageUrl { get; set; }

        public bool IsPublic { get; set; } = true;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }

    public class CreateAlumniRequest
    {
        [Required]
        [Range(1950, 2030)]
        public int GraduationYear { get; set; }

        [Required]
        [MinLength(1)]
        public string Degree { get; set; }

        [Required]
        [MinLength(1)]
        public string Major { get; set; }

        [MinLength(1)]
        public string CurrentCompany { get; set; }

        [MinLength(1)]
        public string CurrentPosition { get; set; }

        [MinLength(1)]
        public string Location { get; set; }

        public string Bio { get; set; }

        [Url]
        public string LinkedinUrl { get; set; }

        [Url]
        public string GithubUrl { get; set; }

        [Url]
        public string WebsiteUrl { get; set; }

        public bool IsPublic { get; set; } = true;
    }

    [Table("events")]
    public class Event
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string Location { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public int? MaxAttendees { get; set; }

        public int CurrentAttendees { get; set; } = 0;

        public bool IsOnline { get; set; } = false;

        public string MeetingUrl { get; set; }

        [Required]
        public Guid CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }

    public class CreateEventRequest
    {
        [Required]
        [MinLength(1)]
        public string Title { get; set; }

        [Required]
        [MinLength(1)]
        public string Description { get; set; }

        [MinLength(1)]
        public string Location { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Range(1, int.MaxValue)]
        public int? MaxAttendees { get; set; }

        public bool IsOnline { get; set; } = false;

        [Url]
        public string MeetingUrl { get; set; }
    }

    [Table("event_registrations")]
    public class EventRegistration
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid EventId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        public string Status { get; set; } = "pending";
    }

    public class RegistrationStatus
    {
        public const string Confirmed = "confirmed";
        public const string Pending = "pending";
        public const string Cancelled = "cancelled";
    }
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
    }
}