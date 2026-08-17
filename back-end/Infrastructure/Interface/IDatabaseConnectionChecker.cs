namespace TestCrudApplication.Infrastructure.Connectivity;

public interface IDatabaseConnectivityChecker
{
    Task<bool> CheckConnectionAsync(CancellationToken cancellationToken = default);
}