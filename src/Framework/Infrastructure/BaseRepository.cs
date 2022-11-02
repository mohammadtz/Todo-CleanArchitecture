using System.Linq.Expressions;
using Framework.Domain;
using Microsoft.EntityFrameworkCore;

namespace Framework.Infrastructure;

public class BaseRepository<TKey, T> : IBaseRepository<TKey, T> where T : BaseEntity<TKey>
                                                                where TKey : struct
{
    private readonly DbContext _dbContext;

    public BaseRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<T?> GetAsync(TKey key)
    {
        return await _dbContext.FindAsync<T>(key);
    }

    public bool Exist(Expression<Func<T, bool>> expression)
    {
        return _dbContext.Set<T>().Any(expression);
    }

    public async Task Create(T? entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        await _dbContext.AddAsync<T>(entity);
    }

    public void Update(T? entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        _dbContext.Update<T>(entity);
    }

    public void Delete(T? entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        _dbContext.Remove<T>(entity);
    }

    public async Task Delete(TKey key)
    {
        if (string.IsNullOrWhiteSpace(key.ToString())) throw new ArgumentNullException(nameof(key));

        var entity = await GetAsync(key);

        Delete(entity);
    }
}