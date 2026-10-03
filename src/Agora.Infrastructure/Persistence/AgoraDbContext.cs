using Microsoft.EntityFrameworkCore;

namespace Agora.Infrastructure.Persistence;

public sealed class AgoraDbContext(DbContextOptions<AgoraDbContext> options) : DbContext(options)
{
}