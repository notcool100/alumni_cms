namespace Alumni.Application.DTOs;

public class CreateAlumniRequest
{
    public int GraduationYear { get; set; }
    public string Degree { get; set; } = string.Empty;
    public string Major { get; set; } = string.Empty;
    public string? CurrentCompany { get; set; }
    public string? CurrentPosition { get; set; }
    public string? Location { get; set; }
    public string? Bio { get; set; }
    public string? LinkedinUrl { get; set; }
    public string? GithubUrl { get; set; }
    public string? WebsiteUrl { get; set; }
    public bool IsPublic { get; set; } = true;
}

public class UpdateAlumniRequest
{
    public int GraduationYear { get; set; }
    public string Degree { get; set; } = string.Empty;
    public string Major { get; set; } = string.Empty;
    public string? CurrentCompany { get; set; }
    public string? CurrentPosition { get; set; }
    public string? Location { get; set; }
    public string? Bio { get; set; }
    public string? LinkedinUrl { get; set; }
    public string? GithubUrl { get; set; }
    public string? WebsiteUrl { get; set; }
    public bool IsPublic { get; set; }
}

public class AlumniResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public UserResponse User { get; set; } = new();
    public int GraduationYear { get; set; }
    public string Degree { get; set; } = string.Empty;
    public string Major { get; set; } = string.Empty;
    public string? CurrentCompany { get; set; }
    public string? CurrentPosition { get; set; }
    public string? Location { get; set; }
    public string? Bio { get; set; }
    public string? LinkedinUrl { get; set; }
    public string? GithubUrl { get; set; }
    public string? WebsiteUrl { get; set; }
    public string? ProfileImageUrl { get; set; }
    public bool IsPublic { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class AlumniListResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int GraduationYear { get; set; }
    public string Degree { get; set; } = string.Empty;
    public string Major { get; set; } = string.Empty;
    public string? CurrentCompany { get; set; }
    public string? CurrentPosition { get; set; }
    public string? Location { get; set; }
    public string? ProfileImageUrl { get; set; }
}
