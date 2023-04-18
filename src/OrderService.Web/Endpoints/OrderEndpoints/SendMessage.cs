using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.OrderEndpoints;

public class SendMessage : EndpointBaseAsync
  .WithRequest<SendMessageRequest>
  .WithActionResult<SendMessageResponse>
{

  private readonly IRepository<Order> _orderRepository;
  private readonly ICurrentUserService _currentUserService;

  public SendMessage(IRepository<Order> orderRepository, ICurrentUserService currentUserService)
  {
    _orderRepository = orderRepository;
    _currentUserService = currentUserService;
  }

  [HttpPost(SendMessageRequest.Route)]
  [Authorize]
  [SwaggerOperation(
    Summary = "Send message to order",
    Description = "Send message to order",
    OperationId = "Order.sendMessage",
    Tags = new[] { "OrderEndpoints" })
  ]

  public override async Task<ActionResult<SendMessageResponse>> HandleAsync(SendMessageRequest request, CancellationToken cancellationToken = default)
  {
    var spec = new OrderChatByIdAndUserIdSpec(request.orderId, _currentUserService.TryParseUserId());
    var order = await _orderRepository.FirstOrDefaultAsync(spec);

    if (order == null)
    {
      return BadRequest("Order is not found");
    }

    string date = DateTime.Now.ToString("HH:ss dd/MM/yyyy");
    order.chat.AddNewCustomerChatMessage(request.message + $" at {date}");

    await _orderRepository.SaveChangesAsync();

    var response = new SendMessageResponse("Success");

    return Ok(response);
  }
}
