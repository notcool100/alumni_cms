using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Alumni.Domain.Entities;

namespace Alumni.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(warnings => warnings
            .Ignore(RelationalEventId.PendingModelChangesWarning));
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Alumni.Domain.Entities.Alumni> Alumni { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<EventRegistration> EventRegistrations { get; set; }
    
    // Role-based access control
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<ApprovalLevel> ApprovalLevels { get; set; }
    
    // Navigation
    public DbSet<NavigationGroup> NavigationGroups { get; set; }
    public DbSet<NavigationItem> NavigationItems { get; set; }
    public DbSet<RoleNavigation> RoleNavigations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Suppress the warning about pending model changes
        modelBuilder.HasAnnotation("Relational:Collation", null);
        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(e => e.Email).HasColumnName("email").IsRequired();
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash").IsRequired();
            entity.Property(e => e.FirstName).HasColumnName("first_name").IsRequired();
            entity.Property(e => e.LastName).HasColumnName("last_name").IsRequired();
            entity.Property(e => e.RoleId).HasColumnName("role_id").IsRequired();
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired();
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").IsRequired();
            
            entity.HasIndex(e => e.Email).IsUnique();
            
            entity.HasOne(e => e.Role)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Alumni configuration
        modelBuilder.Entity<Alumni.Domain.Entities.Alumni>(entity =>
        {
            entity.ToTable("alumni");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(e => e.UserId).HasColumnName("user_id").IsRequired();
            entity.Property(e => e.GraduationYear).HasColumnName("graduation_year").IsRequired();
            entity.Property(e => e.Degree).HasColumnName("degree").IsRequired();
            entity.Property(e => e.Major).HasColumnName("major").IsRequired();
            entity.Property(e => e.CurrentCompany).HasColumnName("current_company");
            entity.Property(e => e.CurrentPosition).HasColumnName("current_position");
            entity.Property(e => e.Location).HasColumnName("location");
            entity.Property(e => e.Bio).HasColumnName("bio");
            entity.Property(e => e.LinkedinUrl).HasColumnName("linkedin_url");
            entity.Property(e => e.GithubUrl).HasColumnName("github_url");
            entity.Property(e => e.WebsiteUrl).HasColumnName("website_url");
            entity.Property(e => e.ProfileImageUrl).HasColumnName("profile_image_url");
            entity.Property(e => e.IsPublic).HasColumnName("is_public").HasDefaultValue(true);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired();
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").IsRequired();

            entity.HasOne(e => e.User)
                .WithOne()
                .HasForeignKey<Alumni.Domain.Entities.Alumni>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Event configuration
        modelBuilder.Entity<Event>(entity =>
        {
            entity.ToTable("events");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(e => e.Title).HasColumnName("title").IsRequired();
            entity.Property(e => e.Description).HasColumnName("description").IsRequired();
            entity.Property(e => e.Location).HasColumnName("location");
            entity.Property(e => e.StartDate).HasColumnName("start_date").IsRequired();
            entity.Property(e => e.EndDate).HasColumnName("end_date").IsRequired();
            entity.Property(e => e.MaxAttendees).HasColumnName("max_attendees");
            entity.Property(e => e.CurrentAttendees).HasColumnName("current_attendees").HasDefaultValue(0);
            entity.Property(e => e.IsOnline).HasColumnName("is_online").HasDefaultValue(false);
            entity.Property(e => e.MeetingUrl).HasColumnName("meeting_url");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by").IsRequired();
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired();
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").IsRequired();

            entity.HasOne(e => e.Creator)
                .WithMany()
                .HasForeignKey(e => e.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // EventRegistration configuration
        modelBuilder.Entity<EventRegistration>(entity =>
        {
            entity.ToTable("event_registrations");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(e => e.EventId).HasColumnName("event_id").IsRequired();
            entity.Property(e => e.UserId).HasColumnName("user_id").IsRequired();
            entity.Property(e => e.RegistrationDate).HasColumnName("registration_date").HasDefaultValue(DateTime.UtcNow);
            entity.Property(e => e.Status).HasColumnName("status").HasDefaultValue(RegistrationStatus.Pending);

            entity.HasOne(e => e.Event)
                .WithMany(e => e.Registrations)
                .HasForeignKey(e => e.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => new { e.EventId, e.UserId }).IsUnique();
        });

        // Role configuration
        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("roles");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired();
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsSystem).HasColumnName("is_system").HasDefaultValue(false);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired();
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").IsRequired();
            
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // Permission configuration
        modelBuilder.Entity<Permission>(entity =>
        {
            entity.ToTable("permissions");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(e => e.Code).HasColumnName("code").IsRequired();
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Module).HasColumnName("module").IsRequired();
            entity.Property(e => e.Action).HasColumnName("action").IsRequired();
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired();
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").IsRequired();
            
            entity.HasIndex(e => e.Code).IsUnique();
        });

        // RolePermission configuration
        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.ToTable("role_permissions");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(e => e.RoleId).HasColumnName("role_id").IsRequired();
            entity.Property(e => e.PermissionId).HasColumnName("permission_id").IsRequired();
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired();

            entity.HasOne(e => e.Role)
                .WithMany(e => e.RolePermissions)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Permission)
                .WithMany(e => e.RolePermissions)
                .HasForeignKey(e => e.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => new { e.RoleId, e.PermissionId }).IsUnique();
        });



        // ApprovalLevel configuration
        modelBuilder.Entity<ApprovalLevel>(entity =>
        {
            entity.ToTable("approval_levels");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired();
            entity.Property(e => e.Level).HasColumnName("level").IsRequired();
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsActive).HasColumnName("is_active").HasDefaultValue(true);
            entity.Property(e => e.RoleId).HasColumnName("role_id").IsRequired();
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired();
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").IsRequired();

            entity.HasOne(e => e.Role)
                .WithMany(e => e.ApprovalLevels)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // NavigationGroup configuration
        modelBuilder.Entity<NavigationGroup>(entity =>
        {
            entity.ToTable("navigation_groups");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired();
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Order).HasColumnName("order").IsRequired();
            entity.Property(e => e.IsActive).HasColumnName("is_active").HasDefaultValue(true);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired();
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").IsRequired();
            
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // NavigationItem configuration
        modelBuilder.Entity<NavigationItem>(entity =>
        {
            entity.ToTable("navigation_items");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(e => e.Label).HasColumnName("label").IsRequired();
            entity.Property(e => e.Icon).HasColumnName("icon");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.Order).HasColumnName("order").IsRequired();
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active").HasDefaultValue(true);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired();
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").IsRequired();

            entity.HasOne(e => e.Parent)
                .WithMany(e => e.Children)
                .HasForeignKey(e => e.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Group)
                .WithMany(e => e.NavigationItems)
                .HasForeignKey(e => e.GroupId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // RoleNavigation configuration
        modelBuilder.Entity<RoleNavigation>(entity =>
        {
            entity.ToTable("role_navigation");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(e => e.RoleId).HasColumnName("role_id").IsRequired();
            entity.Property(e => e.NavigationItemId).HasColumnName("navigation_item_id").IsRequired();
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired();

            entity.HasOne(e => e.Role)
                .WithMany(e => e.RoleNavigations)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.NavigationItem)
                .WithMany(e => e.RoleNavigations)
                .HasForeignKey(e => e.NavigationItemId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => new { e.RoleId, e.NavigationItemId }).IsUnique();
        });

        base.OnModelCreating(modelBuilder);
        
        // Seed data
        SeedData.Seed(modelBuilder);
    }
}
