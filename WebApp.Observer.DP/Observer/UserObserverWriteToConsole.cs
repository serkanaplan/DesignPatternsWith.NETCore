using WebApp.Observer.DP.Models;

namespace WebApp.Observer.DP.Observer;

public class UserObserverWriteToConsole : IUserObserver
{
    private readonly IServiceProvider _serviceProvider;

    public UserObserverWriteToConsole(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    public void UserCreated(AppUser appUser)
    {
        var logger = _serviceProvider.GetRequiredService<ILogger<UserObserverWriteToConsole>>();

        logger.LogInformation($"user created : Id= {appUser.Id}");
    }
}
