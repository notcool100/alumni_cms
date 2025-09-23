using MediatR;
using Alumni.Application.DTOs;
using Alumni.Domain.Interfaces;
using Alumni.Domain.Entities;

namespace Alumni.Application.UseCases.Navigation;

public class GetUserNavigationQuery : IRequest<UserNavigationDTO>
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}

public class GetUserNavigationQueryHandler : IRequestHandler<GetUserNavigationQuery, UserNavigationDTO>
{
    private readonly INavigationRepository _navigationRepository;
    private readonly IRoleRepository _roleRepository;

    public GetUserNavigationQueryHandler(INavigationRepository navigationRepository, IRoleRepository roleRepository)
    {
        _navigationRepository = navigationRepository;
        _roleRepository = roleRepository;
    }

    public async Task<UserNavigationDTO> Handle(GetUserNavigationQuery request, CancellationToken cancellationToken)
    {
        // Get navigation items for the user's role
        var navigationItems = await _roleRepository.GetRoleNavigationAsync(request.RoleId);
        
        // Get all groups
        var groups = await _navigationRepository.GetAllGroupsAsync();
        
        // Create DTOs
        var result = new UserNavigationDTO();
        
        // Convert navigation items to DTOs
        var itemDTOs = navigationItems.Select(item => new NavigationItemDTO
        {
            Id = item.Id,
            Label = item.Label,
            Icon = item.Icon,
            Url = item.Url,
            Order = item.Order,
            ParentId = item.ParentId,
            GroupId = item.GroupId,
            IsActive = item.IsActive,
            Children = item.Children.Select(child => new NavigationItemDTO
            {
                Id = child.Id,
                Label = child.Label,
                Icon = child.Icon,
                Url = child.Url,
                Order = child.Order,
                ParentId = child.ParentId,
                GroupId = child.GroupId,
                IsActive = child.IsActive,
                Children = new List<NavigationItemDTO>()
            }).ToList()
        }).ToList();
        
        // Convert groups to DTOs
        var groupDTOs = groups.Select(group => new NavigationGroupDTO
        {
            Id = group.Id,
            Name = group.Name,
            Description = group.Description,
            Order = group.Order,
            IsActive = group.IsActive,
            NavigationItems = group.NavigationItems
                .Where(item => navigationItems.Any(ni => ni.Id == item.Id))
                .Select(item => new NavigationItemDTO
                {
                    Id = item.Id,
                    Label = item.Label,
                    Icon = item.Icon,
                    Url = item.Url,
                    Order = item.Order,
                    ParentId = item.ParentId,
                    GroupId = item.GroupId,
                    IsActive = item.IsActive,
                    Children = item.Children
                        .Where(child => navigationItems.Any(ni => ni.Id == child.Id))
                        .Select(child => new NavigationItemDTO
                        {
                            Id = child.Id,
                            Label = child.Label,
                            Icon = child.Icon,
                            Url = child.Url,
                            Order = child.Order,
                            ParentId = child.ParentId,
                            GroupId = child.GroupId,
                            IsActive = child.IsActive,
                            Children = new List<NavigationItemDTO>()
                        }).ToList()
                }).ToList()
        }).ToList();
        
        result.Groups = groupDTOs;
        result.FlatItems = itemDTOs;
        
        return result;
    }
}
