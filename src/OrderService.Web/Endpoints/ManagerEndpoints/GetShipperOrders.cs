using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.OrderShippingAggregate;
using OrderService.Core.OrderShippingAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Endpoints.Records;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class GetShipperOrders : EndpointBaseAsync
  .WithRequest<GetShipperOrdersRequest>
  .WithActionResult<GetShipperOrdersResponse>

{

  private IRepository<OrderShipping> _orderShippingRepository;

  public GetShipperOrders(IRepository<OrderShipping> orderShippingRepository)
  {
    _orderShippingRepository = orderShippingRepository;
  }


  [HttpGet(GetShipperOrdersRequest.Route)]
  [SwaggerOperation(
    Summary = "Get orders of a shipper",
    Description = "Get orders of a shipper",
    OperationId = "Shipper.getOrders",
    Tags = new[] { "ManagerEndpoints" })
  ]
  [Authorize(Roles = "MANAGER")]
  public override async Task<ActionResult<GetShipperOrdersResponse>> HandleAsync([FromQuery] GetShipperOrdersRequest request, CancellationToken cancellationToken = default)
  {
    var spec = new OrderShippingPaginatedByShipperIdSpec(request.shipperId, request.skip, request.take);

    var totalCountSpec = new OrderShippingPaginatedByShipperIdSpec(request.shipperId, request.totalSkip, request.totalTake);

    var totalCount = await _orderShippingRepository.CountAsync(totalCountSpec);
    var orderShippings = await _orderShippingRepository.ListAsync(spec);

    var orderShippingRecords = orderShippings.Select(OrderShippingRecord.FromEntity);

    var response = new GetShipperOrdersResponse(totalCount, request.pageSize, orderShippingRecords);

    return Ok(response);
  }
}
