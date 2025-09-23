using Alumni.Domain.Entities;

namespace Alumni.Domain.Interfaces;

public interface IAlumniRepository : IRepository<Alumni.Domain.Entities.Alumni>
{
    Task<Alumni.Domain.Entities.Alumni?> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<Alumni.Domain.Entities.Alumni>> GetPublicAlumniAsync();
    Task<IEnumerable<Alumni.Domain.Entities.Alumni>> GetByGraduationYearAsync(int year);
    Task<IEnumerable<Alumni.Domain.Entities.Alumni>> GetByMajorAsync(string major);
    Task<IEnumerable<Alumni.Domain.Entities.Alumni>> GetByCompanyAsync(string company);
}
