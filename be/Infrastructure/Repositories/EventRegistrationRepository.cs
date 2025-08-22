using Microsoft.EntityFrameworkCore;
using Alumni.Domain.Entities;
using Alumni.Domain.Interfaces;
using Alumni.Infrastructure.Data;

namespace Alumni.Infrastructure.Repositories;

public class EventRegistrationRepository : BaseRepository<EventRegistration>, IEventRegistrationRepository
{
    public EventRegistrationRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<EventRegistration>> GetByEventIdAsync(Guid eventId)
    {
        return await _dbSet
            .Include(er => er.User)
            .Include(er => er.Event)
            .Where(er => er.EventId == eventId)
            .ToListAsync();
    }

    public async Task<IEnumerable<EventRegistration>> GetByUserIdAsync(Guid userId)
    {
        return await _dbSet
            .Include(er => er.Event)
            .Include(er => er.User)
            .Where(er => er.UserId == userId)
            .ToListAsync();
    }

    public async Task<EventRegistration?> GetByEventAndUserAsync(Guid eventId, Guid userId)
    {
        return await _dbSet
            .Include(er => er.Event)
            .Include(er => er.User)
            .FirstOrDefaultAsync(er => er.EventId == eventId && er.UserId == userId);
    }

    public async Task<bool> UserIsRegisteredForEventAsync(Guid eventId, Guid userId)
    {
        return await _dbSet.AnyAsync(er => er.EventId == eventId && er.UserId == userId);
    }

    public async Task<int> GetRegistrationCountForEventAsync(Guid eventId)
    {
        return await _dbSet.CountAsync(er => er.EventId == eventId && er.Status == RegistrationStatus.Confirmed);
    }
}
