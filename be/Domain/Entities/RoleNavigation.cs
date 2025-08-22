namespace Alumni.Domain.Entities;

public class RoleNavigation : BaseEntity
{
    public Guid RoleId { get; private set; }
    public Guid NavigationItemId { get; private set; }

    // Navigation properties
    public virtual Role Role { get; private set; }
    public virtual NavigationItem NavigationItem { get; private set; }

    private RoleNavigation() { } // For EF Core

    public RoleNavigation(Guid roleId, Guid navigationItemId)
    {
        RoleId = roleId;
        NavigationItemId = navigationItemId;
    }
}
