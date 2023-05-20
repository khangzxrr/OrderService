using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Endpoints.Records;
using OrderService.Web.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class GetOrdersByEmployeeId : EndpointBaseAsync
  .WithoutRequest
  .WithActionResult<GetOrdersByEmployeeIdResponse>
{

  private readonly IRepository<Order> _orderRepository;
  private readonly ICurrentUserService _currentUserService;
  private readonly IMapper _mapper;

  public GetOrdersByEmployeeId(IRepository<Order> orderRepository, ICurrentUserService currentUserService, IMapper mapper)
  {
    _orderRepository = orderRepository;
    _currentUserService = currentUserService;
    _mapper = mapper;
  }


  [HttpGet(GetOrdersByEmployeeIdRequest.Route)]
  [SwaggerOperation(
    Summary = "Get orders by Employee Id",
    Description = "Get orders by Employee Id",
    OperationId = "Order.GetAllByEmployeeId",
    Tags = new[] { "EmployeeEndpoints" })
  ]
  [Authorize(Roles = "EMPLOYEE,MANAGER")]
  public override async Task<ActionResult<GetOrdersByEmployeeIdResponse>> HandleAsync(CancellationToken cancellationToken = default)
  {
    var spec = new OrderByEmployeeIdSpec(_currentUserService.TryParseUserId());
    var orders = await _orderRepository.ListAsync(spec);

    if (orders == null)
    {
      return BadRequest("Orders should not be null");
    }

    var orderRecordLists = orders.Select(_mapper.Map<OrderRecord>);
    GetOrdersByEmployeeIdResponse response = new GetOrdersByEmployeeIdResponse(orderRecordLists);

    return Ok(response);
  }
}
