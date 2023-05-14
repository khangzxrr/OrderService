using System.Transactions;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.Interfaces;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.Core.OrderPaymentAggregate;
using OrderService.Core.OrderShippingAggregate;
using OrderService.Core.OrderShippingAggregate.Events;
using OrderService.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class MarkUserTakenOrderShipping : EndpointBaseAsync
  .WithRequest<MarkUserTakenOrderShippingRequest>
  .WithActionResult<MarkUserTakenOrderShippingResponse>
{

  private readonly IRepository<Order> _orderRepository;
  private readonly IRepository<OrderShipping> _orderShippingRepository;

  private readonly IOrderPaymentService _orderPaymentService;

  private readonly IMediator _mediator;

  public MarkUserTakenOrderShipping(IRepository<Order> orderRepository, IRepository<OrderShipping> orderShippingRepository, IOrderPaymentService orderPaymentService, IMediator mediator)
  {
    _orderRepository = orderRepository;
    _orderShippingRepository = orderShippingRepository;
    _orderPaymentService = orderPaymentService;
    _mediator = mediator;
  }

  [Authorize(Roles = "EMPLOYEE,MANAGER")]
  [HttpPost(MarkUserTakenOrderShippingRequest.Route)]
  [SwaggerOperation(
    Summary = "Mark finished deliver by customer taken at warehouse",
    Description = "Mark finished deliver by customer taken at warehouse",
    OperationId = "Order.MarkFinishCustomerTakenAtWarehouse",
    Tags = new[] { "EmployeeEndpoints" })
  ]
  public override async Task<ActionResult<MarkUserTakenOrderShippingResponse>> HandleAsync(MarkUserTakenOrderShippingRequest request, CancellationToken cancellationToken = default)
  {
    var orderSpec = new OrderByIdSpec(request.orderId);
    var order = await _orderRepository.FirstOrDefaultAsync(orderSpec);

    if (order == null)
    {
      return BadRequest("order is null");
    }

    order.SetQueueInShipping(OrderLocalShippingStatus.delivered);
    order.SetStatus(OrderStatus.finished);

    OrderShipping orderShipping = new OrderShipping(false, "khách đến nhận");
    orderShipping.setOrderShippingStatus(OrderShippingStatus.customerReceived);
    orderShipping.setOrder(order.Id);


    var paymentResult = await _orderPaymentService.AddNewPayment(
      request.orderId,
      PaymentStatus.SecondPayment.Name,
      OrderPayment.ConvertVNDToVNPayVND(order.remainCost),
      $"shipper_user_taken_warehouse");

    if (paymentResult.Errors.Any())
    {
      return BadRequest(paymentResult.Errors);
    }

    await _orderShippingRepository.AddAsync(orderShipping);

    await _orderRepository.SaveChangesAsync();
    await _orderShippingRepository.SaveChangesAsync();

    var orderShippingUpdatedStatusEvent = new OrderShippingUpdateStatusEvent(request.orderId, orderShipping.orderShippingStatus);
    await _mediator.Publish(orderShippingUpdatedStatusEvent);

    var response = new MarkUserTakenOrderShippingResponse("OK");

    return Ok(response);
  }
}
