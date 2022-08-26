using Microsoft.EntityFrameworkCore;
using Todo.Domain.Todo;

namespace Todo.Infrastructure.EFCore.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly TodoDbContext _dbContext;

    public TodoRepository(TodoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Domain.Todo.Todo?>> GetAllAsync()
    {
        return await _dbContext.Tasks.ToListAsync();
    }

    public async Task<Domain.Todo.Todo?> GetAsync(int id)
    {
        return await _dbContext.Tasks.FirstOrDefaultAsync(x => x != null && x.Id == id);
    }

    public async Task Insert(Domain.Todo.Todo? entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        await _dbContext.Tasks.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Domain.Todo.Todo? entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        _dbContext.Tasks.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var entity = await GetAsync(id);
        await Delete(entity);
    }

    public async Task Delete(Domain.Todo.Todo? entity)
    {        
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        _dbContext.Tasks.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}