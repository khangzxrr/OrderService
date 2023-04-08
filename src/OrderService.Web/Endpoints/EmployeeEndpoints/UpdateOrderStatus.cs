using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Events;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class UpdateOrderStatus : EndpointBaseAsync
  .WithRequest<UpdateOrderStatusRequest>
  .WithActionResult<UpdateOrderStatusResponse>
{

  private readonly IRepository<Order> _orderRepository;
  private readonly IMediator _mediator;

  public UpdateOrderStatus(IRepository<Order> orderRepository, IMediator mediator)
  {
    _orderRepository = orderRepository;
    _mediator = mediator;
  }

  [HttpPost(UpdateOrderStatusRequest.Route)]
  [SwaggerOperation(
    Summary = "Update order status by OrderId",
    Description = "Update order status by OrderId",
    OperationId = "Order.UpdateStatus",
    Tags = new[] { "EmployeeEndpoints" })
  ]
  public override async Task<ActionResult<UpdateOrderStatusResponse>> HandleAsync([FromBody] UpdateOrderStatusRequest request, CancellationToken cancellationToken = default)
  {
    OrderStatus orderStatus;
    try
    {
      orderStatus = OrderStatus.FromName(request.orderStatus);
    }
    catch(Exception)
    {
      return BadRequest("Order status is not valid");
    }

    var spec = new OrderByIdSpec(request.orderId);
    var order = await _orderRepository.FirstOrDefaultAsync(spec);

    if (order == null)
    {
      return BadRequest("Order not found");
    }

    order.SetStatus(orderStatus);

    await _orderRepository.SaveChangesAsync();

    var orderStatusUpdateEvent = new OrderStatusUpdatedEvent(request.orderId, orderStatus);
    await _mediator.Publish(orderStatusUpdateEvent);

    var response = new UpdateOrderStatusResponse("Update success");
    return Ok(response);
  }
}
