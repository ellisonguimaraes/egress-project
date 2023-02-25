using Egress.Domain.Entities;

namespace Egress.Infra.Data.Repositories.Interfaces;

public interface IPersonCourseRepository : IRepository<PersonCourse>
{
    Task<PersonCourse?> GetByMatAsync(string mat);
}
