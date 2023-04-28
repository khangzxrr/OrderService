using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.OrderShippingAggregate;
using OrderService.Core.ShipperAggregate;
using OrderService.Core.ShipperAggregate.specifications;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Interfaces;

namespace OrderService.Web.Endpoints.ShipperEndpoints;

public class GetAllOrder : EndpointBaseAsync
  .WithRequest<GetAllOrderRequest>
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
  [HttpGet]
  public override async Task<ActionResult<GetAllOrderResponse>> HandleAsync(GetAllOrderRequest request, CancellationToken cancellationToken = default)
  {
    var spec = new ShipperWithOrderByShipperIdSpec(_currentUserService.TryParseUserId());

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
