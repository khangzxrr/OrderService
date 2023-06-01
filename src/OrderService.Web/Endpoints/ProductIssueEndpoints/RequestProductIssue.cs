﻿
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.Core.ProductReturnAggregate;
using OrderService.Core.ProductReturnAggregate.specifications;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Endpoints.Records;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.ProductIssueEndpoints;

public class RequestProductIssue : EndpointBaseAsync
  .WithRequest<RequestProductIssueRequest>
  .WithActionResult<RequestProductIssueResponse>
{

  private readonly IRepository<Order> _orderRepository;
  private readonly IRepository<ProductIssue> _productIssueRepository;
  


  public RequestProductIssue(IRepository<ProductIssue> productIssueRepository, IRepository<Order> orderRepository)
  {
    _productIssueRepository = productIssueRepository;
    _orderRepository = orderRepository;

  }

  [HttpPost(RequestProductIssueRequest.Route)]
  [SwaggerOperation(
    Summary = "Request product return",
    Description = "Request product return",
    OperationId = "ProductReturn.Request",
    Tags = new[] { "ProductIssueEndpoints" })
  ]
  [Authorize(Roles = "CUSTOMER")]
  public override async Task<ActionResult<RequestProductIssueResponse>> HandleAsync([FromBody] RequestProductIssueRequest request, CancellationToken cancellationToken = default)
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

    var activeIssueSpec = new ProductIssueByProductIdSpec(orderDetail.product.Id);

    var productIssue = await _productIssueRepository.FirstOrDefaultAsync(activeIssueSpec);

    if (productIssue != null)
    {
      return BadRequest("already exist");
    }


    var newProductIssue = new ProductIssue();

    //important field
    newProductIssue.AssignCustomerInfo(order.user.email, order.contactPhonenumber, order.user.fullname);
    newProductIssue.AssignEmployee(order.chat.employee);
    newProductIssue.SetProduct(orderDetail.product);
    newProductIssue.SetIsWarranty(request.isWarranty);


    newProductIssue.SetSeries(request.series);
    newProductIssue.SetReturnReason(request.description);
    newProductIssue.SetMedias(request.medias);
    

    await _productIssueRepository.AddAsync(newProductIssue);
    await _productIssueRepository.SaveChangesAsync();

    var response = new RequestProductIssueResponse(ProductIssueRecord.FromEntity(newProductIssue));

       


    return Ok(response);
  }
}