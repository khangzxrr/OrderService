using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.Core.UserAggregate;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Endpoints.Records;
using OrderService.Web.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.OrderEndpoints;

public class GetById : EndpointBaseAsync
  .WithRequest<GetByIdRequest>
  .WithActionResult<GetByIdResponse>
{

  private readonly IRepository<Order> _orderRepository;
  private readonly ICurrentUserService _currentUserService;

  private readonly IMapper _mapper;

  public GetById(IRepository<Order> orderRepository, ICurrentUserService currentUserService, IMapper mapper)
  {
    _orderRepository = orderRepository;
    _currentUserService = currentUserService;
    _mapper = mapper;
  }


  [HttpGet(GetByIdRequest.Route)]
  [Authorize]
  [SwaggerOperation(
    Summary = "Get order by id",
    Description = "Get order by id",
    OperationId = "Order.GetById",
    Tags = new[] { "OrderEndpoints" })
  ]
  public override async Task<ActionResult<GetByIdResponse>> HandleAsync([FromRoute] GetByIdRequest request, CancellationToken cancellationToken = default)
  {
    int userId = _currentUserService.TryParseUserId();

    var orderSpec = new OrderByIdSpec(request.OrderId);
    var order = await _orderRepository.FirstOrDefaultAsync(orderSpec);

    if (order == null)
    {
      return BadRequest("Order is not found");
    }

    if (order.userId != userId)
    {
      return BadRequest("Order is not yours");
    }

    var orderRecord = _mapper.Map<OrderRecord>(order);

    var response = new GetByIdResponse(orderRecord);

    return Ok(response);
  }
}
