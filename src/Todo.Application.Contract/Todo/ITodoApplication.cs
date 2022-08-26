using Todo.Domain.Todo;

namespace Todo.Application.Contract.Todo;

public interface ITodoApplication
{
    Task<List<TodoDto>> GetAll();
    Task<TodoDto> GetById(int id);
    Task ChangeStatus(int id);
    Task Create(TodoCommand command);
    Task Update(int id, TodoCommand command);
    Task Delete(int id);
}