using Framework.Infrastructure;
using Todo.Application.Contract.Tag;
using Todo.Application.Contract.Utils;
using Todo.Domain.Exceptions;
using Todo.Domain.Tag;

namespace Todo.Application;

public class TagApplication : ITagApplication
{
    private readonly ITagRepository _tagRepository;
    private readonly ILogger _logger;
    private readonly IUnitOfWork _unitOfWork;

    public TagApplication(ITagRepository tagRepository, ILogger logger, IUnitOfWork unitOfWork)
    {
        _tagRepository = tagRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<TagDto>> GetAll()
    {
        var result = await _tagRepository.GetListAsync();

        return result.ToList();
    }

    public async Task<TagDto> GetById(int id)
    {
        var tag = await _tagRepository.GetAsync(id);

        if (tag == null) throw new NotFoundException(nameof(Tag));

        return new TagDto(tag);
    }

    public async Task Create(TagCommand command)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            if (command == null) throw new ArgumentNullException(nameof(command));
            if (command.Name is null) throw new NotFoundException(nameof(command.Name));

            var entity = new Tag(command.Name, command.Color);
            await _tagRepository.Create(entity);

            await _unitOfWork.CommitTransactionAsync();

            _logger.Log(nameof(Tag).IsCreated());
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task Update(int id, TagCommand command)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var entity = await _tagRepository.GetAsync(id);

            if (entity is null) throw new ArgumentNullException(nameof(command));
            if (command.Name is null) throw new NotFoundException(nameof(command.Name));

            entity.Color = command.Color ?? "ffffff";
            entity.Name = command.Name;

            _tagRepository.Update(entity);

            await _unitOfWork.CommitTransactionAsync();

            _logger.Log(nameof(Tag).IsUpdated());
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task Delete(int id)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            await _tagRepository.Delete(id);

            await _unitOfWork.CommitTransactionAsync();

            _logger.Log(nameof(Tag).IsDeleted());
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}
