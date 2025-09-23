namespace Alumni.Domain.Entities;

public class RolePermission : BaseEntity
{
    public Guid RoleId { get; private set; }
    public Guid PermissionId { get; private set; }

    // Navigation properties
    public virtual Role Role { get; private set; }
    public virtual Permission Permission { get; private set; }

    private RolePermission() { } // For EF Core

    public RolePermission(Guid roleId, Guid permissionId)
    {
        RoleId = roleId;
        PermissionId = permissionId;
    }
}
