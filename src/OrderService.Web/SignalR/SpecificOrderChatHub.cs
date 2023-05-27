using System.Collections.Concurrent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Dtos;

namespace OrderService.Web.SignalR;

[Authorize]
public class SpecificOrderChatHub: Hub
{

  private static ConcurrentDictionary<string, string> _userInGroup = new ConcurrentDictionary<string, string>();

  private readonly IRepository<Order> _orderRepository;

  public SpecificOrderChatHub(IRepository<Order> orderRepository)
  {
    _orderRepository = orderRepository;
  }

  private async Task SendMessageToOrderByEmployee(int orderId, int userId, string message)
  {
    var spec = new OrderChatByIdAndEmployeeIdSpec(orderId, userId);

    var order = await _orderRepository.FirstOrDefaultAsync(spec);

    if (order == null)
    {
      throw new Exception("order is not found");
    }

    order.chat.AddMessageFromEmployee(message);
  }

  private async Task SendMessageToOrderByCustomer(int orderId, int userId, string message) {

    var spec = new OrderChatByIdAndUserIdSpec(orderId, userId);

    var order = await _orderRepository.FirstOrDefaultAsync(spec);

    if (order == null)
    {
      throw new Exception("order is not found");
    }

    order.chat.AddMessageFromCustomer(message);
  }  

  public async Task SendMessage(ChatMessageDto chatMessageDto)
  {
    var groupName = _userInGroup[Context.ConnectionId];

    if (groupName == null)
    {
      throw new Exception("groupName is not found");
    }

    var userId = int.Parse(Context.User!.Claims.Where(c => c.Type == "userId").First().Value);
    var roleName = Context.User!.Claims.Where(c => c.Type.Contains("claims/role")).First().Value;

    var orderId = int.Parse(groupName.Split("_").Last()); //order_1 order_333


    if (roleName == "EMPLOYEE") {
      await SendMessageToOrderByEmployee(orderId, userId, chatMessageDto.message);
    } 
    else if (roleName == "CUSTOMER")
    {
      await SendMessageToOrderByCustomer(orderId, userId, chatMessageDto.message);
    }

    //save changes
    await _orderRepository.SaveChangesAsync();

    await Clients.Group(groupName).SendAsync("boardcast");
  }

  public async Task ConnectToChatRoom(ConnectToChatRoomRequest connectToChatRoomRequest)
  {
    var groupName = $"order_{connectToChatRoomRequest.orderId}";

    await Groups.AddToGroupAsync(Context.ConnectionId, groupName);


    _userInGroup.TryAdd(Context.ConnectionId, groupName);

    Console.WriteLine($"{Context.ConnectionId}  {groupName}");
  }

  public override Task OnConnectedAsync()
  {

    return base.OnConnectedAsync();
  }

  public override Task OnDisconnectedAsync(Exception? exception)
  {
    _userInGroup.Remove(Context.ConnectionId, out _);

    return base.OnDisconnectedAsync(exception);
  }
}
