using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Agora.Infrastructure;

public sealed class AgoraDatabaseOptions
{
    /// <summary>Banco na nuvem (Neon).</summary>
    public const string CloudSection = "DatabaseCloud";

    /// <summary>Banco PostgreSQL local.</summary>
    public const string LocalSection = "DatabaseLocal";

    /// <summary>
    /// Chave que indica qual secao usar. Ausente, assume <see cref="CloudSection"/>.
    /// </summary>
    public const string ProfileKey = "DatabaseProfile";

    public string Host { get; set; } = string.Empty;
    public int Port { get; set; } = 5432;
    public string Database { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string SslMode { get; set; } = "Require";
    public int MaximumPoolSize { get; set; } = 10;

    /// <summary>
    /// Resolve as opcoes do perfil ativo a partir da configuracao.
    /// </summary>
    public static (string Profile, AgoraDatabaseOptions Options) FromConfiguration(
        IConfiguration configuration)
    {
        var profile = configuration[ProfileKey];

        if (string.IsNullOrWhiteSpace(profile))
        {
            profile = CloudSection;
        }

        var options = configuration.GetSection(profile).Get<AgoraDatabaseOptions>()
            ?? throw new InvalidOperationException(
                $"Secao '{profile}' ausente na configuracao.");

        return (profile, options);
    }

    public string Build(string profile)
    {
        if (string.IsNullOrWhiteSpace(Host))
        {
            throw new InvalidOperationException(
                $"'{profile}:Host' nao configurado.");
        }

        if (string.IsNullOrWhiteSpace(Database))
        {
            throw new InvalidOperationException(
                $"'{profile}:Database' nao configurado.");
        }

        if (string.IsNullOrWhiteSpace(Username))
        {
            throw new InvalidOperationException(
                $"'{profile}:Username' nao configurado.");
        }

        if (string.IsNullOrWhiteSpace(Password))
        {
            throw new InvalidOperationException(
                $"'{profile}:Password' nao configurado. " +
                $"Defina com: dotnet user-secrets set \"{profile}:Password\" \"<senha>\" --project src\\Agora.Api");
        }

        return new NpgsqlConnectionStringBuilder
        {
            Host = Host,
            Port = Port,
            Database = Database,
            Username = Username,
            Password = Password,
            SslMode = Enum.Parse<SslMode>(SslMode, ignoreCase: true),
            MaxPoolSize = MaximumPoolSize,
        }.ConnectionString;
    }
}