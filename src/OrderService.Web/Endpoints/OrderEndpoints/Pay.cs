using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.Interfaces;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.OrderEndpoints;

public class Pay : EndpointBaseAsync
  .WithRequest<PayRequest>
  .WithActionResult<PayResponse>
{


  private readonly IRepository<Order> _orderRepository;
  private readonly ICurrentUserService _currentUserService;
  private readonly IPaymentService _paymentService;

  public Pay(IPaymentService paymentService, IRepository<Order> orderRepository, ICurrentUserService currentUserService) { 
    _paymentService = paymentService;
    _orderRepository = orderRepository;
    _currentUserService = currentUserService;
  }

  [HttpGet(PayRequest.Route)]
  [Authorize]
  [SwaggerOperation(
    Summary = "Get pay url of a order by Id",
    Description = "Get pay url of a order by Id",
    OperationId = "Order.Pay",
    Tags = new[] { "OrderEndpoints" })
  ]
  public override async Task<ActionResult<PayResponse>> HandleAsync([FromQuery] PayRequest request, CancellationToken cancellationToken = default)
  {
    var orderSpec = new OrderPaymentByIdAndUserIdSpec(request.OrderId, _currentUserService.TryParseUserId());
    var order = await _orderRepository.FirstOrDefaultAsync(orderSpec);

    if (order == null)
    {
      return BadRequest(nameof(order) + " is not exist");
    }

    var result = await _paymentService.GeneratePaymentUrl(request.OrderId, request.Hostname);

    if (result.Errors.Any())
    {
      return BadRequest(result.Errors);
    }

    var response = new PayResponse(result.Value);

    return Ok(response);
  }
}
