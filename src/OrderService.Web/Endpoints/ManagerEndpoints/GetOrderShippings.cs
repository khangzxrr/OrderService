using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.OrderShippingAggregate;
using OrderService.Core.OrderShippingAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Endpoints.Records;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class GetOrderShippings : EndpointBaseAsync
  .WithRequest<GetOrderShippingsRequest>
  .WithActionResult<GetOrderShippingsResponse>
{

  private readonly IRepository<OrderShipping> _orderShippingRepository;

  public GetOrderShippings(IRepository<OrderShipping> orderShippingRepository)
  {
    _orderShippingRepository = orderShippingRepository;
  }

  [HttpGet(GetOrderShippingsRequest.Route)]
  [SwaggerOperation(
   Summary = "Get order shipping list",
   Description = "Get order shipping list",
   OperationId = "OrderShippings.get",
   Tags = new[] { "ManagerEndpoints" })
 ]
  [Authorize(Roles = "MANAGER")]
  public override async Task<ActionResult<GetOrderShippingsResponse>> HandleAsync([FromQuery] GetOrderShippingsRequest request, CancellationToken cancellationToken = default)
  {
    var spec = new OrderShippingPagingatedSpec(request.pageIndex * request.pageSize, request.pageSize);

    var totalOrderShippings = await _orderShippingRepository.CountAsync();
    var orderShippings = await _orderShippingRepository.ListAsync(spec);

    var orderShippingRecords = orderShippings.Select(OrderShippingRecord.FromEntity);

    var pageCount = Utils.Utils.getPageCount(totalOrderShippings, request.pageSize);

    var response = new GetOrderShippingsResponse(pageCount, orderShippingRecords);

    return Ok(response);
  }
}
