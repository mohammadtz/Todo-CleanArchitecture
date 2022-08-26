using Todo.Application.Contract.Tag;
using Todo.Application.Contract.Utils;
using Todo.Domain.Exceptions;
using Todo.Domain.Tag;

namespace Todo.Application;

public class TagApplication : ITagApplication
{
    private readonly ITagRepository _tagRepository;
    private readonly ILogger _logger;

    public TagApplication(ITagRepository tagRepository, ILogger logger)
    {
        _tagRepository = tagRepository;
        _logger = logger;
    }

    public async Task<List<TagDto>> GetAll()
    {
        var result = await _tagRepository.GetAllAsync();

        return result?.Select(x => new TagDto(x)).ToList() ?? new List<TagDto>();
    }

    public async Task<TagDto> GetById(int id)
    {
        var tag = await _tagRepository.GetAsync(id);

        if (tag == null) throw new NotFoundException(nameof(Tag));

        return new TagDto(tag);
    }

    public async Task Create(TagCommand command)
    {
        if(command == null) throw new ArgumentNullException(nameof(command));
        if (command.Name is null) throw new NotFoundException(nameof(command.Name));

        var entity = new Tag(command.Name, command.Color);
        await _tagRepository.Insert(entity);

        _logger.Log(nameof(Tag).IsCreated());
    }

    public async Task Update(int id, TagCommand command)
    {
        var entity = await _tagRepository.GetAsync(id);

        if (entity is null) throw new ArgumentNullException(nameof(command));
        if (command.Name is null) throw new NotFoundException(nameof(command.Name));

        entity.Color = command.Color ?? "ffffff";
        entity.Name = command.Name;

        await _tagRepository.Update(entity);

        _logger.Log(nameof(Tag).IsUpdated());
    }

    public async Task Delete(int id)
    {
        await _tagRepository.Delete(id);

        _logger.Log(nameof(Tag).IsDeleted());
    }
}
