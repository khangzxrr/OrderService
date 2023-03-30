using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using OrderService.Web.Interfaces;

namespace OrderService.Web.SignalR;

[Authorize]
public class NotificationHub : Hub, INotificationHub
{

  private readonly ICurrentUserService _currentUserService;

  private Dictionary<int, string> connections = new Dictionary<int, string>();

  public NotificationHub(ICurrentUserService currentUserService)
  {
    _currentUserService = currentUserService;
  }

  public async Task SendPrivateMessage(int userId,  string message)
  {
    if (!connections.ContainsKey(userId))
    {
      Console.WriteLine("User doesn't exist in signalR connection");
      return;
    }

    await Clients.Client(connections[userId]).SendAsync("boardcast", message);
  }


  public override async Task OnConnectedAsync()
  {
    await base.OnConnectedAsync();

    int userId = _currentUserService.TryParseUserId();

    connections.Remove(userId);
    connections.Add(userId, Context.ConnectionId);

    Console.WriteLine("connected to signalR: " + _currentUserService.UserId);
  }
}
