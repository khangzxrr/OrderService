
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.Dtos;
using OrderService.Core.Interfaces;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.Core.ProductReturnAggregate;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Endpoints.Records;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.ProductReturnEndpoints;

public class RequestProductReturn : EndpointBaseAsync
  .WithRequest<RequestProductReturnRequest>
  .WithActionResult<RequestProductReturnResponse>
{

  private readonly IRepository<Order> _orderRepository;
  private readonly IRepository<ProductReturn> _productReturnRepository;


  public RequestProductReturn(IRepository<ProductReturn> productReturnRepository, IRepository<Order> orderRepository)
  {
    _productReturnRepository = productReturnRepository;
    _orderRepository = orderRepository;

  }

  [HttpPost(RequestProductReturnRequest.Route)]
  [SwaggerOperation(
    Summary = "Request product return",
    Description = "Request product return",
    OperationId = "ProductReturn.Request",
    Tags = new[] { "ProductReturnEndpoints" })
  ]
  public override async Task<ActionResult<RequestProductReturnResponse>> HandleAsync([FromBody] RequestProductReturnRequest request, CancellationToken cancellationToken = default)
  {

    var orderSpec = new OrderByOrderDetailIdSpec(request.orderDetailId);

    var order = await _orderRepository.FirstOrDefaultAsync(orderSpec);

    if (order == null)
    {
      return BadRequest("order not found");
    }

    if (order.status != OrderStatus.finished)
    {
      return BadRequest("order is not in correct state");
    }

    var orderDetail = order.orderDetails.Where(od => od.Id == request.orderDetailId).FirstOrDefault();

    if (orderDetail == null)
    {
      return BadRequest("order detail is not found");
    }

    var productReturn = new ProductReturn();

    productReturn.SetIsWarranty(request.isWarranty);
    productReturn.SetSeries(request.series);
    productReturn.SetReturnReason(request.description);
    productReturn.SetMedias(request.medias);
    productReturn.SetProduct(orderDetail.product);

    await _productReturnRepository.AddAsync(productReturn);
    await _productReturnRepository.SaveChangesAsync();

    var response = new RequestProductReturnResponse(ProductReturnRecord.FromEntity(productReturn));

       


    return Ok(response);
  }
}
