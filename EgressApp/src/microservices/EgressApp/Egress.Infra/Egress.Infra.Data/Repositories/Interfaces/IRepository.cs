using Egress.Domain.Entities;
using Egress.Domain.Utils;

namespace Egress.Infra.Data.Repositories.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id);
    
    Task<PagedList<T>> GetPaginate(PaginationParameters parameters);

    Task<T> CreateAsync(T entity);

    Task<T> UpdateAsync(T entity);

    Task<T> DeleteAsync(Guid id);
}