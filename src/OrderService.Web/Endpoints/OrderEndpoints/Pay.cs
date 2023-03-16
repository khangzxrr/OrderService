using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.Interfaces;
using OrderService.Core.OrderAggregate;
using OrderService.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.OrderEndpoints;

public class Pay : EndpointBaseAsync
  .WithRequest<PayRequest>
  .WithActionResult<PayResponse>
{

  private readonly IRepository<Order> _orderRepository;
  private readonly IPaymentService _paymentService;
  public Pay(IPaymentService paymentService, IRepository<Order> orderRepository) { 
    _paymentService = paymentService;
    _orderRepository = orderRepository;
  }

  [HttpGet(PayRequest.Route)]
  [SwaggerOperation(
    Summary = "Get pay url of a order by Id",
    Description = "Get pay url of a order by Id",
    OperationId = "Order.Pay",
    Tags = new[] { "OrderEndpoints" })
  ]
  public override async Task<ActionResult<PayResponse>> HandleAsync([FromRoute] PayRequest request, CancellationToken cancellationToken = default)
  {
    var order = await _orderRepository.GetByIdAsync(request.OrderId);
    if (order == null)
    {
      return NotFound("Order is not found");
    }

    string url = _paymentService.GeneratePaymentUrl(order);

    var response = new PayResponse(url);

    return Ok(response);
  }
}
