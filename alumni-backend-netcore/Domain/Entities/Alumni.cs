namespace Alumni.Domain.Entities;

public class Alumni : BaseEntity
{
    public Guid UserId { get; private set; }
    public int GraduationYear { get; private set; }
    public string Degree { get; private set; }
    public string Major { get; private set; }
    public string? CurrentCompany { get; private set; }
    public string? CurrentPosition { get; private set; }
    public string? Location { get; private set; }
    public string? Bio { get; private set; }
    public string? LinkedinUrl { get; private set; }
    public string? GithubUrl { get; private set; }
    public string? WebsiteUrl { get; private set; }
    public string? ProfileImageUrl { get; private set; }
    public bool IsPublic { get; private set; }

    // Navigation properties
    public User User { get; private set; }

    private Alumni() { } // For EF Core

    public Alumni(Guid userId, int graduationYear, string degree, string major, string? currentCompany = null, 
        string? currentPosition = null, string? location = null, string? bio = null, string? linkedinUrl = null, 
        string? githubUrl = null, string? websiteUrl = null, string? profileImageUrl = null, bool isPublic = true)
    {
        UserId = userId;
        GraduationYear = graduationYear;
        Degree = degree;
        Major = major;
        CurrentCompany = currentCompany;
        CurrentPosition = currentPosition;
        Location = location;
        Bio = bio;
        LinkedinUrl = linkedinUrl;
        GithubUrl = githubUrl;
        WebsiteUrl = websiteUrl;
        ProfileImageUrl = profileImageUrl;
        IsPublic = isPublic;
    }

    public void UpdateProfile(int graduationYear, string degree, string major, string? currentCompany, 
        string? currentPosition, string? location, string? bio, string? linkedinUrl, string? githubUrl, 
        string? websiteUrl, string? profileImageUrl, bool isPublic)
    {
        GraduationYear = graduationYear;
        Degree = degree;
        Major = major;
        CurrentCompany = currentCompany;
        CurrentPosition = currentPosition;
        Location = location;
        Bio = bio;
        LinkedinUrl = linkedinUrl;
        GithubUrl = githubUrl;
        WebsiteUrl = websiteUrl;
        ProfileImageUrl = profileImageUrl;
        IsPublic = isPublic;
        UpdateModifiedDate();
    }

    public void UpdateProfileImage(string profileImageUrl)
    {
        ProfileImageUrl = profileImageUrl;
        UpdateModifiedDate();
    }

    public void TogglePublicVisibility()
    {
        IsPublic = !IsPublic;
        UpdateModifiedDate();
    }
}
