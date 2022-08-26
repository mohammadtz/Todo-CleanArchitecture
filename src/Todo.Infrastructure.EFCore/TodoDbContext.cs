using Microsoft.EntityFrameworkCore;
using Todo.Domain.Tag;
using Todo.Infrastructure.EFCore.EntityMapper;

namespace Todo.Infrastructure.EFCore;

public class TodoDbContext : DbContext
{
    public virtual DbSet<Domain.Todo.Todo> Tasks { get; set; } = null!;
    public virtual DbSet<Tag> Tags { get; set; } = null!;

    public TodoDbContext()
    {
    }

    public TodoDbContext(DbContextOptions<TodoDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TodoMap());
        modelBuilder.ApplyConfiguration(new TagMap());

        base.OnModelCreating(modelBuilder);
    }
}