using Microsoft.EntityFrameworkCore;
using Alumni.Domain.Entities;
using Alumni.Domain.Interfaces;
using Alumni.Infrastructure.Data;

namespace Alumni.Infrastructure.Repositories;

public class EventRepository : BaseRepository<Event>, IEventRepository
{
    public EventRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Event>> GetUpcomingEventsAsync()
    {
        return await _dbSet
            .Include(e => e.Creator)
            .Where(e => e.StartDate > DateTime.UtcNow)
            .OrderBy(e => e.StartDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Event>> GetPastEventsAsync()
    {
        return await _dbSet
            .Include(e => e.Creator)
            .Where(e => e.EndDate < DateTime.UtcNow)
            .OrderByDescending(e => e.StartDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Event>> GetEventsByCreatorAsync(Guid creatorId)
    {
        return await _dbSet
            .Include(e => e.Creator)
            .Where(e => e.CreatedBy == creatorId)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Event>> GetEventsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _dbSet
            .Include(e => e.Creator)
            .Where(e => e.StartDate >= startDate && e.EndDate <= endDate)
            .OrderBy(e => e.StartDate)
            .ToListAsync();
    }
}
