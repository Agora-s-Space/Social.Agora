using Agora.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Agora.Infrastructure;

public static class AgoraPersistenceServiceCollectionExtensions
{
    public static IServiceCollection AddAgoraPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var (profile, options) = AgoraDatabaseOptions.FromConfiguration(configuration);

        var connectionString = options.Build(profile);

        services.AddSingleton(options);
        services.AddDbContext<AgoraDbContext>(dbOptions => dbOptions
            .UseNpgsql(connectionString, npgsql => npgsql.MigrationsAssembly(
                typeof(AgoraDbContext).Assembly.GetName().Name)));

        return services;
    }
}