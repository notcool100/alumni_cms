using Alumni.Domain.Entities;

namespace Alumni.Domain.Interfaces;

public interface IRoleRepository : IRepository<Role>
{
    Task<Role?> GetByNameAsync(string name);
    Task<IEnumerable<Role>> GetSystemRolesAsync();
    Task<IEnumerable<Role>> GetNonSystemRolesAsync();
    Task<bool> ExistsByNameAsync(string name);
    Task<IEnumerable<Permission>> GetRolePermissionsAsync(Guid roleId);
    Task<IEnumerable<NavigationItem>> GetRoleNavigationAsync(Guid roleId);
}
