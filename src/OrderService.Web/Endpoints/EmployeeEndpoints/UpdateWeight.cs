using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class UpdateWeight : EndpointBaseAsync
  .WithRequest<UpdateWeightRequest>
  .WithActionResult<UpdateWeightResponse>
{

  private readonly IRepository<Order> _orderRepository;

  public UpdateWeight(IRepository<Order> orderRepository)
  {
    _orderRepository = orderRepository;
  }

  [Authorize(Roles = "EMPLOYEE,MANAGER")]
  [HttpPost(UpdateWeightRequest.Route)]
  [SwaggerOperation(
    Summary = "Update weight of an order",
    Description = "Update weight of an order",
    OperationId = "Order.setOrderWeight",
    Tags = new[] { "EmployeeEndpoints" })
  ]
  public override Task<ActionResult<UpdateWeightResponse>> HandleAsync(UpdateWeightRequest request, CancellationToken cancellationToken = default)
  {
    //UPDATE EACH PRODUCT
    //OR ? UPDATE WEIGHT OF A ORDER?


    //var orderSpec = new OrderByIdSpec(request.orderId);

    //var order = await _orderRepository.FirstOrDefaultAsync(orderSpec);

    //if (order == null) {
    //  return BadRequest("Order is not found");
    //}
    throw new NotImplementedException();
   
  }
}
