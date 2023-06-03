using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.Core.ProductAggregate;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Endpoints.Records;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class MarkResellOrder : EndpointBaseAsync
  .WithRequest<MarkResellOrderRequest>
  .WithActionResult<MarkResellOrderResponse>
{

  private readonly IRepository<Order> _orderRepository;
  private readonly IMapper _mapper;

  public MarkResellOrder(IRepository<Order> orderRepository, IMapper mapper)
  {
    _orderRepository = orderRepository;
    _mapper = mapper;
  }

  [Authorize(Roles = "EMPLOYEE,MANAGER")]
  [HttpPost(MarkResellOrderRequest.Route)]
  [SwaggerOperation(
    Summary = "Mark resell order",
    Description = "Mark resell order",
    OperationId = "Order.MarkResellOrder",
    Tags = new[] { "EmployeeEndpoints" })
  ]
  public override async Task<ActionResult<MarkResellOrderResponse>> HandleAsync(MarkResellOrderRequest request, CancellationToken cancellationToken = default)
  {
    var spec = new OrderByIdSpec(request.orderId);
    var order = await _orderRepository.FirstOrDefaultAsync(spec);

    if (order == null)
    {
      return BadRequest("order not found");
    }

    order.SetStatus(OrderStatus.reselling);


    foreach (var orderDetail in order.orderDetails)
    {
      orderDetail.product.setProductResellingStatus(ProductStatus.selling);
    }

    await _orderRepository.SaveChangesAsync();

    var orderRecord = _mapper.Map<OrderRecord>(order);
    var response = new MarkResellOrderResponse(orderRecord);

    return Ok(response);
  }
}
