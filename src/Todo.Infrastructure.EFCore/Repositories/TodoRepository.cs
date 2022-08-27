using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Todo.Domain.Tag;
using Todo.Domain.Todo;

namespace Todo.Infrastructure.EFCore.Repositories;

public class TodoRepository : BaseRepository<int,Domain.Todo.Todo>, ITodoRepository
{
    private readonly TodoDbContext _dbContext;

    public TodoRepository(TodoDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<TodoDto>> GetListAsync()
    {
        var query = await (from todo in _dbContext.Tasks
            let tagDto = todo.Tag != null ? new TagDto(todo.Tag) : null
            select new TodoDto(todo, tagDto)).ToListAsync();

        return query;
    }
}