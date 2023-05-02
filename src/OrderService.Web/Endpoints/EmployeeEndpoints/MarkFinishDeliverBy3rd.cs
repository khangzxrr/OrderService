using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.Interfaces;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Events;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.Core.OrderShippingAggregate;
using OrderService.Core.OrderShippingAggregate.Events;
using OrderService.Core.OrderShippingAggregate.specifications;
using OrderService.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class MarkFinishDeliverBy3rd : EndpointBaseAsync
  .WithRequest<MarkFinishDeliverBy3rdRequest>
  .WithActionResult<MarkFinishDeliverBy3rdResponse>
{

  private readonly IRepository<Order> _orderRepository;
  private readonly IRepository<OrderShipping> _orderShippingRepository;

  private readonly IOrderPaymentService _orderPaymentService;

  private readonly IMediator _mediator;

  public MarkFinishDeliverBy3rd(IRepository<Order> orderRepository, IRepository<OrderShipping> orderShippingRepository, IOrderPaymentService orderPaymentService, IMediator mediator)
  {
    _orderRepository = orderRepository;
    _orderShippingRepository = orderShippingRepository;
    _orderPaymentService = orderPaymentService;

    _mediator = mediator;
  }

  [Authorize(Roles = "EMPLOYEE")]
  [HttpPost(MarkFinishDeliverBy3rdRequest.Route)]
  [SwaggerOperation(
    Summary = "Mark finished deliver by using 3rd deliver",
    Description = "Mark finished deliver by using 3rd deliver",
    OperationId = "Order.MarkFinish3rdDeliver",
    Tags = new[] { "EmployeeEndpoints" })
  ]
  public override async Task<ActionResult<MarkFinishDeliverBy3rdResponse>> HandleAsync(MarkFinishDeliverBy3rdRequest request, CancellationToken cancellationToken = default)
  {
    var orderSpec = new OrderByIdSpec(request.orderId);
    var order = await _orderRepository.FirstOrDefaultAsync(orderSpec);


    var orderShippingSpec = new OrderShippingByOrderId(request.orderId);
    var orderShipping = await _orderShippingRepository.FirstOrDefaultAsync(orderShippingSpec);

    if (order == null)
    {
      return BadRequest("order is not found");
    }

    if (orderShipping == null)
    {
      return BadRequest("order shipping is not found");
    } 


    if (order.localShippingStatus != OrderLocalShippingStatus.assignedTo3rdShipper)
    {
      return BadRequest("order local shipping is not correct state");
    }

    order.SetQueueInShipping(OrderLocalShippingStatus.delivered);
    order.SetStatus(OrderStatus.finished);
    orderShipping.setOrderShippingStatus(OrderShippingStatus.customerReceived);


    var paymentResult = await _orderPaymentService.AddNewPayment(
      request.orderId,
      PaymentStatus.SecondPayment.Name,
      OrderPayment.ConvertVNDToVNPayVND(order.remainCost),
      $"shipper_3rd_{DateTime.UtcNow}");

    if (paymentResult.Errors.Any())
    {
      return BadRequest(paymentResult.Errors);
    }

    await _orderRepository.SaveChangesAsync();
    await _orderShippingRepository.SaveChangesAsync();

    var orderShippingUpdatedStatusEvent = new OrderShippingUpdateStatusEvent(request.orderId, orderShipping.orderShippingStatus);
    await _mediator.Publish(orderShippingUpdatedStatusEvent);

    var response = new MarkFinishDeliverBy3rdResponse("OK");

    return Ok(response);
  }
}
