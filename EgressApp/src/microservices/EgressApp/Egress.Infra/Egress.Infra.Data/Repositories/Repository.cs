using Egress.Domain.Entities;
using Egress.Domain.Exceptions;
using Egress.Domain.Utils;
using Egress.Infra.CrossCutting.Resource;
using Egress.Infra.Data.Context;
using Egress.Infra.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Egress.Infra.Data.Repositories;

/// <summary>
/// Generic repository
/// </summary>
/// <typeparam name="TEntity">Entity type</typeparam>
public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly ApplicationDbContext Context;
    
    protected readonly DbSet<TEntity> DbSet;

    public Repository(ApplicationDbContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }
    
    /// <summary>
    /// Get entity by id
    /// </summary>
    /// <param name="id">Guid identifier</param>
    /// <returns>Entity</returns>
    public async Task<TEntity?> GetByIdAsync(Guid id)
        => await DbSet.SingleOrDefaultAsync(e => e.Id.Equals(id));

    /// <summary>
    /// Get paged entity list
    /// </summary>
    /// <param name="parameters">Page number and page size</param>
    /// <returns>Entity paged list</returns>
    public Task<PagedList<TEntity>> GetPaginate(PaginationParameters parameters)
        => Task.FromResult(new PagedList<TEntity>(
                DbSet.OrderBy(i => i.Id),
                parameters.PageNumber,
                parameters.PageSize
            ));

    /// <summary>
    /// Create entity in db
    /// </summary>
    /// <param name="entity">Entity to add</param>
    /// <returns>Entity with Guid identifier</returns>
    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = entity.CreatedAt;
        
        DbSet.Add(entity);
        await Context.SaveChangesAsync();

        return entity;
    }
    
    /// <summary>
    /// Update entity in db
    /// </summary>
    /// <param name="entity">Entity to update</param>
    /// <returns>Updated entity</returns>
    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var data = await GetByIdAsync(entity.Id);

        if (data is null)
            throw new BusinessException(string.Format(ErrorCodeResource.NOT_FOUND_ERROR, nameof(TEntity)));
        
        data.UpdatedAt = DateTime.UtcNow;
        
        Context.Entry(data).CurrentValues.SetValues(entity);
        Context.Entry(data).Property(e => e.CreatedAt).IsModified = false;

        await Context.SaveChangesAsync();

        return data;
    }

    /// <summary>
    /// Delete entity in db
    /// </summary>
    /// <param name="id">Guid identifier</param>
    /// <returns>Deleted entity</returns>
    public async Task<TEntity> DeleteAsync(Guid id)
    {
        var data = await GetByIdAsync(id);

        if (data is null)
            throw new BusinessException(string.Format(ErrorCodeResource.NOT_FOUND_ERROR, nameof(TEntity)));

        DbSet.Remove(data);
        await Context.SaveChangesAsync();

        return data;
    }
}