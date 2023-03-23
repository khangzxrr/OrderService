using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.Core.UserAggregate;
using OrderService.Core.UserAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.OrderEndpoints;

public class GetById : EndpointBaseAsync
  .WithRequest<GetByIdRequest>
  .WithActionResult<GetByIdResponse>
{

  private readonly IRepository<Order> _orderRepository;
  private readonly IRepository<User> _userRepository;
  private readonly ICurrentUserService _currentUserService;

  private readonly IMapper _mapper;



  public GetById(IRepository<Order> orderRepository, ICurrentUserService currentUserService, IRepository<User> userRepository, IMapper mapper)
  {
    _orderRepository = orderRepository;
    _currentUserService = currentUserService;
    _userRepository = userRepository;
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

    var userSpec = new UserByIdSpec(userId);
    var user = await _userRepository.FirstOrDefaultAsync(userSpec);

    if (user == null)
    {
      return BadRequest("User not found");
    }

    var order = user.orders.Where(o => o.Id == request.OrderId).FirstOrDefault();
    if (order == null)
    {
      return BadRequest("This order is not yours");
    }

    var orderSpec = new OrderByIdSpec(request.OrderId);
    order = await _orderRepository.FirstOrDefaultAsync(orderSpec);

    if (order == null)
    {
      return BadRequest("Order is not found");
    }

    var orderRecord = _mapper.Map<OrderRecord>(order);

    var response = new GetByIdResponse(orderRecord);

    return Ok(response);
  }
}
