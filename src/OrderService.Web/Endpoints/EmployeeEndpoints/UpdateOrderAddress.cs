using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minio.DataModel.Tags;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class UpdateOrderAddress : EndpointBaseAsync
  .WithRequest<UpdateOrderAddressRequest>
  .WithActionResult<UpdateOrderAddressResponse>
{

  private readonly IRepository<Order> _orderRepository;

  public UpdateOrderAddress(IRepository<Order> orderRepository)
  {
    _orderRepository = orderRepository;
  }

  [HttpPost(UpdateOrderAddressRequest.Route)]
  [SwaggerOperation(
    Summary = "Update order address by OrderId",
    Description = "Update order address by OrderId",
    OperationId = "Order.UpdateAddress",
    Tags = new[] { "EmployeeEndpoints" })
  ]
  [Authorize(Roles = "EMPLOYEE,MANAGER")]
  public override async Task<ActionResult<UpdateOrderAddressResponse>> HandleAsync([FromBody] UpdateOrderAddressRequest request, CancellationToken cancellationToken = default)
  {
    var spec = new OrderByIdSpec(request.orderId);
    var order = await _orderRepository.FirstOrDefaultAsync(spec);

    if (order == null)
    {
      return BadRequest("order is not found");
    }

    order.SetDeliveryAdress(request.address);

    await _orderRepository.SaveChangesAsync();

    var response = new UpdateOrderAddressResponse("update address success");

    return Ok(response);
  }
}
