using Microsoft.EntityFrameworkCore;
using Wenlin.Application.Contracts.Persistence;

namespace Wenlin.Persistence.Repositories;

public class BaseRepository<T> : IAsyncRepository<T> where T : class
{
    protected readonly WenlinDbContext _dbContext;

    public BaseRepository(WenlinDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public virtual async Task<T> GetByIdAsync(Guid id)
    {
#pragma warning disable CS8603 // Possible null reference return.
        return await _dbContext.Set<T>().FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}

