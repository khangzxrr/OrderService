using Ardalis.ApiEndpoints;
using Ardalis.Specification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Endpoints.Records;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class GetOrders : EndpointBaseAsync
  .WithRequest<GetOrdersRequest>
  .WithActionResult<GetOrdersResponse>
{

  private readonly IRepository<Order> _orderRepository;

  public GetOrders(IRepository<Order> orderRepository) { 
    _orderRepository = orderRepository;
  }

  [HttpGet(GetOrdersRequest.Route)]
  [SwaggerOperation(
    Summary = "Get orders list",
    Description = "Get orders list",
    OperationId = "Orders.get",
    Tags = new[] { "ManagerEndpoints" })
  ]
  [Authorize(Roles = "MANAGER")]
  public override async Task<ActionResult<GetOrdersResponse>> HandleAsync([FromQuery] GetOrdersRequest request, CancellationToken cancellationToken = default)
  {

    OrderStatus orderStatus;
    var isStatusName = OrderStatus.TryFromName(request.statusName, true, out orderStatus);

    Specification<Order> spec;

    if (isStatusName)
    {
      spec = new GeneralOrderPaginatedByStatusAndDateSpec(request.pageIndex * request.pageSize, request.pageSize, request.startDate, request.endDate, orderStatus);
    } else
    {
      spec = new GeneralOrderPaginated(request.pageIndex * request.pageSize, request.pageSize, null, null);
    }


    var orders = await _orderRepository.ListAsync(spec);

    var orderRecords = orders.Select(GeneralOrderRecord.FromEntity);

    int pageCount = Utils.Utils.getPageCount(orders.Count, request.pageSize);

    var response = new GetOrdersResponse(pageCount, orderRecords);

    return Ok(response);
  }
}
