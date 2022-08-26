namespace Todo.Domain.Todo;

public interface ITodoRepository
{
    Task<IEnumerable<Todo?>> GetAllAsync();
    Task<Todo?> GetAsync(int id);
    Task Insert(Todo? entity);
    Task Update(Todo? entity);
    Task Delete(int id);
    Task Delete(Todo? entity);
    Task SaveChangesAsync();
}
