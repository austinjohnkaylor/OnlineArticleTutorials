using System.Linq.Expressions;
using API.Data;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private ApplicationDbContext _context;
    protected readonly DbSet<T> DbSet;
    protected readonly ILogger Logger;

    public GenericRepository(
        ApplicationDbContext context,
        ILogger logger)
    {
        _context = context;
        DbSet = context.Set<T>();
        Logger = logger;
    }

    public virtual async Task<T> GetById(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public virtual async Task<bool> Add(T entity)
    {
        await DbSet.AddAsync(entity);
        return true;
    }

    public virtual Task<bool> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public virtual Task<IEnumerable<T>> All()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
    {
        return await DbSet.Where(predicate).ToListAsync();
    }

    public virtual Task<bool> Upsert(T entity)
    {
        throw new NotImplementedException();
    }
}
