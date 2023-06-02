using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.Interfaces;
using OrderService.Core.ProductIssueAggregate;
using OrderService.Core.ProductIssueRefundConfiguration;
using OrderService.Core.ProductIssueRefundConfiguration.specifications;
using OrderService.Core.ProductReturnAggregate;
using OrderService.Core.ProductReturnAggregate.specifications;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Endpoints.Records;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class UpdateProductIssue : EndpointBaseAsync
  .WithRequest<UpdateProductIssueRequest>
  .WithActionResult<UpdateProductIssueResponse>
{

  private readonly IRepository<ProductIssue> _productIssueRepository;

  private readonly IRepository<ProductIssueRefundConfiguration> _refundConfigurationRepository;

  private readonly IGetMostFreeEmployeeService _getMostFreeEmployeeService;

  public UpdateProductIssue(IRepository<ProductIssue> productIssueRepository, IRepository<ProductIssueRefundConfiguration> refundConfiguration, IGetMostFreeEmployeeService getMostFreeEmployeeService)
  {
    _productIssueRepository = productIssueRepository;
    _refundConfigurationRepository = refundConfiguration;
    _getMostFreeEmployeeService = getMostFreeEmployeeService;
  }

  [Authorize(Roles = "EMPLOYEE,MANAGER")]
  [HttpPatch(UpdateProductIssueRequest.Route)]
  [SwaggerOperation(
    Summary = "Update product issues",
    Description = "Update product issues",
    OperationId = "Employee.UpdateProductIssues",
    Tags = new[] { "EmployeeEndpoints" })
  ]
  public override async Task<ActionResult<UpdateProductIssueResponse>> HandleAsync([FromBody] UpdateProductIssueRequest request, CancellationToken cancellationToken = default)
  {

    var spec = new ProductIssueByIdSpec(request.productIssueId);

    var productIssue = await _productIssueRepository.FirstOrDefaultAsync(spec);

    if (productIssue == null)
    {
      return BadRequest("product issue is not found");
    }
    ProductIssueStatus newProductIssueStatus;
    bool isSuccessParsed = ProductIssueStatus.TryFromName(request.status!, out newProductIssueStatus);

    if (!isSuccessParsed)
    {
      return BadRequest("failed to parse product issue status");
    }



    //prevent duplicate status
    if (newProductIssueStatus == productIssue.status)
    {
      return Ok(new UpdateProductIssueResponse(ProductIssueRecord.FromEntity(productIssue)));
    }



    if (newProductIssueStatus == ProductIssueStatus.refund)
    {
      var refundConfigSpec = new GetRefundByProductIssueStatus(productIssue.status);
      var refundConfig = await _refundConfigurationRepository.FirstOrDefaultAsync(refundConfigSpec);

      if (refundConfig == null)
      {
        return BadRequest("failed to parse refund rate");
      }

      float refundCost = refundConfig.refundRate * productIssue.totalOrderDetailPrice / 100.0f * productIssue.product.currencyExchange.rate;

      var refundPayment = new IssuePayment(refundCost, IssuePaymentStatus.refund, "refund", true);
      productIssue.AddIssuePayment(refundPayment);
    }
    else if (newProductIssueStatus == ProductIssueStatus.shippingToCustomer)
    {
      var mostFreeShipper = await _getMostFreeEmployeeService.GetMostFreeShipper();

      if (mostFreeShipper == null)
      {
        return BadRequest("fail to get most free shipper");
      }

      var productIssueShipping = new ProductIssueShipping();
      productIssueShipping.SetShipper(mostFreeShipper);


      productIssue.AssignShipping(productIssueShipping);
    }

    productIssue.SetStatus(newProductIssueStatus);

    await _productIssueRepository.SaveChangesAsync();

    var response = new UpdateProductIssueResponse(ProductIssueRecord.FromEntity(productIssue));

    return Ok(response);
  }

}
