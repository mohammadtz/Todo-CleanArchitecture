using Todo.Application.Contract.Tag;
using Todo.Domain.Tag;

namespace Todo.Application;

public class TagApplication : ITagApplication
{
    private readonly ITagRepository _tagRepository;

    public TagApplication(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task<List<TagDto>> GetAll()
    {
        var result = await _tagRepository.GetAllAsync();

        return result?.Select(x => new TagDto(x)).ToList() ?? new List<TagDto>();
    }

    public async Task<TagDto> GetById(int id)
    {
        var tag = await _tagRepository.GetAsync(id);
        return new TagDto(tag);
    }

    public async Task Create(TagCommand command)
    {
        var entity = new Tag(command.Name, command.Color);
        await _tagRepository.Insert(entity);
    }

    public async Task Update(int id, TagCommand command)
    {
        var entity = await _tagRepository.GetAsync(id);

        if (entity is null) throw new ArgumentNullException(nameof(entity));

        entity.Color = command.Color;
        entity.Name = command.Name;

        await _tagRepository.Update(entity);
    }

    public async Task Delete(int id)
    {
        await _tagRepository.Delete(id);
    }
}
