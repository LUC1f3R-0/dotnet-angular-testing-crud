using TestCrudApplication.Infrastructure.Connectivity;

namespace backend.HostedService;

public class StartupConnectionCheckService : IHostedService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<StartupConnectionCheckService> _logger;

    public StartupConnectionCheckService(IServiceScopeFactory scopeFactory,ILogger<StartupConnectionCheckService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();

        var databaseConnectivityChecker = scope.ServiceProvider.GetRequiredService<IDatabaseConnectivityChecker>();

        var isConnected = await databaseConnectivityChecker.CheckConnectionAsync(cancellationToken);

        if (isConnected)
        {
            _logger.LogInformation("Database connection: TRUE");
        }
        else
        {
            _logger.LogError("Database connection: FALSE");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}