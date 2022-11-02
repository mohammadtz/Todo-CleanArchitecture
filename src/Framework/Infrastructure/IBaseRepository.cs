using System.Linq.Expressions;
using Framework.Domain;

namespace Framework.Infrastructure;

public interface IBaseRepository<in TKey, T> where T : BaseEntity<TKey>
                                             where TKey : struct
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetAsync(TKey key);
    bool Exist(Expression<Func<T, bool>> expression);
    Task Create(T? entity);
    void Update(T? entity);
    void Delete(T? entity);
    Task Delete(TKey key);
}
