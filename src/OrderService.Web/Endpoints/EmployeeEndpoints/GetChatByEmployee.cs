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

public class GetChatByEmployee : EndpointBaseAsync
  .WithRequest<GetChatByEmployeeRequest>
  .WithActionResult<GetChatByEmployeeResponse>

{

  private readonly ICurrentUserService _currentUserService;
  private readonly IRepository<Order> _orderRepository;

  public GetChatByEmployee(ICurrentUserService currentUserService, IRepository<Order> orderRepository)
  {
    _currentUserService = currentUserService;
    _orderRepository = orderRepository;
  }


  [HttpGet(GetChatByEmployeeRequest.Route)]
  [Authorize(Roles = "EMPLOYEE,MANAGER")]
  [SwaggerOperation(
    Summary = "Get order Chat",
    Description = "Get order Chat",
    OperationId = "Order.GetChat",
    Tags = new[] { "EmployeeEndpoints" })
  ]
  public override async Task<ActionResult<GetChatByEmployeeResponse>> HandleAsync([FromRoute] GetChatByEmployeeRequest request, CancellationToken cancellationToken = default)
  {
    int userId = _currentUserService.TryParseUserId();

    var spec = new OrderChatByIdAndEmployeeIdSpec(request.orderId, userId);
    var order = await _orderRepository.FirstOrDefaultAsync(spec);

    if (order == null)
    {
      return BadRequest(nameof(order) + " not exist");
    }

    var messages = order.chat.chatMessages.Select(ChatMessageRecord.FromEntity);
    var response = new GetChatResponse(messages);

    return Ok(response);
  }
}
