using Agora.Infrastructure;
using Agora.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
builder.Services.AddAgoraPersistence(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();

app.MapGet("/health", async (AgoraDbContext db, CancellationToken cancellationToken) =>
{
    var canConnect = await db.Database.CanConnectAsync(cancellationToken);

    return canConnect
        ? Results.Ok(new HealthResponse("online", "ok"))
        : Results.Json(new HealthResponse("offline", "inacessivel"), statusCode: StatusCodes.Status503ServiceUnavailable);
});

app.Run();

internal record HealthResponse(string Status, string Database);