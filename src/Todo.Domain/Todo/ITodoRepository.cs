using Framework.Infrastructure;

namespace Todo.Domain.Todo;

public interface ITodoRepository:IBaseRepository<int, Todo>
{
    Task<IEnumerable<TodoDto>> GetListAsync();
}
