using Agora.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace Agora.Infrastructure.Tests;

internal static class AgoraApiConfiguration
{
    public static string Profile { get; }

    public static AgoraDatabaseOptions DatabaseOptions { get; }

    public static IConfigurationRoot Configuration { get; }

    static AgoraApiConfiguration()
    {
        var apiProjectDirectory = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "..", "src", "Agora.Api"));

        var appsettingsPath = Path.Combine(apiProjectDirectory, "appsettings.json");

        if (!File.Exists(appsettingsPath))
        {
            throw new FileNotFoundException(
                $"appsettings.json da API nao encontrado em '{appsettingsPath}'.");
        }

        var configuration = new ConfigurationBuilder()
            .AddJsonFile(appsettingsPath)
            .AddUserSecrets(typeof(AgoraApiConfiguration).Assembly, optional: true)
            .Build();

        Configuration = (IConfigurationRoot)configuration;
        (Profile, DatabaseOptions) = AgoraDatabaseOptions.FromConfiguration(configuration);
    }
}