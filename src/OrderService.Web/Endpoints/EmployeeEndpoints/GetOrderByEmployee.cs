using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Endpoints.Records;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class GetOrderByEmployee : EndpointBaseAsync
  .WithRequest<GetOrderByEmployeeRequest>
  .WithActionResult<GetOrderByEmployeeResponse>
{

  private readonly IRepository<Order> _orderRepository;

  private readonly IMapper _mapper;

  public GetOrderByEmployee(IRepository<Order> orderRepository, IMapper mapper)
  {
    _orderRepository = orderRepository;
    _mapper = mapper;
  }

  [Authorize(Roles = "EMPLOYEE,MANAGER")]
  [HttpGet(GetOrderByEmployeeRequest.Route)]
  [SwaggerOperation(
    Summary = "Get order by Employee Id",
    Description = "Get order by Employee Id",
    OperationId = "Order.GetByEmployeeId",
    Tags = new[] { "EmployeeEndpoints" })
  ]
  public override async Task<ActionResult<GetOrderByEmployeeResponse>> HandleAsync([FromRoute] GetOrderByEmployeeRequest request, CancellationToken cancellationToken = default)
  {
    var spec = new OrderByIdSpec(request.orderId);
    var order = await _orderRepository.FirstOrDefaultAsync(spec);

    if (order == null)
    {
      return BadRequest("Order not found");
    }

    var orderRecord = _mapper.Map<OrderRecord>(order);

    var response = new GetOrderByEmployeeResponse(orderRecord);

    return Ok(response);
  }
}
