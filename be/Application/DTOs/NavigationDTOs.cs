namespace Alumni.Application.DTOs;

public class NavigationGroupDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Order { get; set; }
    public bool IsActive { get; set; }
    public List<NavigationItemDTO> NavigationItems { get; set; } = new();
}

public class NavigationItemDTO
{
    public Guid Id { get; set; }
    public string Label { get; set; } = string.Empty;
    public string? Icon { get; set; }
    public string? Url { get; set; }
    public int Order { get; set; }
    public Guid? ParentId { get; set; }
    public Guid? GroupId { get; set; }
    public bool IsActive { get; set; }
    public List<NavigationItemDTO> Children { get; set; } = new();
}

public class UserNavigationDTO
{
    public List<NavigationGroupDTO> Groups { get; set; } = new();
    public List<NavigationItemDTO> FlatItems { get; set; } = new();
}
