using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.ProductReturnAggregate;
using OrderService.Core.ProductReturnAggregate.specifications;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Endpoints.Records;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.ProductIssueEndpoints;

public class ProductIssueById : EndpointBaseAsync
  .WithRequest<ProductIssueByIdRequest>
  .WithActionResult<ProductIssueByIdResponse>
{

  private readonly IRepository<ProductIssue> _productReturnRepository;

  public ProductIssueById(IRepository<ProductIssue> productReturnRepository)
  {
    _productReturnRepository = productReturnRepository;
  }

  [HttpGet(ProductIssueByIdRequest.Route)]
  [SwaggerOperation(
    Summary = "get product return by id",
    Description = "get product return by id",
    OperationId = "ProductReturn.GetById",
    Tags = new[] { "ProductIssueEndpoints" })
  ]
  [Authorize(Roles = "CUSTOMER")]
  public override async Task<ActionResult<ProductIssueByIdResponse>> HandleAsync([FromQuery] ProductIssueByIdRequest request, CancellationToken cancellationToken = default)
  {

    var spec = new ProductIssueByIdSpec(request.id);

    var productReturn = await _productReturnRepository.FirstOrDefaultAsync(spec);
      
    if (productReturn == null)
    {
      return BadRequest("product return not found");
    }

    var response = new ProductIssueByIdResponse(ProductIssueRecord.FromEntity(productReturn));

    return Ok(response);
  }
}
