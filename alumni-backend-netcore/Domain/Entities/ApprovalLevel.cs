namespace Alumni.Domain.Entities;

public class ApprovalLevel : BaseEntity
{
    public string Name { get; private set; }
    public int Level { get; private set; }
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }
    public Guid RoleId { get; private set; }

    // Navigation properties
    public virtual Role Role { get; private set; }

    private ApprovalLevel() { } // For EF Core

    public ApprovalLevel(string name, int level, Guid roleId, string? description = null, bool isActive = true)
    {
        Name = name;
        Level = level;
        RoleId = roleId;
        Description = description;
        IsActive = isActive;
    }

    public void UpdateApprovalLevel(string name, int level, string? description = null)
    {
        Name = name;
        Level = level;
        Description = description;
        UpdateModifiedDate();
    }

    public void SetActive(bool isActive)
    {
        this.IsActive = isActive;
        UpdateModifiedDate();
    }
}
