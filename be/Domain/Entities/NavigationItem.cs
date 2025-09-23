namespace Alumni.Domain.Entities;

public class NavigationItem : BaseEntity
{
    public string Label { get; private set; }
    public string? Icon { get; private set; }
    public string? Url { get; private set; }
    public int Order { get; private set; }
    public Guid? ParentId { get; private set; }
    public Guid? GroupId { get; private set; }
    public bool IsActive { get; private set; }

    // Navigation properties
    public virtual NavigationItem? Parent { get; private set; }
    public virtual ICollection<NavigationItem> Children { get; private set; } = new List<NavigationItem>();
    public virtual NavigationGroup? Group { get; private set; }
    public virtual ICollection<RoleNavigation> RoleNavigations { get; private set; } = new List<RoleNavigation>();

    private NavigationItem() { } // For EF Core

    public NavigationItem(string label, int order, string? icon = null, string? url = null, Guid? parentId = null, Guid? groupId = null, bool isActive = true)
    {
        Label = label;
        Order = order;
        Icon = icon;
        Url = url;
        ParentId = parentId;
        GroupId = groupId;
        IsActive = isActive;
    }

    public void UpdateItem(string label, int order, string? icon = null, string? url = null)
    {
        Label = label;
        Order = order;
        Icon = icon;
        Url = url;
        UpdateModifiedDate();
    }

    public void SetParent(Guid? parentId)
    {
        ParentId = parentId;
        UpdateModifiedDate();
    }

    public void SetGroup(Guid? groupId)
    {
        GroupId = groupId;
        UpdateModifiedDate();
    }

    public void SetActive(bool isActive)
    {
        this.IsActive = isActive;
        UpdateModifiedDate();
    }

    public void AddChild(NavigationItem child)
    {
        Children.Add(child);
        UpdateModifiedDate();
    }

    public void RemoveChild(Guid childId)
    {
        var child = Children.FirstOrDefault(c => c.Id == childId);
        if (child != null)
        {
            Children.Remove(child);
            UpdateModifiedDate();
        }
    }
}
