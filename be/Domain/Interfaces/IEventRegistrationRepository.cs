using Alumni.Domain.Entities;

namespace Alumni.Domain.Interfaces;

public interface IEventRegistrationRepository : IRepository<EventRegistration>
{
    Task<IEnumerable<EventRegistration>> GetByEventIdAsync(Guid eventId);
    Task<IEnumerable<EventRegistration>> GetByUserIdAsync(Guid userId);
    Task<EventRegistration?> GetByEventAndUserAsync(Guid eventId, Guid userId);
    Task<bool> UserIsRegisteredForEventAsync(Guid eventId, Guid userId);
    Task<int> GetRegistrationCountForEventAsync(Guid eventId);
}
