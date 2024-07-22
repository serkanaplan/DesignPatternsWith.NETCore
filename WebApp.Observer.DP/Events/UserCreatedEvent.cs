
using MediatR;
using WebApp.Observer.DP.Models;

namespace WebApp.Observer.DP.Events;

public class UserCreatedEvent : INotification
{
    public AppUser AppUser { get; set; }
}
