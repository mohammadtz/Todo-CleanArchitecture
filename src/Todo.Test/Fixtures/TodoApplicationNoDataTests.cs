using Microsoft.EntityFrameworkCore;
using Todo.Application;
using Todo.Application.Contract.Todo;
using Todo.Application.Contract.Utils;
using Todo.Application.Utils;
using Todo.Infrastructure.EFCore;
using Todo.Infrastructure.EFCore.Repositories;
using Todo.Test.Seeds;

namespace Todo.Test.Fixtures;

public class TodoApplicationNoDataTests
{
    protected TodoDbContext? Context;
    protected ITodoApplication Application = null!;
    private ILogger logger;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<TodoDbContext>()
            .UseInMemoryDatabase("mr_lottery_db").Options;

        Context = new TodoDbContext(options);
        var todoRepository = new TodoRepository(Context);

        logger = new Logger();

        var tagRepository = new TagRepository(Context);
        Application = new TodoApplication(todoRepository, tagRepository, logger);
    }

    [Test]
    public async Task GetAll_WhenCalled_ReturnsListOfTasks()
    {
        var tasks = await Application.GetAll();

        Assert.That(tasks, Is.Empty);
    }

    [Test]
    public void GetById_WhenTodoNotFound_ThrowException()
    {
        Assert.That(async () => await Application.GetById(1), Throws.Exception);
    }

    [Test]
    public void Delete_WhenTodoNotFound_ThrowException()
    {
        Assert.That(async () => await Application.Delete(1), Throws.Exception);
    }

    [Test]
    public void ChangeStatus_WhenTodoNotFound_ThrowException()
    {
        Assert.That(async () => await Application.ChangeStatus(1), Throws.Exception);
    }

    [Test]
    public void Create_WhenTagNotFound_ThrowException()
    {
        Assert.That(async () => await Application.Create(new TodoCommand { Title = "test", TagId = 1 }), Throws.Exception);
    }

    [Test]
    public void Update_WhenTodoNotFound_ThrowException()
    {
        Assert.That(async () => await Application.Update(1, new TodoCommand { Title = "test", TagId = 1 }), Throws.Exception);
    }

    [Test]
    public void Update_WhenTagNotFound_ThrowException()
    {
        if (Context != null) SeedData.SeedTodo(Context);
        Assert.That(async () => await Application.Update(1, new TodoCommand { Title = "test", TagId = 0 }), Throws.Exception);
    }
}