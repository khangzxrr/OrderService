using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Endpoints.OrderEndpoints;
using OrderService.Web.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class SendMessageToOrder : EndpointBaseAsync
  .WithRequest<SendMessageToOrderRequest>
  .WithActionResult<SendMessageToOrderResponse>
{

  private readonly IRepository<Order> _orderRepository;
  private readonly ICurrentUserService _currentUserService;

  public SendMessageToOrder(IRepository<Order> orderRepository, ICurrentUserService currentUserService)
  {
    _orderRepository = orderRepository;
    _currentUserService = currentUserService;
  }

  [HttpPost(SendMessageToOrderRequest.Route)]
  [SwaggerOperation(
    Summary = "Send message to order",
    Description = "Send message to order",
    OperationId = "Order.SendMessage",
    Tags = new[] { "EmployeeEndpoints" })
  ]
  [Authorize(Roles = "EMPLOYEE,MANAGER")]
  public override async Task<ActionResult<SendMessageToOrderResponse>> HandleAsync([FromBody] SendMessageToOrderRequest request, CancellationToken cancellationToken = default)
  {

    var userId = _currentUserService.TryParseUserId();

    var spec = new OrderChatByIdAndEmployeeIdSpec(request.orderId, userId);
    var order = await _orderRepository.FirstOrDefaultAsync(spec);

    if (order == null) {
      return BadRequest("Order is not found");
    }

    order.chat.AddMessageFromEmployee(request.message);

    await _orderRepository.SaveChangesAsync();

    var messages = order.chat.chatMessages.Select(ChatMessageRecord.FromEntity);
    var response = new SendMessageToOrderResponse(messages);


    return Ok(response);
  }
}
