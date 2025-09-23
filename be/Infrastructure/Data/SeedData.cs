using Microsoft.EntityFrameworkCore;
using Alumni.Domain.Entities;

namespace Alumni.Infrastructure.Data;

public static class SeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        // Seed Roles
        var superAdminRole = new Role("SuperAdmin", "Super Administrator with full system access", true);
        var adminRole = new Role("Admin", "Administrator with management access", true);
        var staffRole = new Role("Staff", "Staff member with limited administrative access", true);
        var alumniRole = new Role("Alumni", "Alumni member with basic access", true);
        var moderatorRole = new Role("Moderator", "Content moderator with approval rights", true);

        // Set specific IDs and dates for seeding
        superAdminRole.Id = Guid.Parse("11111111-1111-1111-1111-111111111111");
        superAdminRole.CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        superAdminRole.UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
        adminRole.Id = Guid.Parse("22222222-2222-2222-2222-222222222222");
        adminRole.CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        adminRole.UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
        staffRole.Id = Guid.Parse("33333333-3333-3333-3333-333333333333");
        staffRole.CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        staffRole.UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
        alumniRole.Id = Guid.Parse("44444444-4444-4444-4444-444444444444");
        alumniRole.CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        alumniRole.UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
        moderatorRole.Id = Guid.Parse("55555555-5555-5555-5555-555555555555");
        moderatorRole.CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        moderatorRole.UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        modelBuilder.Entity<Role>().HasData(
            superAdminRole,
            adminRole,
            staffRole,
            alumniRole,
            moderatorRole
        );

        // Seed Permissions
        var permissions = new List<Permission>
        {
            // User Management
            new Permission("users.view", "User", "View", "View user information") { Id = Guid.Parse("10000000-0000-0000-0000-000000000001") },
            new Permission("users.create", "User", "Create", "Create new users") { Id = Guid.Parse("10000000-0000-0000-0000-000000000002") },
            new Permission("users.edit", "User", "Edit", "Edit user information") { Id = Guid.Parse("10000000-0000-0000-0000-000000000003") },
            new Permission("users.delete", "User", "Delete", "Delete users") { Id = Guid.Parse("10000000-0000-0000-0000-000000000004") },

            // Alumni Management
            new Permission("alumni.view", "Alumni", "View", "View alumni profiles") { Id = Guid.Parse("20000000-0000-0000-0000-000000000001") },
            new Permission("alumni.create", "Alumni", "Create", "Create alumni profiles") { Id = Guid.Parse("20000000-0000-0000-0000-000000000002") },
            new Permission("alumni.edit", "Alumni", "Edit", "Edit alumni profiles") { Id = Guid.Parse("20000000-0000-0000-0000-000000000003") },
            new Permission("alumni.delete", "Alumni", "Delete", "Delete alumni profiles") { Id = Guid.Parse("20000000-0000-0000-0000-000000000004") },
            new Permission("alumni.approve", "Alumni", "Approve", "Approve alumni registrations") { Id = Guid.Parse("20000000-0000-0000-0000-000000000005") },

            // Event Management
            new Permission("events.view", "Event", "View", "View events") { Id = Guid.Parse("30000000-0000-0000-0000-000000000001") },
            new Permission("events.create", "Event", "Create", "Create events") { Id = Guid.Parse("30000000-0000-0000-0000-000000000002") },
            new Permission("events.edit", "Event", "Edit", "Edit events") { Id = Guid.Parse("30000000-0000-0000-0000-000000000003") },
            new Permission("events.delete", "Event", "Delete", "Delete events") { Id = Guid.Parse("30000000-0000-0000-0000-000000000004") },
            new Permission("events.approve", "Event", "Approve", "Approve events") { Id = Guid.Parse("30000000-0000-0000-0000-000000000005") },

            // Content Management
            new Permission("content.view", "Content", "View", "View content") { Id = Guid.Parse("40000000-0000-0000-0000-000000000001") },
            new Permission("content.create", "Content", "Create", "Create content") { Id = Guid.Parse("40000000-0000-0000-0000-000000000002") },
            new Permission("content.edit", "Content", "Edit", "Edit content") { Id = Guid.Parse("40000000-0000-0000-0000-000000000003") },
            new Permission("content.delete", "Content", "Delete", "Delete content") { Id = Guid.Parse("40000000-0000-0000-0000-000000000004") },
            new Permission("content.publish", "Content", "Publish", "Publish content") { Id = Guid.Parse("40000000-0000-0000-0000-000000000005") },

            // Analytics
            new Permission("analytics.view", "Analytics", "View", "View analytics") { Id = Guid.Parse("50000000-0000-0000-0000-000000000001") },
            new Permission("analytics.export", "Analytics", "Export", "Export analytics data") { Id = Guid.Parse("50000000-0000-0000-0000-000000000002") },

            // Role Management
            new Permission("roles.view", "Role", "View", "View roles") { Id = Guid.Parse("60000000-0000-0000-0000-000000000001") },
            new Permission("roles.create", "Role", "Create", "Create roles") { Id = Guid.Parse("60000000-0000-0000-0000-000000000002") },
            new Permission("roles.edit", "Role", "Edit", "Edit roles") { Id = Guid.Parse("60000000-0000-0000-0000-000000000003") },
            new Permission("roles.delete", "Role", "Delete", "Delete roles") { Id = Guid.Parse("60000000-0000-0000-0000-000000000004") },

            // System Settings
            new Permission("settings.view", "Settings", "View", "View system settings") { Id = Guid.Parse("70000000-0000-0000-0000-000000000001") },
            new Permission("settings.edit", "Settings", "Edit", "Edit system settings") { Id = Guid.Parse("70000000-0000-0000-0000-000000000002") }
        };

        // Set dates for all permissions
        foreach (var permission in permissions)
        {
            permission.CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            permission.UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }

        modelBuilder.Entity<Permission>().HasData(permissions);

        // Seed Navigation Groups
        var mainGroup = new NavigationGroup("Main", 1, "Main navigation items") { Id = Guid.Parse("80000000-0000-0000-0000-000000000001") };
        var adminGroup = new NavigationGroup("Administration", 2, "Administrative functions") { Id = Guid.Parse("80000000-0000-0000-0000-000000000002") };
        var contentGroup = new NavigationGroup("Content", 3, "Content management") { Id = Guid.Parse("80000000-0000-0000-0000-000000000003") };
        var analyticsGroup = new NavigationGroup("Analytics", 4, "Analytics and reports") { Id = Guid.Parse("80000000-0000-0000-0000-000000000004") };
        var settingsGroup = new NavigationGroup("Settings", 5, "System settings") { Id = Guid.Parse("80000000-0000-0000-0000-000000000005") };

        // Set dates for navigation groups
        mainGroup.CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        mainGroup.UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        adminGroup.CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        adminGroup.UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        contentGroup.CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        contentGroup.UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        analyticsGroup.CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        analyticsGroup.UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        settingsGroup.CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        settingsGroup.UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        modelBuilder.Entity<NavigationGroup>().HasData(
            mainGroup,
            adminGroup,
            contentGroup,
            analyticsGroup,
            settingsGroup
        );

        // Seed Navigation Items
        var navigationItems = new List<NavigationItem>
        {
            // Main Navigation
            new NavigationItem("Dashboard", 1, "home", "/dashboard", null, mainGroup.Id) { Id = Guid.Parse("90000000-0000-0000-0000-000000000001") },
            new NavigationItem("Alumni", 2, "users", "/alumni", null, mainGroup.Id) { Id = Guid.Parse("90000000-0000-0000-0000-000000000002") },
            new NavigationItem("Events", 3, "calendar", "/events", null, mainGroup.Id) { Id = Guid.Parse("90000000-0000-0000-0000-000000000003") },
            new NavigationItem("Profile", 4, "user", "/profile", null, mainGroup.Id) { Id = Guid.Parse("90000000-0000-0000-0000-000000000004") },

            // Administration Navigation
            new NavigationItem("User Management", 1, "users", "/admin/users", null, adminGroup.Id) { Id = Guid.Parse("90000000-0000-0000-0000-000000000005") },
            new NavigationItem("Role Management", 2, "shield", "/admin/roles", null, adminGroup.Id) { Id = Guid.Parse("90000000-0000-0000-0000-000000000006") },
            new NavigationItem("Alumni Management", 3, "graduation-cap", "/admin/alumni", null, adminGroup.Id) { Id = Guid.Parse("90000000-0000-0000-0000-000000000007") },
            new NavigationItem("Event Management", 4, "calendar", "/admin/events", null, adminGroup.Id) { Id = Guid.Parse("90000000-0000-0000-0000-000000000008") },
            new NavigationItem("Approvals", 5, "check-circle", "/admin/approvals", null, adminGroup.Id) { Id = Guid.Parse("90000000-0000-0000-0000-000000000009") },

            // Content Navigation
            new NavigationItem("Content Management", 1, "file-text", "/admin/content", null, contentGroup.Id) { Id = Guid.Parse("90000000-0000-0000-0000-000000000010") },
            new NavigationItem("News & Updates", 2, "newspaper", "/admin/content/news", null, contentGroup.Id) { Id = Guid.Parse("90000000-0000-0000-0000-000000000011") },
            new NavigationItem("Resources", 3, "folder", "/admin/content/resources", null, contentGroup.Id) { Id = Guid.Parse("90000000-0000-0000-0000-000000000012") },

            // Analytics Navigation
            new NavigationItem("Dashboard Analytics", 1, "bar-chart", "/admin/analytics", null, analyticsGroup.Id) { Id = Guid.Parse("90000000-0000-0000-0000-000000000013") },
            new NavigationItem("Alumni Reports", 2, "users", "/admin/analytics/alumni", null, analyticsGroup.Id) { Id = Guid.Parse("90000000-0000-0000-0000-000000000014") },
            new NavigationItem("Event Reports", 3, "calendar", "/admin/analytics/events", null, analyticsGroup.Id) { Id = Guid.Parse("90000000-0000-0000-0000-000000000015") },
            new NavigationItem("Engagement Metrics", 4, "trending-up", "/admin/analytics/engagement", null, analyticsGroup.Id) { Id = Guid.Parse("90000000-0000-0000-0000-000000000016") },

            // Settings Navigation
            new NavigationItem("System Settings", 1, "settings", "/admin/settings", null, settingsGroup.Id) { Id = Guid.Parse("90000000-0000-0000-0000-000000000017") },
            new NavigationItem("Email Templates", 2, "mail", "/admin/settings/email", null, settingsGroup.Id) { Id = Guid.Parse("90000000-0000-0000-0000-000000000018") },
            new NavigationItem("Navigation", 3, "navigation", "/admin/settings/navigation", null, settingsGroup.Id) { Id = Guid.Parse("90000000-0000-0000-0000-000000000019") }
        };

        // Set dates for navigation items
        foreach (var navItem in navigationItems)
        {
            navItem.CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            navItem.UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }

        modelBuilder.Entity<NavigationItem>().HasData(navigationItems);

        // Seed Role-Permission relationships
        var rolePermissions = new List<RolePermission>();

        // SuperAdmin gets all permissions
        foreach (var permission in permissions)
        {
            rolePermissions.Add(new RolePermission(superAdminRole.Id, permission.Id));
        }

        // Admin gets most permissions except system-level ones
        var adminPermissions = permissions.Where(p => 
            !p.Code.StartsWith("roles.") && 
            !p.Code.StartsWith("settings.") &&
            p.Code != "analytics.export"
        ).ToList();
        
        foreach (var permission in adminPermissions)
        {
            rolePermissions.Add(new RolePermission(adminRole.Id, permission.Id));
        }

        // Staff gets limited permissions
        var staffPermissions = permissions.Where(p => 
            p.Code.StartsWith("alumni.view") ||
            p.Code.StartsWith("events.view") ||
            p.Code.StartsWith("content.view") ||
            p.Code.StartsWith("analytics.view") ||
            p.Code == "events.create" ||
            p.Code == "events.edit" ||
            p.Code == "content.create" ||
            p.Code == "content.edit"
        ).ToList();

        foreach (var permission in staffPermissions)
        {
            rolePermissions.Add(new RolePermission(staffRole.Id, permission.Id));
        }

        // Moderator gets content and approval permissions
        var moderatorPermissions = permissions.Where(p => 
            p.Code.StartsWith("content.") ||
            p.Code == "alumni.approve" ||
            p.Code == "events.approve" ||
            p.Code == "alumni.view" ||
            p.Code == "events.view"
        ).ToList();

        foreach (var permission in moderatorPermissions)
        {
            rolePermissions.Add(new RolePermission(moderatorRole.Id, permission.Id));
        }

        // Alumni gets basic permissions
        var alumniPermissions = permissions.Where(p => 
            p.Code == "alumni.view" ||
            p.Code == "events.view" ||
            p.Code == "content.view"
        ).ToList();

        foreach (var permission in alumniPermissions)
        {
            rolePermissions.Add(new RolePermission(alumniRole.Id, permission.Id));
        }

        // Set dates for role permissions
        foreach (var rolePermission in rolePermissions)
        {
            rolePermission.CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }

        modelBuilder.Entity<RolePermission>().HasData(rolePermissions);

        // Seed Role-Navigation relationships
        var roleNavigations = new List<RoleNavigation>();

        // SuperAdmin gets all navigation
        foreach (var navItem in navigationItems)
        {
            roleNavigations.Add(new RoleNavigation(superAdminRole.Id, navItem.Id));
        }

        // Admin gets most navigation except system settings
        var adminNavItems = navigationItems.Where(n => 
            !n.Url.StartsWith("/admin/settings")
        ).ToList();

        foreach (var navItem in adminNavItems)
        {
            roleNavigations.Add(new RoleNavigation(adminRole.Id, navItem.Id));
        }

        // Staff gets limited navigation
        var staffNavItems = navigationItems.Where(n => 
            n.Url == "/dashboard" ||
            n.Url == "/alumni" ||
            n.Url == "/events" ||
            n.Url == "/profile" ||
            n.Url == "/admin/alumni" ||
            n.Url == "/admin/events" ||
            n.Url == "/admin/content" ||
            n.Url == "/admin/analytics"
        ).ToList();

        foreach (var navItem in staffNavItems)
        {
            roleNavigations.Add(new RoleNavigation(staffRole.Id, navItem.Id));
        }

        // Moderator gets content and approval navigation
        var moderatorNavItems = navigationItems.Where(n => 
            n.Url == "/dashboard" ||
            n.Url == "/alumni" ||
            n.Url == "/events" ||
            n.Url == "/profile" ||
            n.Url == "/admin/approvals" ||
            n.Url == "/admin/content" ||
            n.Url.StartsWith("/admin/content/")
        ).ToList();

        foreach (var navItem in moderatorNavItems)
        {
            roleNavigations.Add(new RoleNavigation(moderatorRole.Id, navItem.Id));
        }

        // Alumni gets basic navigation
        var alumniNavItems = navigationItems.Where(n => 
            n.Url == "/dashboard" ||
            n.Url == "/alumni" ||
            n.Url == "/events" ||
            n.Url == "/profile"
        ).ToList();

        foreach (var navItem in alumniNavItems)
        {
            roleNavigations.Add(new RoleNavigation(alumniRole.Id, navItem.Id));
        }

        // Set dates for role navigations
        foreach (var roleNavigation in roleNavigations)
        {
            roleNavigation.CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }

        modelBuilder.Entity<RoleNavigation>().HasData(roleNavigations);

        // Seed sample users with roles
        var sampleUsers = new List<User>
        {
            new User("admin@alumni.com", "$2a$10$5mp59MZx2bYxkPWDJlHfD.sN.LgcoJPG.jcUEfcHZmbkOgDo9DaMK", "Admin", "User", superAdminRole.Id) 
            { 
                Id = Guid.Parse("a1111111-1111-1111-1111-111111111111"),
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User("staff@alumni.com", "$2a$11$92IXUNpkjO0rOQ5byMi.Ye4oKoEa3Ro9llC/.og/at2.uheWG/igi", "Staff", "User", staffRole.Id) 
            { 
                Id = Guid.Parse("a2222222-2222-2222-2222-222222222222"),
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User("alumni@example.com", "$2a$11$92IXUNpkjO0rOQ5byMi.Ye4oKoEa3Ro9llC/.og/at2.uheWG/igi", "John", "Doe", alumniRole.Id) 
            { 
                Id = Guid.Parse("a3333333-3333-3333-3333-333333333333"),
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        };

        modelBuilder.Entity<User>().HasData(sampleUsers);
    }
}
