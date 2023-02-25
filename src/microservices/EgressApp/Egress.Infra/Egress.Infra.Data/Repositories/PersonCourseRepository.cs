using Egress.Domain.Entities;
using Egress.Infra.Data.Context;
using Egress.Infra.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Egress.Infra.Data.Repositories;

public class PersonCourseRepository : Repository<PersonCourse>, IPersonCourseRepository
{
    public PersonCourseRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<PersonCourse?> GetByMatAsync(string mat)
        => DbSet
            .Include(pc => pc.Course)
            .Include(pc => pc.Person)
            .Include(pc => pc.Person)
                .ThenInclude(p => p.Employments)
            .Include(pc => pc.Person)
                .ThenInclude(p => p.Address)
            .Include(pc => pc.Person)
                .ThenInclude(p => p.PersonCourses)
            .Include(pc => pc.Person)
                .ThenInclude(p => p.Highlights)
            .Include(pc => pc.Person)
                .ThenInclude(p => p.Specializations)
            .Include(pc => pc.Person)
                .ThenInclude(p => p.Testimonies)
            .SingleOrDefault(pc => pc.Mat.Equals(mat));
}
