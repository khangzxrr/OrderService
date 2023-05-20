using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Endpoints.Records;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class GetCustomerOrders : EndpointBaseAsync
  .WithRequest<GetCustomerOrdersRequest>
  .WithActionResult<GetCustomerOrdersResponse>
{

  private readonly IRepository<Order> _orderRepository;

  public GetCustomerOrders(IRepository<Order> orderRepository)
  {
    _orderRepository = orderRepository;
  }

  [HttpGet(GetCustomerOrdersRequest.Route)]
  [SwaggerOperation(
    Summary = "Get customer orders list",
    Description = "Get customer orders list",
    OperationId = "CustomerOrders.get",
    Tags = new[] { "ManagerEndpoints" })
  ]
  [Authorize(Roles = "MANAGER")]
  public override async Task<ActionResult<GetCustomerOrdersResponse>> HandleAsync([FromQuery] GetCustomerOrdersRequest request, CancellationToken cancellationToken = default)
  {
    var spec = new OrderPaginatedByUserIdSpec(request.skip, request.take, request.customerId);
    var totalSpec = new OrderPaginatedByUserIdSpec(request.totalSkip, request.totalTake, request.customerId);

    var totalCount = await _orderRepository.CountAsync(totalSpec);
    var orders = await _orderRepository.ListAsync(spec);

    var generalOrderRecords = orders.Select(GeneralOrderRecord.FromEntity);

    var response = new GetCustomerOrdersResponse(totalCount, request.pageSize, generalOrderRecords);

    return Ok(response);
  }
}
