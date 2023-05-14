using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class GetTotalOrder : EndpointBaseAsync
  .WithoutRequest
  .WithActionResult<GetTotalOrderResponse>
{

  private readonly IRepository<Order> _orderRepository;

  public GetTotalOrder(IRepository<Order> orderRepository)
  {
    _orderRepository = orderRepository;
  }

  [HttpGet(GetTotalOrderRequest.Route)]
  [SwaggerOperation(
    Summary = "Get orders count",
    Description = "Get orders count",
    OperationId = "Orders.total",
    Tags = new[] { "ManagerEndpoints" })
  ]
  [Authorize(Roles = "MANAGER")]
  public override async Task<ActionResult<GetTotalOrderResponse>> HandleAsync(CancellationToken cancellationToken = default)
  {
    var spec = new GeneralOrderPaginated(0, 0, null, null);

    var orderCount = await _orderRepository.CountAsync(spec);

    var response = new GetTotalOrderResponse(orderCount);

    return Ok(response);
  }
}
