namespace Alumni.Domain.Entities;

public class Permission : BaseEntity
{
    public string Code { get; private set; }
    public string? Description { get; private set; }
    public string Module { get; private set; }
    public string Action { get; private set; }

    // Navigation properties
    public virtual ICollection<RolePermission> RolePermissions { get; private set; } = new List<RolePermission>();

    private Permission() { } // For EF Core

    public Permission(string code, string module, string action, string? description = null)
    {
        Code = code;
        Module = module;
        Action = action;
        Description = description;
    }

    public void UpdatePermission(string code, string module, string action, string? description = null)
    {
        Code = code;
        Module = module;
        Action = action;
        Description = description;
        UpdateModifiedDate();
    }
}
