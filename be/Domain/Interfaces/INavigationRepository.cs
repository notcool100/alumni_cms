using Alumni.Domain.Entities;

namespace Alumni.Domain.Interfaces;

public interface INavigationRepository : IRepository<NavigationItem>
{
    Task<IEnumerable<NavigationItem>> GetByGroupAsync(Guid groupId);
    Task<IEnumerable<NavigationItem>> GetByRoleAsync(Guid roleId);
    Task<IEnumerable<NavigationItem>> GetActiveItemsAsync();
    Task<IEnumerable<NavigationItem>> GetHierarchicalItemsAsync();
    Task<IEnumerable<NavigationGroup>> GetAllGroupsAsync();
    Task<NavigationGroup?> GetGroupByIdAsync(Guid groupId);
    Task<NavigationGroup?> GetGroupByNameAsync(string name);
}
