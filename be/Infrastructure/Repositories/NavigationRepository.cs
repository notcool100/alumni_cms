using Microsoft.EntityFrameworkCore;
using Alumni.Domain.Entities;
using Alumni.Domain.Interfaces;
using Alumni.Infrastructure.Data;

namespace Alumni.Infrastructure.Repositories;

public class NavigationRepository : INavigationRepository
{
    private readonly AppDbContext _context;

    public NavigationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<NavigationItem?> GetByIdAsync(Guid id)
    {
        return await _context.NavigationItems
            .Include(n => n.Parent)
            .Include(n => n.Children)
            .Include(n => n.Group)
            .Include(n => n.RoleNavigations)
                .ThenInclude(rn => rn.Role)
            .FirstOrDefaultAsync(n => n.Id == id);
    }

    public async Task<IEnumerable<NavigationItem>> GetAllAsync()
    {
        return await _context.NavigationItems
            .Include(n => n.Parent)
            .Include(n => n.Children)
            .Include(n => n.Group)
            .Include(n => n.RoleNavigations)
                .ThenInclude(rn => rn.Role)
            .ToListAsync();
    }

    public async Task<NavigationItem> AddAsync(NavigationItem entity)
    {
        await _context.NavigationItems.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(NavigationItem entity)
    {
        _context.NavigationItems.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(NavigationItem entity)
    {
        _context.NavigationItems.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.NavigationItems.AnyAsync(n => n.Id == id);
    }

    public async Task<IEnumerable<NavigationItem>> GetByGroupAsync(Guid groupId)
    {
        return await _context.NavigationItems
            .Where(n => n.GroupId == groupId && n.IsActive)
            .OrderBy(n => n.Order)
            .ToListAsync();
    }

    public async Task<IEnumerable<NavigationItem>> GetByRoleAsync(Guid roleId)
    {
        return await _context.RoleNavigations
            .Where(rn => rn.RoleId == roleId)
            .Include(rn => rn.NavigationItem)
                .ThenInclude(n => n.Parent)
            .Include(rn => rn.NavigationItem)
                .ThenInclude(n => n.Group)
            .Where(rn => rn.NavigationItem.IsActive)
            .Select(rn => rn.NavigationItem)
            .OrderBy(n => n.Order)
            .ToListAsync();
    }

    public async Task<IEnumerable<NavigationItem>> GetActiveItemsAsync()
    {
        return await _context.NavigationItems
            .Where(n => n.IsActive)
            .Include(n => n.Parent)
            .Include(n => n.Group)
            .OrderBy(n => n.Order)
            .ToListAsync();
    }

    public async Task<IEnumerable<NavigationItem>> GetHierarchicalItemsAsync()
    {
        return await _context.NavigationItems
            .Where(n => n.IsActive && n.ParentId == null)
            .Include(n => n.Children.Where(c => c.IsActive))
            .Include(n => n.Group)
            .OrderBy(n => n.Order)
            .ToListAsync();
    }

    public async Task<IEnumerable<NavigationGroup>> GetAllGroupsAsync()
    {
        return await _context.NavigationGroups
            .Where(g => g.IsActive)
            .Include(g => g.NavigationItems.Where(n => n.IsActive))
            .OrderBy(g => g.Order)
            .ToListAsync();
    }

    public async Task<NavigationGroup?> GetGroupByIdAsync(Guid groupId)
    {
        return await _context.NavigationGroups
            .Include(g => g.NavigationItems.Where(n => n.IsActive))
            .FirstOrDefaultAsync(g => g.Id == groupId);
    }

    public async Task<NavigationGroup?> GetGroupByNameAsync(string name)
    {
        return await _context.NavigationGroups
            .Include(g => g.NavigationItems.Where(n => n.IsActive))
            .FirstOrDefaultAsync(g => g.Name == name);
    }
}
