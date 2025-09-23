using Microsoft.EntityFrameworkCore;
using Alumni.Domain.Entities;
using Alumni.Domain.Interfaces;
using Alumni.Infrastructure.Data;

namespace Alumni.Infrastructure.Repositories;

public class PermissionRepository : IPermissionRepository
{
    private readonly AppDbContext _context;

    public PermissionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Permission?> GetByIdAsync(Guid id)
    {
        return await _context.Permissions
            .Include(p => p.RolePermissions)
                .ThenInclude(rp => rp.Role)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Permission>> GetAllAsync()
    {
        return await _context.Permissions
            .Include(p => p.RolePermissions)
                .ThenInclude(rp => rp.Role)
            .ToListAsync();
    }

    public async Task<Permission> AddAsync(Permission entity)
    {
        await _context.Permissions.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(Permission entity)
    {
        _context.Permissions.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Permission entity)
    {
        _context.Permissions.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Permissions.AnyAsync(p => p.Id == id);
    }

    public async Task<Permission?> GetByCodeAsync(string code)
    {
        return await _context.Permissions
            .Include(p => p.RolePermissions)
                .ThenInclude(rp => rp.Role)
            .FirstOrDefaultAsync(p => p.Code == code);
    }

    public async Task<IEnumerable<Permission>> GetByModuleAsync(string module)
    {
        return await _context.Permissions
            .Where(p => p.Module == module)
            .ToListAsync();
    }

    public async Task<IEnumerable<Permission>> GetByModuleAndActionAsync(string module, string action)
    {
        return await _context.Permissions
            .Where(p => p.Module == module && p.Action == action)
            .ToListAsync();
    }

    public async Task<bool> ExistsByCodeAsync(string code)
    {
        return await _context.Permissions.AnyAsync(p => p.Code == code);
    }
}
