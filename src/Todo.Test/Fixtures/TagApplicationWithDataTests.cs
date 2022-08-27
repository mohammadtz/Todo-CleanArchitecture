using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Todo.Application;
using Todo.Application.Contract.Tag;
using Todo.Application.Contract.Utils;
using Todo.Application.Utils;
using Todo.Domain.Exceptions;
using Todo.Domain.Tag;
using Todo.Infrastructure.EFCore;
using Todo.Infrastructure.EFCore.Repositories;
using Todo.Test.Seeds;

namespace Todo.Test.Fixtures;

[Category("Tag")]
[TestFixture, Apartment(ApartmentState.STA)]
public class TagApplicationWithDataTests
{
    protected TodoDbContext? Context;
    protected ITagApplication Application = null!;
    private ILogger _logger = null!;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<TodoDbContext>()
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .UseInMemoryDatabase("mr_lottery_db").Options;

        Context = new TodoDbContext(options);
        _logger = new Logger();

        var tagRepository = new TagRepository(Context);
        var unitOfWork = new UnitOfWorkEf(Context);
        Application = new TagApplication(tagRepository, _logger, unitOfWork);

        SeedData.AddData(Context);
    }

    [Test]
    public async Task GetAll_WhenCalled_ReturnsListOfTasks()
    {
        var tasks = await Application.GetAll();

        Assert.That(tasks, Is.Not.Empty);
    }

    [Test]
    public async Task GetById_WhenCalled_ReturnsExpectedTag()
    {
        var result = await Application.GetById(1);

        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public async Task Delete_WhenTagExist_RemoveExpectedTag()
    {
        Assert.That(await Application.GetById(2), Is.Not.Null);

        await Application.Delete(2);

        Assert.That(async () => await Application.GetById(2), Throws.Exception);
    }

    [Test]
    public async Task Create_WhenDataIsValid_ShowSuccessLog()
    {
        await Application.Create(new TagCommand { Name = "tag test", Color = "hhhhhh" });

        Assert.That(_logger.LastError, Is.EqualTo(nameof(Tag).IsCreated()));
    }

    [Test]
    public void Create_WhenNameIsNull_ThrowNotFoundException()
    {
        Assert.That(async () => await Application.Create(new TagCommand { }), Throws.TypeOf<NotFoundException>());
    }

    [Test]
    public async Task Create_WhenColorIsNull_ShowSuccessLog()
    {
        await Application.Create(new TagCommand { Name = "tag test" });

        Assert.That(_logger.LastError, Is.EqualTo(nameof(Tag).IsCreated()));
    }

    [Test]
    public async Task Update_WhenDataIsValid_ShowSuccessLog()
    {
        await Application.Update(1, new TagCommand { Name = "tag test", Color = "hhhhhh" });

        Assert.That(_logger.LastError, Is.EqualTo(nameof(Tag).IsUpdated()));
    }

    [Test]
    public void Update_WhenNameIsNull_ThrowNotFoundException()
    {
        Assert.That(async () => await Application.Update(1, new TagCommand { }), Throws.TypeOf<NotFoundException>());
    }

    [Test]
    public async Task Update_WhenColorIsNull_ShowSuccessLog()
    {
        await Application.Update(1, new TagCommand { Name = "tag test" });

        Assert.That(_logger.LastError, Is.EqualTo(nameof(Tag).IsUpdated()));
    }

    [Test]
    public async Task Delete_WhenDataIsValid_ShowSuccessLog()
    {
        await Application.Delete(3);

        Assert.That(_logger.LastError, Is.EqualTo(nameof(Tag).IsDeleted()));
    }
}