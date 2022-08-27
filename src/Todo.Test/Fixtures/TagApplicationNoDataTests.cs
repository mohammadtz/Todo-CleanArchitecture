using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Todo.Application;
using Todo.Application.Contract.Tag;
using Todo.Application.Contract.Utils;
using Todo.Application.Utils;
using Todo.Domain.Tag;
using Todo.Infrastructure.EFCore;
using Todo.Infrastructure.EFCore.Repositories;

namespace Todo.Test.Fixtures;

[Category("Tag")]
[TestFixture, Apartment(ApartmentState.MTA)]
public class TagApplicationNoDataTests
{
    protected TodoDbContext? Context;
    protected ITagApplication Application = null!;
    private ILogger logger;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<TodoDbContext>()
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .UseInMemoryDatabase("mr_lottery_db").Options;

        Context = new TodoDbContext(options);
        logger = new Logger();

        var tagRepository = new TagRepository(Context);
        var unitOfWork = new UnitOfWorkEf(Context);
        Application = new TagApplication(tagRepository, logger, unitOfWork);
    }

    [Test]
    public async Task GetAll_WhenCalled_ReturnsListOfTasks()
    {
        var tasks = await Application.GetAll();

        Assert.That(tasks, Is.Empty);
    }

    [Test]
    public void GetById_WhenTagNotFound_ThrowException()
    {
        Assert.That(async () => await Application.GetById(1), Throws.Exception);
    }

    [Test]
    public void Delete_WhenTagNotFound_ThrowException()
    {
        Assert.That(async () => await Application.Delete(1), Throws.Exception);
    }

    [Test]
    public void Create_WhenNameNotFound_ThrowException()
    {
        Assert.That(async () => await Application.Create(new TagCommand { }), Throws.Exception);
    }

    [Test]
    public void Update_WhenTagNotFound_ThrowException()
    {
        Assert.That(async () => await Application.Update(1, new TagCommand { }), Throws.Exception);
    }
}