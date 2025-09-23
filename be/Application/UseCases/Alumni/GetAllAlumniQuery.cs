using MediatR;
using Alumni.Application.DTOs;
using Alumni.Domain.Interfaces;

namespace Alumni.Application.UseCases.Alumni;

public record GetAllAlumniQuery : IRequest<ApiResponse<List<AlumniListResponse>>>;

public class GetAllAlumniQueryHandler : IRequestHandler<GetAllAlumniQuery, ApiResponse<List<AlumniListResponse>>>
{
    private readonly IAlumniRepository _alumniRepository;

    public GetAllAlumniQueryHandler(IAlumniRepository alumniRepository)
    {
        _alumniRepository = alumniRepository;
    }

    public async Task<ApiResponse<List<AlumniListResponse>>> Handle(GetAllAlumniQuery request, CancellationToken cancellationToken)
    {
        var alumni = await _alumniRepository.GetPublicAlumniAsync();
        
        var response = alumni.Select(a => new AlumniListResponse
        {
            Id = a.Id,
            FirstName = a.User.FirstName,
            LastName = a.User.LastName,
            GraduationYear = a.GraduationYear,
            Degree = a.Degree,
            Major = a.Major,
            CurrentCompany = a.CurrentCompany,
            CurrentPosition = a.CurrentPosition,
            Location = a.Location,
            ProfileImageUrl = a.ProfileImageUrl
        }).ToList();

        return new ApiResponse<List<AlumniListResponse>>
        {
            Success = true,
            Data = response
        };
    }
}
