namespace Alumni.Domain.Entities;

public class NavigationGroup : BaseEntity
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public int Order { get; private set; }
    public bool IsActive { get; private set; }

    // Navigation properties
    public virtual ICollection<NavigationItem> NavigationItems { get; private set; } = new List<NavigationItem>();

    private NavigationGroup() { } // For EF Core

    public NavigationGroup(string name, int order, string? description = null, bool isActive = true)
    {
        Name = name;
        Order = order;
        Description = description;
        IsActive = isActive;
    }

    public void UpdateGroup(string name, int order, string? description = null)
    {
        Name = name;
        Order = order;
        Description = description;
        UpdateModifiedDate();
    }

    public void SetActive(bool isActive)
    {
        this.IsActive = isActive;
        UpdateModifiedDate();
    }

    public void AddNavigationItem(NavigationItem navigationItem)
    {
        NavigationItems.Add(navigationItem);
        UpdateModifiedDate();
    }

    public void RemoveNavigationItem(Guid navigationItemId)
    {
        var navigationItem = NavigationItems.FirstOrDefault(ni => ni.Id == navigationItemId);
        if (navigationItem != null)
        {
            NavigationItems.Remove(navigationItem);
            UpdateModifiedDate();
        }
    }
}
