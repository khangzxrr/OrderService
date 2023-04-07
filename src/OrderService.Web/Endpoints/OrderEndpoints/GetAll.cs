using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.OrderEndpoints;

public class GetAll : EndpointBaseAsync
  .WithoutRequest
  .WithActionResult<GetAllResponse>
{

  private readonly IRepository<Order> _orderRepository;
  private readonly ICurrentUserService _currentUserService;

  public GetAll(IRepository<Order> orderRepository, ICurrentUserService currentUserService)
  {
    _orderRepository = orderRepository;
    _currentUserService = currentUserService;
  }

  [HttpGet(GetAllRequest.Route)]
  [Authorize]
  [SwaggerOperation(
    Summary = "Get all orders",
    Description = "Get all orders",
    OperationId = "Order.GetAll",
    Tags = new[] { "OrderEndpoints" })
  ]
  public override async Task<ActionResult<GetAllResponse>> HandleAsync(CancellationToken cancellationToken = default)
  {
    int userId = _currentUserService.TryParseUserId();

    var orderSpec = new GeneralOrderByUserIdSpec(userId);
    var orders = await _orderRepository.ListAsync(orderSpec);

    IEnumerable<GeneralOrderRecord> orderRecords = orders.Select(o => GeneralOrderRecord.FromEntity(o));

    var response = new GetAllResponse(orderRecords);

    return Ok(response);
  }
}
