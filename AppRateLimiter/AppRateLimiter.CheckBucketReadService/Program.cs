using AppRateLimiter.DAL;
using AppRateLimiter.ReadService;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.AddScoped<IUnitofWork, UnitofWork>();
        services.AddScoped<ILogger<CheckBucketReadService>, Logger<CheckBucketReadService>>();
    })
    .Build();

host.Run();
