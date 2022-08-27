using Framework.Infrastructure;

namespace Todo.Infrastructure.EFCore;

public class UnitOfWorkEf : IUnitOfWork
{
    private readonly TodoDbContext _dbContext;

    public UnitOfWorkEf(TodoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task BeginTransactionAsync()
    {
       await _dbContext.Database.BeginTransactionAsync();
    }

    public async  Task CommitTransactionAsync()
    {
       await _dbContext.SaveChangesAsync();
       await _dbContext.Database.CommitTransactionAsync();
    }

    public async  Task RollbackAsync()
    {
       await _dbContext.Database.RollbackTransactionAsync();
    }
}
