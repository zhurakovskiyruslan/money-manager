using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoneyManager.Application.Abstractions.Persistence;
using MoneyManager.Infrastructure.Persistence;
using MoneyManager.Infrastructure.Repositories;

namespace MoneyManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connectionString));
        services.AddScoped(typeof(IWriteRepository<>), typeof(EfWriteRepository<>));
        services.AddScoped(typeof(IReadRepository<>), typeof(EfReadRepository<>));
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<ITransferRepository, TransferRepository>();
        services.AddScoped<ICustomCategoryRepository, CustomCategoryRepository>();
        services.AddScoped<ISharedCategoryRepository, SharedCategoryRepository>();
        return services;
    }
}