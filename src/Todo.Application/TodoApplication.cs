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
    private  readonly ITagRepository _tagRepository;
    private readonly ILogger _logger;

    public TodoApplication(ITodoRepository todoRepository, ITagRepository tagRepository, ILogger logger)
    {
        _todoRepository = todoRepository;
        _tagRepository = tagRepository;
        _logger = logger;
    }

    public async Task<List<TodoDto>> GetAll()
    {
        var result = await _todoRepository.GetAllAsync();

        var response = result?.Select(x =>
            new TodoDto(x, x?.Tag != null ? new TagDto(x.Tag) : null)).ToList() ?? new List<TodoDto>();

        return response;
    }

    public async Task<TodoDto> GetById(int id)
    {
        var todo = await _todoRepository.GetAsync(id);

        if (todo == null) throw new NotFoundException(nameof(Domain.Todo.Todo));

        return new TodoDto(todo, todo?.Tag != null ? new TagDto(todo.Tag) : null);
    }

    public async Task ChangeStatus(int id)
    {
        var entity = await _todoRepository.GetAsync(id);

        if (entity == null) throw new NotFoundException(nameof(Domain.Todo.Todo));

        entity.ChangeStatus();

        await _todoRepository.SaveChangesAsync();
    }

    public async Task Create(TodoCommand command)
    {
        if (command.TagId != null && !await _tagRepository.IsValid(command.TagId ?? 0))
            throw new MessageException(nameof(command.TagId).InValid());
        if (command.Title is null) throw new NotFoundException(nameof(command.Title));

        var entity = new Domain.Todo.Todo(command.Title, command.TagId);
        await _todoRepository.Insert(entity);

        _logger.Log(nameof(Todo).IsCreated());
    }

    public async Task Update(int id, TodoCommand command)
    {
        var entity = await _todoRepository.GetAsync(id);

        if (entity is null) throw new ArgumentNullException(nameof(Domain.Todo.Todo));

        if (command.TagId != null && !await _tagRepository.IsValid(command.TagId ?? 0))
            throw new ArgumentNullException(nameof(command.TagId));

        entity.Title = command.Title;
        entity.TagId = command.TagId;

        await _todoRepository.Update(entity);

        _logger.Log(nameof(Todo).IsUpdated());
    }

    public async Task Delete(int id)
    {
        await _todoRepository.Delete(id);

        _logger.Log(nameof(Todo).IsDeleted());
    }
}