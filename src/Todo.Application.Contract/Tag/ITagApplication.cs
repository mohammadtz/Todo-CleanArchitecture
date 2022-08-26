using Todo.Domain.Tag;

namespace Todo.Application.Contract.Tag;

public interface ITagApplication
{
    Task<List<TagDto>> GetAll();
    Task<TagDto> GetById(int id);
    Task Create(TagCommand command);
    Task Update(int id, TagCommand command);
    Task Delete(int id);
}