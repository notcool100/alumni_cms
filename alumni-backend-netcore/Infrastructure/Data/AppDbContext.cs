using Microsoft.EntityFrameworkCore;
using Alumni.Domain.Entities;

namespace Alumni.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Alumni.Domain.Entities.Alumni> Alumni { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<EventRegistration> EventRegistrations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
            entity.Property(e => e.Role).HasColumnName("role").IsRequired();
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired();
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").IsRequired();
            
            entity.HasIndex(e => e.Email).IsUnique();
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

        base.OnModelCreating(modelBuilder);
    }
}
