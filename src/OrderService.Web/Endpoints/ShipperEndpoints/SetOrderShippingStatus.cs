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

public class SetOrderShippingStatus : EndpointBaseAsync
  .WithRequest<SetOrderShippingStatusRequest>
  .WithActionResult<SetOrderShippingStatusResponse>
{

  private readonly ICurrentUserService _currentUserService;
  private readonly IRepository<Shipper> _shipperRepository;

  public SetOrderShippingStatus(ICurrentUserService currentUserService, IRepository<Shipper> shipperRepository)
  {
    _currentUserService = currentUserService;
    _shipperRepository = shipperRepository;
  }

  [Authorize(Roles = "SHIPPER")]
  [HttpPost(SetOrderShippingStatusRequest.Route)]
  [SwaggerOperation(
    Summary = "Set order shipping status",
    Description = "Set order shipping status",
    OperationId = "Shipper.setOrderShippingStatus",
    Tags = new[] { "ShipperEndpoints" })
  ]
  public override async Task<ActionResult<SetOrderShippingStatusResponse>> HandleAsync(SetOrderShippingStatusRequest request, CancellationToken cancellationToken = default)
  {
    var spec = new ShipperWithOrderByUserIdSpec(_currentUserService.TryParseUserId());
    var shipper = await _shipperRepository.FirstOrDefaultAsync(spec);

    OrderShippingStatus newOrderShippingStatus;
    var isSuccessParsedOrderShippingStatus = OrderShippingStatus.TryFromName(request.shippingStatus, true, out newOrderShippingStatus);

    if (isSuccessParsedOrderShippingStatus == false)
    {
      return BadRequest("OrderShippingStatus is not valid");
    }

    if (shipper == null)
    {
      return BadRequest("Shipper not found");
    }

    var orderShipping = shipper.OrderShippings.Where(o => o.Id == request.orderShippingId).FirstOrDefault();

    if (orderShipping == null)
    {
      return BadRequest("Order shipping is not found");
    }

    orderShipping.setOrderShippingStatus(newOrderShippingStatus);

    await _shipperRepository.SaveChangesAsync();

    var orderShippingRecord = ShipperOrderRecord.FromEntity(orderShipping);

    var response = new SetOrderShippingStatusResponse(orderShippingRecord!);

    return Ok(response);
  }
}
