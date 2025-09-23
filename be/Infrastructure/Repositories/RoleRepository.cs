using Microsoft.EntityFrameworkCore;
using Alumni.Domain.Entities;
using Alumni.Domain.Interfaces;
using Alumni.Infrastructure.Data;

namespace Alumni.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _context;

    public RoleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Role?> GetByIdAsync(Guid id)
    {
        return await _context.Roles
            .Include(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
            .Include(r => r.RoleNavigations)
                .ThenInclude(rn => rn.NavigationItem)
            .Include(r => r.Users)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Role>> GetAllAsync()
    {
        return await _context.Roles
            .Include(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
            .Include(r => r.RoleNavigations)
                .ThenInclude(rn => rn.NavigationItem)
            .ToListAsync();
    }

    public async Task<Role> AddAsync(Role entity)
    {
        await _context.Roles.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(Role entity)
    {
        _context.Roles.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Role entity)
    {
        _context.Roles.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Roles.AnyAsync(r => r.Id == id);
    }

    public async Task<Role?> GetByNameAsync(string name)
    {
        return await _context.Roles
            .Include(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
            .Include(r => r.RoleNavigations)
                .ThenInclude(rn => rn.NavigationItem)
            .FirstOrDefaultAsync(r => r.Name == name);
    }

    public async Task<IEnumerable<Role>> GetSystemRolesAsync()
    {
        return await _context.Roles
            .Where(r => r.IsSystem)
            .ToListAsync();
    }

    public async Task<IEnumerable<Role>> GetNonSystemRolesAsync()
    {
        return await _context.Roles
            .Where(r => !r.IsSystem)
            .ToListAsync();
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Roles.AnyAsync(r => r.Name == name);
    }

    public async Task<IEnumerable<Permission>> GetRolePermissionsAsync(Guid roleId)
    {
        return await _context.RolePermissions
            .Where(rp => rp.RoleId == roleId)
            .Include(rp => rp.Permission)
            .Select(rp => rp.Permission)
            .ToListAsync();
    }

    public async Task<IEnumerable<NavigationItem>> GetRoleNavigationAsync(Guid roleId)
    {
        return await _context.RoleNavigations
            .Where(rn => rn.RoleId == roleId)
            .Include(rn => rn.NavigationItem)
            .Select(rn => rn.NavigationItem)
            .ToListAsync();
    }
}
