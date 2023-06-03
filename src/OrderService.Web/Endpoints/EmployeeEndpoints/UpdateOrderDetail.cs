using Ardalis.ApiEndpoints;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Events;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Endpoints.Records;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class UpdateOrderDetail : EndpointBaseAsync
  .WithRequest<UpdateOrderDetailRequest>
  .WithActionResult<UpdateOrderDetailResponse>
{

  private readonly IRepository<Order> _orderRepository;
  private readonly IMapper _mapper;
  private readonly IMediator _mediator;


  public UpdateOrderDetail(IRepository<Order> orderRepository, IMapper mapper, IMediator mediator)
  {
    _orderRepository = orderRepository;
    _mapper = mapper;
    _mediator = mediator;
  }


  [HttpPatch(UpdateOrderDetailRequest.Route)]
  [SwaggerOperation(
    Summary = "Update order detail info",
    Description = "Update order detail info",
    OperationId = "Order.UpdateOrderDetail",
    Tags = new[] { "EmployeeEndpoints" })
  ]
  [Authorize(Roles = "EMPLOYEE,MANAGER")]
  public override async Task<ActionResult<UpdateOrderDetailResponse>> HandleAsync(UpdateOrderDetailRequest request, CancellationToken cancellationToken = default)
  {
    var orderSpec = new OrderByIdSpec(request.orderId);

    var order = await _orderRepository.FirstOrDefaultAsync(orderSpec);

    if (order == null)
    {
      return BadRequest("order not found");
    }


    var orderDetail = order.orderDetails.Where(od => od.Id == request.orderDetailId).FirstOrDefault();

    if (orderDetail == null)
    {
      return BadRequest("order detail is not found");
    }

    if (request.quantity.HasValue)
    {
      orderDetail.setQuantity(request.quantity.Value);
    }
    
    if (!request.productDescription.IsNullOrEmpty())
    {
      orderDetail.product.setProductDescription(request.productDescription!);
    }

    if (request.shipCost.HasValue)
    {
      orderDetail.product.setShipCost(request.shipCost.Value);
    }

    if (request.productCost.HasValue)
    {
      orderDetail.product.setPrice(request.productCost!.Value);
    }

    if (request.processCost.HasValue)
    {
      orderDetail.setProcessCost(request.processCost!.Value);
    }

    if (request.additionalCost.HasValue)
    {
      orderDetail.setAdditionalCost(request.additionalCost!.Value);
    }

    if (request.costPerWeight.HasValue)
    {
      orderDetail.product.setCostPerWeight(request.costPerWeight!.Value);
    }

    if (request.productWarrantable.HasValue)
    {
      orderDetail.product.setProductWarrantable(request.productWarrantable!.Value);
    }

    if (!request.warrantyDescription.IsNullOrEmpty())
    {
      orderDetail.product.setProductWarrantyDescription(request.warrantyDescription!);
    }

    if (request.productReturnable.HasValue)
    {
       orderDetail.product.setProductReturnable(request.productReturnable!.Value);
    }

    if (!request.returnDescription.IsNullOrEmpty())
    {
      orderDetail.product.setProductReturnDescription(request.returnDescription!);
    }

    if (request.disable.HasValue)
    {

      orderDetail.disableOrderDetail();

      int countOrderDetails = order.orderDetails.Count();

      if (countOrderDetails == 1)
      {
        order.SetStatus(OrderStatus.denied);
      }

    }


    await _orderRepository.SaveChangesAsync();

    var orderDetailUpdateEvent = new OrderDetailUpdateEvent(order);
    await _mediator.Publish(orderDetailUpdateEvent);

    var orderRecord = _mapper.Map<OrderRecord>(order);

    var response = new UpdateOrderDetailResponse(orderRecord);

    return Ok(response);
  }
}
