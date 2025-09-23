namespace Alumni.Domain.Entities;

public class Role : BaseEntity
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public bool IsSystem { get; private set; }

    // Navigation properties
    public virtual ICollection<RolePermission> RolePermissions { get; private set; } = new List<RolePermission>();
    public virtual ICollection<RoleNavigation> RoleNavigations { get; private set; } = new List<RoleNavigation>();
    public virtual ICollection<ApprovalLevel> ApprovalLevels { get; private set; } = new List<ApprovalLevel>();
    public virtual ICollection<User> Users { get; private set; } = new List<User>();

    private Role() { } // For EF Core

    public Role(string name, string? description = null, bool isSystem = false)
    {
        Name = name;
        Description = description;
        IsSystem = isSystem;
    }

    public void UpdateRole(string name, string? description = null)
    {
        Name = name;
        Description = description;
        UpdateModifiedDate();
    }

    public void SetAsSystemRole()
    {
        IsSystem = true;
        UpdateModifiedDate();
    }

    public void AddPermission(RolePermission rolePermission)
    {
        if (!RolePermissions.Any(rp => rp.PermissionId == rolePermission.PermissionId))
        {
            RolePermissions.Add(rolePermission);
            UpdateModifiedDate();
        }
    }

    public void RemovePermission(Guid permissionId)
    {
        var rolePermission = RolePermissions.FirstOrDefault(rp => rp.PermissionId == permissionId);
        if (rolePermission != null)
        {
            RolePermissions.Remove(rolePermission);
            UpdateModifiedDate();
        }
    }

    public void AddNavigation(RoleNavigation roleNavigation)
    {
        if (!RoleNavigations.Any(rn => rn.NavigationItemId == roleNavigation.NavigationItemId))
        {
            RoleNavigations.Add(roleNavigation);
            UpdateModifiedDate();
        }
    }

    public void RemoveNavigation(Guid navigationItemId)
    {
        var roleNavigation = RoleNavigations.FirstOrDefault(rn => rn.NavigationItemId == navigationItemId);
        if (roleNavigation != null)
        {
            RoleNavigations.Remove(roleNavigation);
            UpdateModifiedDate();
        }
    }
}
