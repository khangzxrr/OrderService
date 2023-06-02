using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minio.DataModel.Tags;
using OrderService.Core.ProductIssueAggregate.specifications;
using OrderService.Core.ProductReturnAggregate;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Endpoints.Records;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.ShipperEndpoints;

public class AcceptProductIssuePaymentAndFinish : EndpointBaseAsync
  .WithRequest<AcceptProductIssuePaymentAndFinishRequest>
  .WithActionResult<AcceptProductIssuePaymentAndFinishResponse>
{
  private readonly IRepository<ProductIssue> _productIssueRepository;

  public AcceptProductIssuePaymentAndFinish(IRepository<ProductIssue> productIssueRepository)
  {
    _productIssueRepository = productIssueRepository;
  }

  [Authorize(Roles = "SHIPPER")]
  [HttpPost(AcceptProductIssuePaymentAndFinishRequest.Route)]
  [SwaggerOperation(
    Summary = "Accept product issue payment and finish",
    Description = "Accept product issue payment and finish",
    OperationId = "Shipper.AcceptProductIssuePaymentAndFinish",
    Tags = new[] { "ShipperEndpoints" })
  ]
  public override async Task<ActionResult<AcceptProductIssuePaymentAndFinishResponse>> HandleAsync([FromBody] AcceptProductIssuePaymentAndFinishRequest request, CancellationToken cancellationToken = default)
  {
    var spec = new ProductIssueWithShippingByIdSpec(request.productIssueId);

    var productIssue = await _productIssueRepository.FirstOrDefaultAsync(spec);

    if (productIssue == null)
    {
      return BadRequest("product issue is not found");
    }


    productIssue.AcceptAllIssuePayments();

    productIssue.SetStatus(ProductIssueStatus.shipperReceived);
    productIssue.SetStatus(ProductIssueStatus.shippingToCustomer);
    productIssue.SetStatus(ProductIssueStatus.customerReceived);
    productIssue.SetStatus(ProductIssueStatus.finish);

    productIssue.productIssueShipping!.SetShippingStatus(Core.OrderShippingAggregate.OrderShippingStatus.customerReceived);

    await _productIssueRepository.SaveChangesAsync();

    var response = new AcceptProductIssuePaymentAndFinishResponse(ProductIssueShippingRecord.FromEntity(productIssue));

    return Ok(response);
  }
}
