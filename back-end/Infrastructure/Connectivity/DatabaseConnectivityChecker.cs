using backend.Data;
using TestCrudApplication.Infrastructure.Connectivity;

namespace TestCrudApplication.HostedServices;

public class DatabaseConnectivityChecker: IDatabaseConnectivityChecker
{
    private readonly AppDbContext _dbContext;

    public DatabaseConnectivityChecker(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> CheckConnectionAsync(
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await _dbContext.Database.CanConnectAsync(cancellationToken);
        }
        catch
        {
            return false;
        }
    }
}