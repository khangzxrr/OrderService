using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using OrderService.Core.Interfaces;
using OrderService.Core.RabbitMqDto;
using OrderService.Web.Interfaces;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace OrderService.Web.SignalR;


public class NotificationHub : Hub
{

  private readonly IProduceProductRequestService _produceProductRequestService;

  private readonly IRedisClient _redisClient;

  public NotificationHub(IProduceProductRequestService produceProductRequestService, IRedisClient redisClient)
  {
    _produceProductRequestService = produceProductRequestService;
    _redisClient = redisClient;
  }

  public static async Task SendPrivateMessage(IHubClients clients, string connectionId,  string message)
  {
    await clients.Client(connectionId).SendAsync("fetched_new_product", message);
  }

  public async void AddProductUrlToFetchData(RabbitRequestProductData rabbitRequestProductData)
  {
    rabbitRequestProductData.connectionId = Context.ConnectionId;

    var rabitResponseProductData = await _redisClient.Db0.GetAsync<RabbitResponseProductData>($"product_{rabbitRequestProductData.productUrl}");

    if (rabitResponseProductData != null)
    {

      //replace connection to new connection
      rabitResponseProductData.connectionId = rabbitRequestProductData.connectionId;

      _produceProductRequestService.SendToResultQueue(rabitResponseProductData);

      return;
    }

     //send to fetching queue if product is not fetched yet
     _produceProductRequestService.SendToQueue(rabbitRequestProductData);
    
  }


  public override async Task OnConnectedAsync()
  {
    await base.OnConnectedAsync();

    Console.WriteLine("connected to signalR: " + Context.ConnectionId);

  }
}
