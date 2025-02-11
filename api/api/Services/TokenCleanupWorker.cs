namespace api.Services;

public class TokenCleanupWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public TokenCleanupWorker(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await CleanExpiredTokens(stoppingToken);
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }

    private async Task CleanExpiredTokens(CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var authService = scope.ServiceProvider.GetRequiredService<AuthService>();
            await authService.RemoveExpiredTokensAsync(cancellationToken);
        }   
    }
}
