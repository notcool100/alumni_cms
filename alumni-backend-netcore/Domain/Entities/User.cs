namespace Alumni.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Role { get; private set; }

    private User() { } // For EF Core

    public User(string email, string passwordHash, string firstName, string lastName, string role = "alumni")
    {
        Email = email;
        PasswordHash = passwordHash;
        FirstName = firstName;
        LastName = lastName;
        Role = role;
    }

    public void UpdateProfile(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        UpdateModifiedDate();
    }

    public void UpdatePassword(string newPasswordHash)
    {
        PasswordHash = newPasswordHash;
        UpdateModifiedDate();
    }

    public void UpdateRole(string newRole)
    {
        Role = newRole;
        UpdateModifiedDate();
    }
}
