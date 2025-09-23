namespace Alumni.Domain.Entities;

public class Event : BaseEntity
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string? Location { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public int? MaxAttendees { get; private set; }
    public int CurrentAttendees { get; private set; }
    public bool IsOnline { get; private set; }
    public string? MeetingUrl { get; private set; }
    public Guid CreatedBy { get; private set; }

    // Navigation properties
    public User Creator { get; private set; }
    public ICollection<EventRegistration> Registrations { get; private set; } = new List<EventRegistration>();

    private Event() { } // For EF Core

    public Event(string title, string description, DateTime startDate, DateTime endDate, Guid createdBy, 
        string? location = null, int? maxAttendees = null, bool isOnline = false, string? meetingUrl = null)
    {
        Title = title;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        CreatedBy = createdBy;
        Location = location;
        MaxAttendees = maxAttendees;
        IsOnline = isOnline;
        MeetingUrl = meetingUrl;
        CurrentAttendees = 0;
    }

    public void UpdateEvent(string title, string description, DateTime startDate, DateTime endDate, 
        string? location, int? maxAttendees, bool isOnline, string? meetingUrl)
    {
        Title = title;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        Location = location;
        MaxAttendees = maxAttendees;
        IsOnline = isOnline;
        MeetingUrl = meetingUrl;
        UpdateModifiedDate();
    }

    public bool CanRegister()
    {
        return MaxAttendees == null || CurrentAttendees < MaxAttendees;
    }

    public void IncrementAttendeeCount()
    {
        if (CanRegister())
        {
            CurrentAttendees++;
            UpdateModifiedDate();
        }
    }

    public void DecrementAttendeeCount()
    {
        if (CurrentAttendees > 0)
        {
            CurrentAttendees--;
            UpdateModifiedDate();
        }
    }

    public bool IsEventFull()
    {
        return MaxAttendees.HasValue && CurrentAttendees >= MaxAttendees.Value;
    }

    public bool IsEventInPast()
    {
        return DateTime.UtcNow > EndDate;
    }

    public bool IsEventUpcoming()
    {
        return DateTime.UtcNow < StartDate;
    }
}
