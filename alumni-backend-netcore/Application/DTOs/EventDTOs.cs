namespace Alumni.Application.DTOs;

public class CreateEventRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Location { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int? MaxAttendees { get; set; }
    public bool IsOnline { get; set; } = false;
    public string? MeetingUrl { get; set; }
}

public class UpdateEventRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Location { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int? MaxAttendees { get; set; }
    public bool IsOnline { get; set; } = false;
    public string? MeetingUrl { get; set; }
}

public class EventResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Location { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int? MaxAttendees { get; set; }
    public int CurrentAttendees { get; set; }
    public bool IsOnline { get; set; }
    public string? MeetingUrl { get; set; }
    public Guid CreatedBy { get; set; }
    public UserResponse Creator { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsEventFull { get; set; }
    public bool IsEventInPast { get; set; }
    public bool IsEventUpcoming { get; set; }
}

public class EventListResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Location { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int? MaxAttendees { get; set; }
    public int CurrentAttendees { get; set; }
    public bool IsOnline { get; set; }
    public string CreatorName { get; set; } = string.Empty;
    public bool IsEventFull { get; set; }
    public bool IsEventInPast { get; set; }
    public bool IsEventUpcoming { get; set; }
}
