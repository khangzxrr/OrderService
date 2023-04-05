using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using OrderService.Web.Interfaces;

namespace OrderService.Web.SignalR;


[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class NotificationHub : Hub
{

  private readonly ICurrentUserService _currentUserService;

  private static Dictionary<int, string> connections = new Dictionary<int, string>();

  public NotificationHub(ICurrentUserService currentUserService)
  {
    _currentUserService = currentUserService;
  }

  public static async Task SendPrivateMessage(IHubClients clients, int userId,  string message)
  {
    if (!connections.ContainsKey(userId))
    {
      Console.WriteLine("User doesn't exist in signalR connection");
      return;
    }

    await clients.Client(connections[userId]).SendAsync("boardcast", message);
  }


  [Authorize]
  public override async Task OnConnectedAsync()
  {
    await base.OnConnectedAsync();

    //var userId = _currentUserService.TryParseUserId();
    var userId = int.Parse(Context.User!.Claims.Where(c => c.Type == "userId").First().Value);

    connections[userId] = Context.ConnectionId;

    Console.WriteLine("connected to signalR: " + userId);

  }
}
