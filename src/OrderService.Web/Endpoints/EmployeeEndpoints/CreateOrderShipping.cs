using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.Interfaces;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.Core.OrderShippingAggregate;
using OrderService.Core.OrderShippingAggregate.Events;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Endpoints.Records;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class CreateOrderShipping : EndpointBaseAsync
  .WithRequest<CreateOrderShippingRequest>
  .WithActionResult<CreateOrderShippingResponse>
{

  private readonly IRepository<Order> _orderRepository;
  private readonly IRepository<OrderShipping> _orderShippingRepository;

  private readonly IGetMostFreeEmployeeService _getMostFreeEmployeeService;

  private readonly IMediator _mediator;


  public CreateOrderShipping(IRepository<Order> orderRepository, IRepository<OrderShipping> orderShippingRepository, IGetMostFreeEmployeeService getMostFreeEmployeeService, IMediator mediator)
  {
    _orderRepository = orderRepository;
    _orderShippingRepository = orderShippingRepository;
    _getMostFreeEmployeeService = getMostFreeEmployeeService;
    _mediator = mediator;
  }
  

  [HttpPost(CreateOrderShippingRequest.Route)]
  [Authorize(Roles = "EMPLOYEE,MANAGER")]
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
      return BadRequest("order not found");
    }

    if (order.status != OrderStatus.inVNwarehouse)
    {
      return BadRequest("order is not in correct state to create order shipping");
    }

    if (order.localShippingStatus != OrderLocalShippingStatus.notInQueue)
    {
      return BadRequest("order is already in shipping state");
    }

    var shipper = await _getMostFreeEmployeeService.GetMostFreeShipper();

    CreateOrderShippingResponse response;

    if (!request.isUsing3rd && shipper == null)
    {

      order.SetQueueInShipping(OrderLocalShippingStatus.inQueue);

      await _orderRepository.SaveChangesAsync();

      response = new CreateOrderShippingResponse(null, "PLACE_IN_QUEUE_WAITING_FOR_SHIPPER");

      return Ok(response);
    }

    order.SetQueueInShipping(OrderLocalShippingStatus.assignedShipper);

    OrderShipping orderShipping = new OrderShipping(request.isUsing3rd, request.shippingDescription!);

    orderShipping.setOrder(order.Id);


    if (request.isUsing3rd)
    {
      order.SetQueueInShipping(OrderLocalShippingStatus.assignedTo3rdShipper);
    } else
    {
      orderShipping.setShipper(shipper!);
    }

    await _orderRepository.SaveChangesAsync();

    await _orderShippingRepository.AddAsync(orderShipping);
    await _orderShippingRepository.SaveChangesAsync();

    var createOrderShippingEvent = new OrderShippingCreatedEvent(request.orderId, request.isUsing3rd);
    await _mediator.Publish(createOrderShippingEvent);

    response = new CreateOrderShippingResponse(OrderShippingRecord.FromEntity(orderShipping), "OK");

    return Ok(response);
  }

}
