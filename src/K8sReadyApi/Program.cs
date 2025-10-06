
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Health Checks
builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy(), tags: new[] { "live" })
    .AddCheck("ready", () => HealthCheckResult.Healthy(), tags: new[] { "ready" });

var app = builder.Build();

// Swagger (enabled for all envs by default; restrict as you like)
app.UseSwagger();
app.UseSwaggerUI();

// Basic root endpoint
app.MapGet("/", () => new
{
    message = "OK",
    service = "K8sReadyApi",
    version = typeof(Program).Assembly.GetName().Version?.ToString() ?? "1.0.0",
    timestamp = DateTimeOffset.UtcNow
});

// Health endpoints
app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = r => r.Tags.Contains("live")
});
app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = r => r.Tags.Contains("ready")
});

app.Run();
