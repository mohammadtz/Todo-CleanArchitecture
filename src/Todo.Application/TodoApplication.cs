using Framework.Infrastructure;
using Todo.Application.Contract.Tag;
using Todo.Application.Contract.Todo;
using Todo.Application.Contract.Utils;
using Todo.Domain.Exceptions;
using Todo.Domain.Tag;
using Todo.Domain.Todo;

namespace Todo.Application;

public class TodoApplication : ITodoApplication
{
    private readonly ITodoRepository _todoRepository;
    private readonly ITagRepository _tagRepository;
    private readonly ILogger _logger;
    private readonly IUnitOfWork _unitOfWork;

    public TodoApplication(ITodoRepository todoRepository, ITagRepository tagRepository, ILogger logger, IUnitOfWork unitOfWork)
    {
        _todoRepository = todoRepository;
        _tagRepository = tagRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<TodoDto>> GetAll()
    {
        var result = await _todoRepository.GetListAsync();

        return result.ToList();
    }

    public async Task<TodoDto> GetById(int id)
    {
        var todo = await _todoRepository.GetAsync(id);

        if (todo == null) throw new NotFoundException(nameof(Domain.Todo.Todo));

        return new TodoDto(todo, todo?.Tag != null ? new TagDto(todo.Tag) : null);
    }

    public async Task ChangeStatus(int id)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var entity = await _todoRepository.GetAsync(id);

            if (entity == null) throw new NotFoundException(nameof(Domain.Todo.Todo));

            entity.ChangeStatus();
            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task Create(TodoCommand command)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            if (command.TagId != null && !_tagRepository.Exist(x => x.Id == command.TagId))
                throw new MessageException(nameof(command.TagId).InValid());
            if (command.Title is null) throw new NotFoundException(nameof(command.Title));

            var entity = new Domain.Todo.Todo(command.Title, command.TagId);
            await _todoRepository.Create(entity);

            await _unitOfWork.CommitTransactionAsync();

            _logger.Log(nameof(Todo).IsCreated());
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task Update(int id, TodoCommand command)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var entity = await _todoRepository.GetAsync(id);

            if (entity is null) throw new ArgumentNullException(nameof(Domain.Todo.Todo));

            if (command.TagId != null && !_tagRepository.Exist(x => x.Id == command.TagId))
                throw new ArgumentNullException(nameof(command.TagId));

            entity.Title = command.Title;
            entity.TagId = command.TagId;

            _todoRepository.Update(entity);

            await _unitOfWork.CommitTransactionAsync();

            _logger.Log(nameof(Todo).IsUpdated());
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

            await _todoRepository.Delete(id);

            await _unitOfWork.CommitTransactionAsync();

            _logger.Log(nameof(Todo).IsDeleted());
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}