using Alumni.Domain.Entities;

namespace Alumni.Domain.Interfaces;

public interface IEventRepository : IRepository<Event>
{
    Task<IEnumerable<Event>> GetUpcomingEventsAsync();
    Task<IEnumerable<Event>> GetPastEventsAsync();
    Task<IEnumerable<Event>> GetEventsByCreatorAsync(Guid creatorId);
    Task<IEnumerable<Event>> GetEventsByDateRangeAsync(DateTime startDate, DateTime endDate);
}
