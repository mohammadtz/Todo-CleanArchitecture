using Framework.Infrastructure;

namespace Todo.Domain.Tag;

public interface ITagRepository : IBaseRepository<int, Tag>
{
    Task<IEnumerable<TagDto>> GetListAsync();
}