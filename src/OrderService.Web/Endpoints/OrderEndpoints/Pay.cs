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

  private readonly IPaymentService _paymentService;
  public Pay(IPaymentService paymentService) { 
    _paymentService = paymentService;
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
    var result = await _paymentService.GeneratePaymentUrl(request.OrderId);

    if (result.Errors.Any())
    {
      return BadRequest(result.Errors);
    }

    var response = new PayResponse(result.Value);

    return Ok(response);
  }
}
