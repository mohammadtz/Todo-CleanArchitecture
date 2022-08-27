using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Todo.Application.Contract.Utils;
using Todo.Domain.Exceptions;
using Todo.Domain.Tag;

namespace Todo.Infrastructure.EFCore.Repositories;

public class TagRepository : BaseRepository<int, Tag>, ITagRepository
{
    private readonly TodoDbContext _dbContext;

    public TagRepository(TodoDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<TagDto>> GetListAsync()
    {
        return await _dbContext.Tags.Select(tag => new TagDto(tag)).ToListAsync();
    }
}