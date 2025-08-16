using MediatR;
using Alumni.Application.DTOs;
using Alumni.Domain.Interfaces;

namespace Alumni.Application.UseCases.Alumni;

public record UpdateAlumniCommand : IRequest<ApiResponse<AlumniResponse>>
{
    public Guid Id { get; init; }
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

public class UpdateAlumniCommandHandler : IRequestHandler<UpdateAlumniCommand, ApiResponse<AlumniResponse>>
{
    private readonly IAlumniRepository _alumniRepository;

    public UpdateAlumniCommandHandler(IAlumniRepository alumniRepository)
    {
        _alumniRepository = alumniRepository;
    }

    public async Task<ApiResponse<AlumniResponse>> Handle(UpdateAlumniCommand request, CancellationToken cancellationToken)
    {
        var alumni = await _alumniRepository.GetByIdAsync(request.Id);
        
        if (alumni == null)
        {
            return new ApiResponse<AlumniResponse>
            {
                Success = false,
                Message = "Alumni profile not found"
            };
        }

        // Check if the user owns this alumni profile
        if (alumni.UserId != request.UserId)
        {
            return new ApiResponse<AlumniResponse>
            {
                Success = false,
                Message = "You can only update your own alumni profile"
            };
        }

        // Update the alumni profile
        alumni.UpdateProfile(
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
            alumni.ProfileImageUrl,
            request.IsPublic
        );

        await _alumniRepository.UpdateAsync(alumni);

        // Get the updated alumni with user data
        var updatedAlumni = await _alumniRepository.GetByUserIdAsync(request.UserId);

        var response = new AlumniResponse
        {
            Id = updatedAlumni!.Id,
            UserId = updatedAlumni.UserId,
            User = new UserResponse
            {
                Id = updatedAlumni.User.Id,
                Email = updatedAlumni.User.Email,
                FirstName = updatedAlumni.User.FirstName,
                LastName = updatedAlumni.User.LastName,
                Role = updatedAlumni.User.Role
            },
            GraduationYear = updatedAlumni.GraduationYear,
            Degree = updatedAlumni.Degree,
            Major = updatedAlumni.Major,
            CurrentCompany = updatedAlumni.CurrentCompany,
            CurrentPosition = updatedAlumni.CurrentPosition,
            Location = updatedAlumni.Location,
            Bio = updatedAlumni.Bio,
            LinkedinUrl = updatedAlumni.LinkedinUrl,
            GithubUrl = updatedAlumni.GithubUrl,
            WebsiteUrl = updatedAlumni.WebsiteUrl,
            ProfileImageUrl = updatedAlumni.ProfileImageUrl,
            IsPublic = updatedAlumni.IsPublic,
            CreatedAt = updatedAlumni.CreatedAt,
            UpdatedAt = updatedAlumni.UpdatedAt
        };

        return new ApiResponse<AlumniResponse>
        {
            Success = true,
            Data = response,
            Message = "Alumni profile updated successfully"
        };
    }
}
