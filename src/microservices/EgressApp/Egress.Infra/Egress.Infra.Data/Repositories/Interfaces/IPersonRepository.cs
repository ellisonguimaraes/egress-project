using Egress.Domain.Entities;

namespace Egress.Infra.Data.Repositories.Interfaces;

public interface IPersonRepository : IRepository<Person>
{
    Task<Person?> GetByCpfAsync(string cpf);
}
