using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.Interfaces;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Events;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.Core.OrderShippingAggregate;
using OrderService.Core.OrderShippingAggregate.specifications;
using OrderService.Core.ProductAggregate;
using OrderService.Core.ProductAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class UpdateWeight : EndpointBaseAsync
  .WithRequest<UpdateWeightRequest>
  .WithActionResult<UpdateWeightResponse>
{

  private readonly IRepository<Order> _orderRepository;

  private readonly IRepository<Product> _productRepository;

  private readonly IAddOrderDetailService _addOrderDetailService;

  private readonly IRepository<OrderShipping> _orderShippingRepository;

  private readonly IMediator _mediator;

  public UpdateWeight(IRepository<Order> orderRepository, IRepository<Product> productRepository, IAddOrderDetailService addOrderDetailService, IRepository<OrderShipping> orderShippingRepository, IMediator mediator)
  {
    _orderRepository = orderRepository;
    _productRepository = productRepository;
    _orderShippingRepository = orderShippingRepository;
    _addOrderDetailService = addOrderDetailService;
    _mediator = mediator;
  }

  [Authorize(Roles = "EMPLOYEE,MANAGER")]
  [HttpPost(UpdateWeightRequest.Route)]
  [SwaggerOperation(
    Summary = "Update weight of an order",
    Description = "Update weight of an order",
    OperationId = "Order.setOrderWeight",
    Tags = new[] { "EmployeeEndpoints" })
  ]
  public override async Task<ActionResult<UpdateWeightResponse>> HandleAsync(UpdateWeightRequest request, CancellationToken cancellationToken = default)
  {


    var orderSpec = new OrderByIdSpec(request.orderId);
    var order = await _orderRepository.FirstOrDefaultAsync(orderSpec);

    var orderShippingSpec = new OrderShippingByOrderIdSpec(request.orderId);

    var orderShipping = await _orderShippingRepository.FirstOrDefaultAsync(orderShippingSpec);

    if (orderShipping != null)
    {
      return BadRequest("cannot update weight when in shipper sending state");
    }

    if (order == null)
    {
      return BadRequest("order is null");
    }

    if (order.status != OrderStatus.inVNwarehouse)
    {
      return BadRequest("not in correct state to update weight");
    }

    var productSpec = new ProductByIdSpec(request.productId);
    var product = await _productRepository.FirstOrDefaultAsync(productSpec);

    if (product == null)
    {
      return BadRequest("product is null");
    }

    product.setProductWeight(request.weight);

    await _productRepository.SaveChangesAsync();

    _addOrderDetailService.UpdateOrderDetails(order);

    await _orderRepository.SaveChangesAsync();

    var response = new UpdateWeightResponse(request.weight);

    var updatedWeightEvent = new OrderDetailUpdatedWeightEvent(order, product);
    await _mediator.Publish(updatedWeightEvent);

    return Ok(response);
  }
}
