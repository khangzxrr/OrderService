using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.OrderShippingAggregate;
using OrderService.Core.ShipperAggregate;
using OrderService.Core.ShipperAggregate.specifications;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.ShipperEndpoints;

public class GetAllOrder : EndpointBaseAsync
  .WithoutRequest
  .WithActionResult<GetAllOrderResponse>
{

  private readonly IRepository<Shipper> _shipperRepository;
  private readonly ICurrentUserService _currentUserService;

  public GetAllOrder(IRepository<Shipper> shipperRepository, ICurrentUserService currentUserService) 
  {
    _shipperRepository = shipperRepository;
    _currentUserService = currentUserService;
  }


  [Authorize(Roles = "SHIPPER")]
  [HttpGet(GetAllOrderRequest.Route)]
  [SwaggerOperation(
    Summary = "Get all order shipping",
    Description = "Get all order shipping",
    OperationId = "Shipper.getallOrder",
    Tags = new[] { "ShipperEndpoints" })
  ]
  public override async Task<ActionResult<GetAllOrderResponse>> HandleAsync(CancellationToken cancellationToken = default)
  {
    var spec = new ShipperWithOrderByUserIdSpec(_currentUserService.TryParseUserId());

    var shipper = await _shipperRepository.FirstOrDefaultAsync(spec);

    if (shipper == null)
    {
      return BadRequest("shipper is not found");
    }

    var orders = shipper.OrderShippings.Select(ShipperOrderRecord.FromEntity);

    var response = new GetAllOrderResponse(orders);

    return Ok(response);
  }
}
