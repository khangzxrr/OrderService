using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.ProductIssueAggregate.specifications;
using OrderService.Core.ProductReturnAggregate;
using OrderService.Core.ShipperAggregate;
using OrderService.Core.ShipperAggregate.specifications;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Endpoints.Records;
using OrderService.Web.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.ShipperEndpoints;

public class GetProductIssueShippings : EndpointBaseAsync
  .WithoutRequest
  .WithActionResult<GetProductIssueShippingsResponse>
{

  private readonly IRepository<ProductIssue> _productIssueRepository;
  private readonly IRepository<Shipper> _shipperRepository;
  private readonly ICurrentUserService _currentUserService;

  public GetProductIssueShippings(IRepository<ProductIssue> productIssueRepository, IRepository<Shipper> shipperRepository, ICurrentUserService currentUserService)
  {
    _productIssueRepository = productIssueRepository;
    _shipperRepository = shipperRepository;
    _currentUserService = currentUserService;
  }

  [Authorize(Roles = "SHIPPER")]
  [HttpGet(GetProductIssueShippingsRequest.Route)]
  [SwaggerOperation(
    Summary = "Get all product issue shippings",
    Description = "Get all product issue shippings",
    OperationId = "Shipper.getallProductIssueShippings",
    Tags = new[] { "ShipperEndpoints" })
  ]
  public override async Task<ActionResult<GetProductIssueShippingsResponse>> HandleAsync(CancellationToken cancellationToken = default)
  {

    var shipperSpec = new ShipperWithOrderByUserIdSpec(_currentUserService.TryParseUserId());

    var shipper = await _shipperRepository.FirstOrDefaultAsync(shipperSpec);

    if (shipper == null)
    {
      return BadRequest("shipper is not found");
    }

    var productIssuesSpec = new ProductIssueByShipperIdSpec(shipper.Id);
    var productIssues = await _productIssueRepository.ListAsync(productIssuesSpec);

    var productIssueShippingRecords = productIssues.Select(ProductIssueShippingRecord.FromEntity);

    var response = new GetProductIssueShippingsResponse(productIssueShippingRecords);

    return Ok(response);
  }
}
