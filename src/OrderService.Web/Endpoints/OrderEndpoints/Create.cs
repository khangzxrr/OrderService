using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.Interfaces;
using OrderService.Web.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.OrderEndpoints;

public class Create : EndpointBaseAsync
  .WithRequest<CreateOrderRequest>
  .WithActionResult
{

  private readonly ICreateOrderService _createOrderService;
  private readonly IAddOrderDetailService _addOrderDetailService;
  private readonly ICurrentUserService _currentUserService;

  public Create(ICreateOrderService createOrderService, IAddOrderDetailService addOrderDetailService, ICurrentUserService currentUserService)
  {
    _createOrderService = createOrderService;
    _addOrderDetailService = addOrderDetailService;
    _currentUserService = currentUserService;
  }

  [HttpPost(CreateOrderRequest.Route)]
  [Authorize]
  [SwaggerOperation(
    Summary = "Creates a new Order",
    Description = "Creates a new Order",
    OperationId = "Order.Create",
    Tags = new[] { "OrderEndpoints" })
  ]
  public override async Task<ActionResult> HandleAsync(CreateOrderRequest request, CancellationToken cancellationToken = default)
  {
    if (!request.products.Any())
    {
      return BadRequest("Must contain at least 1 product");
    }

    var userId = int.Parse(_currentUserService.UserId!);
    var orderResult = await _createOrderService.CreateNewOrder("description", "customer description", "delivery address", "0919092211");

    if (orderResult.ValidationErrors.Any())
    {
      return BadRequest(orderResult.ValidationErrors);
    }

    if (orderResult.Errors.Any())
    {
      return BadRequest(orderResult.Errors);
    }

    var order = orderResult.Value;

    if (order == null)
    {
      return StatusCode(500, "failed to create a new order");
    }

    foreach(var product in request.products)
    {
      var result = await _addOrderDetailService.AddOrderDetail(order, product.productUrl, product.productQuantity);

      if (result.Errors.Any())
      {
        return BadRequest(result.Errors);
      }
    }

    order = await _createOrderService.SaveNewOrder(userId, order);


    return Ok(order);
  }
}
