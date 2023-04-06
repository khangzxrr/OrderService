using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.Interfaces;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.Core.OrderShippingAggregate;
using OrderService.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class CreateOrderShipping : EndpointBaseAsync
  .WithRequest<CreateOrderShippingRequest>
  .WithActionResult<CreateOrderShippingResponse>
{

  private readonly IRepository<Order> _orderRepository;
  private readonly IRepository<OrderShipping> _orderShippingRepository;

  private readonly IGetMostFreeEmployeeService _getMostFreeEmployeeService;


  public CreateOrderShipping(IRepository<Order> orderRepository, IRepository<OrderShipping> orderShippingRepository, IGetMostFreeEmployeeService getMostFreeEmployeeService)
  {
    _orderRepository = orderRepository;
    _orderShippingRepository = orderShippingRepository;
    _getMostFreeEmployeeService = getMostFreeEmployeeService;
  }
  

  [HttpPost(CreateOrderShippingRequest.Route)]
  [Authorize(Roles = "EMPLOYEE")]
  [SwaggerOperation(
    Summary = "Create order shipping",
    Description = "Create order shipping",
    OperationId = "OrderShipping.Create",
    Tags = new[] { "EmployeeEndpoints" })
  ]
  public override async Task<ActionResult<CreateOrderShippingResponse>> HandleAsync(CreateOrderShippingRequest request, CancellationToken cancellationToken = default)
  {
    var orderSpec = new OrderByIdSpec(request.orderId);
    var order = await _orderRepository.FirstOrDefaultAsync(orderSpec);

    if (order == null)
    {
      return BadRequest("Order not found");
    }

    if (order.status != OrderStatus.inVNwarehouse)
    {
      return BadRequest("order is not in correct state to create order shipping");
    }

    var shipper = await _getMostFreeEmployeeService.GetMostFreeShipper();

    if (!request.isUsing3rd && shipper == null)
    {
      return BadRequest("No shipper is free");
    }

    OrderShipping orderShipping = new OrderShipping(request.isUsing3rd, request.shippingDescription!);
    if (!request.isUsing3rd)
    {
      orderShipping.setOrder(order.Id);
      orderShipping.setShipper(shipper!);
    }

    await _orderShippingRepository.AddAsync(orderShipping);
    await _orderShippingRepository.SaveChangesAsync();

    var response = new CreateOrderShippingResponse(OrderShippingRecord.FromEntity(orderShipping));

    return Ok(response);
  }

}
