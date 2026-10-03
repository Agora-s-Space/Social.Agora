using Agora.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Agora.Infrastructure.Tests;

public sealed class ConnectionTests : IDisposable
{
    private readonly Lazy<AgoraDbContext> _context;

    public ConnectionTests()
    {
        _context = new Lazy<AgoraDbContext>(() => new AgoraDbContext(
            new DbContextOptionsBuilder<AgoraDbContext>()
                .UseNpgsql(AgoraApiConfiguration.DatabaseOptions.Build(AgoraApiConfiguration.Profile))
                .Options));
    }

    public void Dispose()
    {
        if (_context.IsValueCreated)
        {
            _context.Value.Dispose();
        }
    }

    [Fact]
    [Trait("Categoria", "Integracao")]
    public async Task CanConnectAsync_ComBancoConfigurado_RetornaTrue()
    {
        var canConnect = await _context.Value.Database.CanConnectAsync();

        Assert.True(canConnect, $"Nao foi possivel conectar ao PostgreSQL ({AgoraApiConfiguration.Profile}).");
    }

    [Fact]
    [Trait("Categoria", "Integracao")]
    public async Task ExecuteSqlRawAsync_ComBancoConfigurado_ExecutaComSucesso()
    {
        var rowsAffected = await _context.Value.Database.ExecuteSqlRawAsync("SELECT 1");

        Assert.Equal(-1, rowsAffected);
    }

    [Fact]
    public void Build_ComSenhaVazia_LancaExplicito()
    {
        var semSenha = new AgoraDatabaseOptions
        {
            Host = AgoraApiConfiguration.DatabaseOptions.Host,
            Port = AgoraApiConfiguration.DatabaseOptions.Port,
            Database = AgoraApiConfiguration.DatabaseOptions.Database,
            Username = AgoraApiConfiguration.DatabaseOptions.Username,
            Password = string.Empty,
        };

        var excecao = Assert.Throws<InvalidOperationException>(
            () => semSenha.Build(AgoraApiConfiguration.Profile));

        Assert.Contains(
            $"{AgoraApiConfiguration.Profile}:Password",
            excecao.Message,
            StringComparison.Ordinal);
    }

    [Theory]
    [InlineData(AgoraDatabaseOptions.CloudSection)]
    [InlineData(AgoraDatabaseOptions.LocalSection)]
    public void Configuracao_ContemSecaoDoPerfil(string secao)
    {
        var opcoes = AgoraDatabaseOptions.FromConfiguration(AgoraApiConfiguration.Configuration);

        Assert.Equal(AgoraDatabaseOptions.CloudSection, AgoraApiConfiguration.Profile);
        Assert.Equal(AgoraDatabaseOptions.CloudSection, opcoes.Profile);
        Assert.NotNull(AgoraApiConfiguration.Configuration.GetSection(secao).Get<AgoraDatabaseOptions>());
        Assert.False(string.IsNullOrWhiteSpace(
            AgoraApiConfiguration.Configuration[$"{secao}:Host"]));
        Assert.False(string.IsNullOrWhiteSpace(
            AgoraApiConfiguration.Configuration[$"{secao}:Database"]));
    }
}