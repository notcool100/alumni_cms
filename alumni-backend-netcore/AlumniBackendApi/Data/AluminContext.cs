using Microsoft.EntityFrameworkCore;
using AlumniBackendApi.Models;

public class AlumniContext : DbContext
{
    public AlumniContext(DbContextOptions<AlumniContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Alumni> Alumni { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<EventRegistration> EventRegistrations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.Property(e => e.FirstName).IsRequired();
            entity.Property(e => e.LastName).IsRequired();
            entity.Property(e => e.Role).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt).IsRequired();
        });

        modelBuilder.Entity<Alumni>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.UserId).IsRequired();
            entity.Property(e => e.GraduationYear).IsRequired();
            entity.Property(e => e.Degree).IsRequired();
            entity.Property(e => e.Major).IsRequired();
            entity.Property(e => e.IsPublic).HasDefaultValue(true);
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt).IsRequired();
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Title).IsRequired();
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.StartDate).IsRequired();
            entity.Property(e => e.EndDate).IsRequired();
            entity.Property(e => e.CreatedBy).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt).IsRequired();
            entity.Property(e => e.CurrentAttendees).HasDefaultValue(0);
            entity.Property(e => e.IsOnline).HasDefaultValue(false);
        });

        modelBuilder.Entity<EventRegistration>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.EventId).IsRequired();
            entity.Property(e => e.UserId).IsRequired();
            entity.Property(e => e.RegistrationDate).HasDefaultValue(DateTime.UtcNow);
            entity.Property(e => e.Status).HasDefaultValue("pending");
        });

        base.OnModelCreating(modelBuilder);
    }
}