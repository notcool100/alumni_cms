using Microsoft.EntityFrameworkCore;
using Alumni.Domain.Entities;
using Alumni.Domain.Interfaces;
using Alumni.Infrastructure.Data;

namespace Alumni.Infrastructure.Repositories;

public class AlumniRepository : BaseRepository<Alumni.Domain.Entities.Alumni>, IAlumniRepository
{
    public AlumniRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Alumni.Domain.Entities.Alumni?> GetByUserIdAsync(Guid userId)
    {
        return await _dbSet
            .Include(a => a.User)
            .FirstOrDefaultAsync(a => a.UserId == userId);
    }

    public async Task<IEnumerable<Alumni.Domain.Entities.Alumni>> GetPublicAlumniAsync()
    {
        return await _dbSet
            .Include(a => a.User)
            .Where(a => a.IsPublic)
            .ToListAsync();
    }

    public async Task<IEnumerable<Alumni.Domain.Entities.Alumni>> GetByGraduationYearAsync(int year)
    {
        return await _dbSet
            .Include(a => a.User)
            .Where(a => a.GraduationYear == year && a.IsPublic)
            .ToListAsync();
    }

    public async Task<IEnumerable<Alumni.Domain.Entities.Alumni>> GetByMajorAsync(string major)
    {
        return await _dbSet
            .Include(a => a.User)
            .Where(a => a.Major.Contains(major) && a.IsPublic)
            .ToListAsync();
    }

    public async Task<IEnumerable<Alumni.Domain.Entities.Alumni>> GetByCompanyAsync(string company)
    {
        return await _dbSet
            .Include(a => a.User)
            .Where(a => a.CurrentCompany != null && a.CurrentCompany.Contains(company) && a.IsPublic)
            .ToListAsync();
    }
}
