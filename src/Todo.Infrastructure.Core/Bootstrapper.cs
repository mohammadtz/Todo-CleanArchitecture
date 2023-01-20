using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Todo.Application;
using Todo.Application.Contract.Tag;
using Todo.Application.Contract.Todo;
using Todo.Application.Utils;
using Todo.Domain.Tag;
using Todo.Domain.Todo;
using Todo.Infrastructure.EFCore;
using Todo.Infrastructure.EFCore.Repositories;
using ILogger = Todo.Application.Contract.Utils.ILogger;

namespace Todo.Infrastructure.Core;

public static class Bootstrapper
{
    public static void Config(this IServiceCollection services, string? connectionString)
    {
        // applications
        services.AddTransient<ITagApplication, TagApplication>();
        services.AddTransient<ITodoApplication, TodoApplication>();

        // repositories
        services.AddTransient<ITagRepository, TagRepository>();
        services.AddTransient<ITodoRepository, TodoRepository>();

        // unit of works
        services.AddTransient<IUnitOfWork, UnitOfWorkEf>();
        
        services.AddSingleton<ILogger, Logger>();

        services.AddDbContext<TodoDbContext>(options =>
        {
            if (connectionString != null) options.UseLazyLoadingProxies().UseSqlite(connectionString);
        });
    }
}
