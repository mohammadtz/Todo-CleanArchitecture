using Microsoft.EntityFrameworkCore;
using Todo.Application.Contract.Utils;
using Todo.Domain.Tag;

namespace Todo.Infrastructure.EFCore.Repositories;

public class TagRepository : ITagRepository
{
    private readonly TodoDbContext _dbContext;

    public TagRepository(TodoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Tag?>> GetAllAsync()
    {
        return await _dbContext.Tags.ToListAsync();
    }

    public async Task<Tag?> GetAsync(int id)
    {
        var result = await _dbContext.Tags.SingleOrDefaultAsync(x => x != null && x.Id == id);

        return result;
    }

    public async Task<bool> IsValid(int id)
    {
        return await _dbContext.Tags.AnyAsync(x => x.Id == id);
    }

    public async Task Insert(Tag? entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        await _dbContext.Tags.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Tag? entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        _dbContext.Tags.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Tag? entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        _dbContext.Tags.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var entity = await GetAsync(id);
        await Delete(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}