using Alumni.Domain.Entities;

namespace Alumni.Domain.Interfaces;

public interface IPermissionRepository : IRepository<Permission>
{
    Task<Permission?> GetByCodeAsync(string code);
    Task<IEnumerable<Permission>> GetByModuleAsync(string module);
    Task<IEnumerable<Permission>> GetByModuleAndActionAsync(string module, string action);
    Task<bool> ExistsByCodeAsync(string code);
}
