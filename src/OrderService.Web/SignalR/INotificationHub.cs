namespace OrderService.Web.SignalR;

public interface INotificationHub
{
  public Task SendPrivateMessage(int userId, string message);

}
