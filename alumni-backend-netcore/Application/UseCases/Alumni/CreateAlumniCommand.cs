using MediatR;
using Alumni.Application.DTOs;
using Alumni.Domain.Interfaces;
using AlumniEntity = Alumni.Domain.Entities.Alumni;

namespace Alumni.Application.UseCases.Alumni;

public record CreateAlumniCommand : IRequest<ApiResponse<AlumniResponse>>
{
    public Guid UserId { get; init; }
    public int GraduationYear { get; init; }
    public string Degree { get; init; } = string.Empty;
    public string Major { get; init; } = string.Empty;
    public string? CurrentCompany { get; init; }
    public string? CurrentPosition { get; init; }
    public string? Location { get; init; }
    public string? Bio { get; init; }
    public string? LinkedinUrl { get; init; }
    public string? GithubUrl { get; init; }
    public string? WebsiteUrl { get; init; }
    public bool IsPublic { get; init; }
}

public class CreateAlumniCommandHandler : IRequestHandler<CreateAlumniCommand, ApiResponse<AlumniResponse>>
{
    private readonly IAlumniRepository _alumniRepository;
    private readonly IUserRepository _userRepository;

    public CreateAlumniCommandHandler(IAlumniRepository alumniRepository, IUserRepository userRepository)
    {
        _alumniRepository = alumniRepository;
        _userRepository = userRepository;
    }

    public async Task<ApiResponse<AlumniResponse>> Handle(CreateAlumniCommand request, CancellationToken cancellationToken)
    {
        // Check if user exists
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user == null)
        {
            return new ApiResponse<AlumniResponse>
            {
                Success = false,
                Message = "User not found"
            };
        }

        // Check if alumni profile already exists for this user
        var existingAlumni = await _alumniRepository.GetByUserIdAsync(request.UserId);
        if (existingAlumni != null)
        {
            return new ApiResponse<AlumniResponse>
            {
                Success = false,
                Message = "Alumni profile already exists for this user"
            };
        }

        // Create new alumni profile
        var alumni = new AlumniEntity(
            request.UserId,
            request.GraduationYear,
            request.Degree,
            request.Major,
            request.CurrentCompany,
            request.CurrentPosition,
            request.Location,
            request.Bio,
            request.LinkedinUrl,
            request.GithubUrl,
            request.WebsiteUrl,
            null,
            request.IsPublic
        );

        await _alumniRepository.AddAsync(alumni);

        // Get the created alumni with user data
        var createdAlumni = await _alumniRepository.GetByUserIdAsync(request.UserId);

        var response = new AlumniResponse
        {
            Id = createdAlumni!.Id,
            UserId = createdAlumni.UserId,
            User = new UserResponse
            {
                Id = createdAlumni.User.Id,
                Email = createdAlumni.User.Email,
                FirstName = createdAlumni.User.FirstName,
                LastName = createdAlumni.User.LastName,
                Role = createdAlumni.User.Role
            },
            GraduationYear = createdAlumni.GraduationYear,
            Degree = createdAlumni.Degree,
            Major = createdAlumni.Major,
            CurrentCompany = createdAlumni.CurrentCompany,
            CurrentPosition = createdAlumni.CurrentPosition,
            Location = createdAlumni.Location,
            Bio = createdAlumni.Bio,
            LinkedinUrl = createdAlumni.LinkedinUrl,
            GithubUrl = createdAlumni.GithubUrl,
            WebsiteUrl = createdAlumni.WebsiteUrl,
            ProfileImageUrl = createdAlumni.ProfileImageUrl,
            IsPublic = createdAlumni.IsPublic,
            CreatedAt = createdAlumni.CreatedAt,
            UpdatedAt = createdAlumni.UpdatedAt
        };

        return new ApiResponse<AlumniResponse>
        {
            Success = true,
            Data = response,
            Message = "Alumni profile created successfully"
        };
    }
}
