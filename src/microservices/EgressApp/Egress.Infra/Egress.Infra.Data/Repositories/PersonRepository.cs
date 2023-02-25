using Egress.Domain.Entities;
using Egress.Infra.Data.Context;
using Egress.Infra.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Egress.Infra.Data.Repositories;

public class PersonRepository : Repository<Person>, IPersonRepository
{
    public PersonRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Get person by cpf
    /// </summary>
    /// <param name="cpf">cpf</param>
    /// <returns>Person</returns>
    public Task<Person?> GetByCpfAsync(string cpf)
        => Task.FromResult(DbSet
            .Include(p => p.Employments)
            .Include(p => p.Address)
            .Include(p => p.PersonCourses)
            .Include(p => p.Highlights)
            .Include(p => p.Specializations)
            .Include(p => p.Testimonies)
            .SingleOrDefault(p => p.Cpf.Equals(cpf)));
}
