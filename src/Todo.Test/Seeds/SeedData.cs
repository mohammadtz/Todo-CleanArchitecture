using Todo.Domain.Tag;
using Todo.Infrastructure.EFCore;

namespace Todo.Test.Seeds;

public static class SeedData
{
    public static List<Tag> Tags =>
        new List<Tag>
        {
            new Tag("Tag Test 1", "0ba552"),
            new Tag("Tag Test 2", "793925"),
            new Tag("Tag Test 3", "738bf2"),
            new Tag("Tag Test 4", "d645d8"),
        };

    public static List<Domain.Todo.Todo> Tasks =>
        new List<Domain.Todo.Todo>
        {
            new Domain.Todo.Todo("My Todo Test 1"),
            new Domain.Todo.Todo("My Todo Test 2"),
            new Domain.Todo.Todo("My Todo Test 3"),
            new Domain.Todo.Todo("My Todo Test 4"),
        };

    public static void SeedTags(TodoDbContext? context)
    {
        if (context != null && context.Tags.Any()) return;

        context?.Tags.AddRange(Tags);
        context?.SaveChanges();
    }

    public static void SeedTodo(TodoDbContext? context)
    {
        if (context != null && context.Tasks.Any()) return;

        var entities = Tasks;

        context?.Tasks.AddRange(entities);
        context?.SaveChanges();
    }

    public static void AddData(TodoDbContext? context)
    {
        SeedTags(context);
        SeedTodo(context);
    }

    public static void RemoveData(TodoDbContext? context)
    {
        context?.Tasks.RemoveRange(SeedData.Tasks);
        context?.Tags.RemoveRange(SeedData.Tags);
        context?.SaveChanges();
    }
}
