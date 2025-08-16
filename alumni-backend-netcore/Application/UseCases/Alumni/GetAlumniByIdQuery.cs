using MediatR;
using Alumni.Application.DTOs;
using Alumni.Domain.Interfaces;

namespace Alumni.Application.UseCases.Alumni;

public record GetAlumniByIdQuery : IRequest<ApiResponse<AlumniResponse>>
{
    public Guid Id { get; init; }
}

public class GetAlumniByIdQueryHandler : IRequestHandler<GetAlumniByIdQuery, ApiResponse<AlumniResponse>>
{
    private readonly IAlumniRepository _alumniRepository;

    public GetAlumniByIdQueryHandler(IAlumniRepository alumniRepository)
    {
        _alumniRepository = alumniRepository;
    }

    public async Task<ApiResponse<AlumniResponse>> Handle(GetAlumniByIdQuery request, CancellationToken cancellationToken)
    {
        var alumni = await _alumniRepository.GetByIdAsync(request.Id);
        
        if (alumni == null)
        {
            return new ApiResponse<AlumniResponse>
            {
                Success = false,
                Message = "Alumni not found"
            };
        }

        if (!alumni.IsPublic)
        {
            return new ApiResponse<AlumniResponse>
            {
                Success = false,
                Message = "Alumni profile is private"
            };
        }

        var response = new AlumniResponse
        {
            Id = alumni.Id,
            UserId = alumni.UserId,
            User = new UserResponse
            {
                Id = alumni.User.Id,
                Email = alumni.User.Email,
                FirstName = alumni.User.FirstName,
                LastName = alumni.User.LastName,
                Role = alumni.User.Role
            },
            GraduationYear = alumni.GraduationYear,
            Degree = alumni.Degree,
            Major = alumni.Major,
            CurrentCompany = alumni.CurrentCompany,
            CurrentPosition = alumni.CurrentPosition,
            Location = alumni.Location,
            Bio = alumni.Bio,
            LinkedinUrl = alumni.LinkedinUrl,
            GithubUrl = alumni.GithubUrl,
            WebsiteUrl = alumni.WebsiteUrl,
            ProfileImageUrl = alumni.ProfileImageUrl,
            IsPublic = alumni.IsPublic,
            CreatedAt = alumni.CreatedAt,
            UpdatedAt = alumni.UpdatedAt
        };

        return new ApiResponse<AlumniResponse>
        {
            Success = true,
            Data = response
        };
    }
}
