using AdvancedHealthChecks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks()
    .AddCheck<CustomHealthCheck>("Custom",
        tags: new[] { "custom" },
        timeout: TimeSpan.FromSeconds(5))
    .AddMongoDb(builder.Configuration["MongoDB:ConnectionString"],
        name: "MongoDB",
        tags: new[] { "mongodb" },
        timeout: TimeSpan.FromSeconds(5))
    .AddRedis(builder.Configuration["Redis:ConnectionString"],
        name: "Redis",
        tags: new[] { "redis" },
        timeout: TimeSpan.FromSeconds(5));

builder.Services.AddHealthChecksUI()
    .AddInMemoryStorage();

var app = builder.Build();

app.MapHealthChecks("/health");

app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = hc => hc.Tags.Contains("ready")
});

app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = hc => hc.Tags.Contains("live")
});

app.MapHealthChecks("/health/details", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapHealthChecksUI();

app.Run();