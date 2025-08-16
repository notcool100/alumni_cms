namespace Alumni.Domain.Entities;

public class EventRegistration : BaseEntity
{
    public Guid EventId { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime RegistrationDate { get; private set; }
    public RegistrationStatus Status { get; private set; }

    // Navigation properties
    public Event Event { get; private set; }
    public User User { get; private set; }

    private EventRegistration() { } // For EF Core

    public EventRegistration(Guid eventId, Guid userId)
    {
        EventId = eventId;
        UserId = userId;
        RegistrationDate = DateTime.UtcNow;
        Status = RegistrationStatus.Pending;
    }

    public void Confirm()
    {
        Status = RegistrationStatus.Confirmed;
        UpdateModifiedDate();
    }

    public void Cancel()
    {
        Status = RegistrationStatus.Cancelled;
        UpdateModifiedDate();
    }

    public bool IsConfirmed => Status == RegistrationStatus.Confirmed;
    public bool IsPending => Status == RegistrationStatus.Pending;
    public bool IsCancelled => Status == RegistrationStatus.Cancelled;
}

public enum RegistrationStatus
{
    Pending,
    Confirmed,
    Cancelled
}
