using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.OrderEndpoints;

public class GetChat : EndpointBaseAsync
  .WithRequest<GetChatRequest>
  .WithActionResult<GetChatResponse>
{

  private readonly IRepository<Order> _orderRepository;
  private readonly ICurrentUserService _currentUserService;

  public GetChat(IRepository<Order> orderRepository, ICurrentUserService currentUserService)
  {
    _orderRepository = orderRepository;
    _currentUserService = currentUserService;
  }

  [HttpGet(GetChatRequest.Route)]
  [Authorize]
  [SwaggerOperation(
    Summary = "Get chat by order id",
    Description = "Get chat by id",
    OperationId = "Order.GetChatByOrderId",
    Tags = new[] { "OrderEndpoints" })
  ]
  public override async Task<ActionResult<GetChatResponse>> HandleAsync([FromRoute] GetChatRequest request, CancellationToken cancellationToken = default)
  {

    int userId = _currentUserService.TryParseUserId();

    var spec = new OrderChatByIdAndUserIdSpec(request.orderId, userId);
    var order = await _orderRepository.FirstOrDefaultAsync(spec);

    if (order == null) {
      return BadRequest(nameof(order) + " not exist");
    }

    var messages = order.chat.chatMessages.Select(ChatMessageRecord.FromEntity);
    var response = new GetChatResponse(messages);

    return Ok(response);
  }
}
