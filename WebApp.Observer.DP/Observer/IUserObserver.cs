using WebApp.Observer.DP.Models;

namespace WebApp.Observer.DP.Observer;
public interface IUserObserver
{
    void UserCreated(AppUser appUser);
}
