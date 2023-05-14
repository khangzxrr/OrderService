using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.OrderShippingAggregate;
using OrderService.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class GetTotalOrderShippings : EndpointBaseAsync
  .WithoutRequest
  .WithActionResult<GetTotalOrderShippingsResponse>
{

  private readonly IRepository<OrderShipping> _orderShippingRepository;

  public GetTotalOrderShippings(IRepository<OrderShipping> orderShippingRepository)
  {
    _orderShippingRepository = orderShippingRepository;
  }

  [HttpGet(GetTotalOrderShippingsRequest.Route)]
  [SwaggerOperation(
   Summary = "Get order shipping total",
   Description = "Get order shipping total",
   OperationId = "OrderShippings.total",
   Tags = new[] { "ManagerEndpoints" })
 ]
  [Authorize(Roles = "MANAGER")]
  public override async Task<ActionResult<GetTotalOrderShippingsResponse>> HandleAsync(CancellationToken cancellationToken = default)
  {

    var totals = await _orderShippingRepository.CountAsync();

    var response = new GetTotalOrderShippingsResponse(totals);

    return Ok(response);
  }
}
