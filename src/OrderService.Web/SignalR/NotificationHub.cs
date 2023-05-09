using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using OrderService.Core.Interfaces;
using OrderService.Core.RabbitMqDto;
using OrderService.Web.Interfaces;

namespace OrderService.Web.SignalR;


public class NotificationHub : Hub
{

  private readonly IProduceProductRequestService _produceProductRequestService;

  public NotificationHub(IProduceProductRequestService produceProductRequestService)
  {
    _produceProductRequestService = produceProductRequestService;
  }

  public static async Task SendPrivateMessage(IHubClients clients, string connectionId,  string message)
  {
    await clients.Client(connectionId).SendAsync("fetched_new_product", message);
  }

  public void AddProductUrlToFetchData(RabbitRequestProductData rabbitRequestProductData)
  {
    rabbitRequestProductData.connectionId = Context.ConnectionId;

    _produceProductRequestService.SendToQueue(rabbitRequestProductData);
  }


  public override async Task OnConnectedAsync()
  {
    await base.OnConnectedAsync();

    Console.WriteLine("connected to signalR: " + Context.ConnectionId);

  }
}
