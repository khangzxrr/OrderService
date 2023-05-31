using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.ProductReturnAggregate;
using OrderService.Core.ProductReturnAggregate.specifications;
using OrderService.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.ProductIssueEndpoints;

public class IsExistActiveProductIssue : EndpointBaseAsync
  .WithRequest<IsExistActiveProductIssueRequest>
  .WithActionResult<IsExistActiveProductIssueResponse>
{

  private readonly IRepository<ProductIssue> _productReturnRepository;

  public IsExistActiveProductIssue(IRepository<ProductIssue> productReturnRepository)
  {
    _productReturnRepository = productReturnRepository;
  }

  [HttpGet(IsExistActiveProductIssueRequest.Route)]
  [SwaggerOperation(
    Summary = "check exist product return by product id",
    Description = "check exist product return by product id",
    OperationId = "ProductReturn.CheckExist",
    Tags = new[] { "ProductIssueEndpoints" })
  ]
  [Authorize(Roles = "CUSTOMER")]
  public override async Task<ActionResult<IsExistActiveProductIssueResponse>> HandleAsync([FromQuery] IsExistActiveProductIssueRequest request, CancellationToken cancellationToken = default)
  {
    var spec = new ProductIssueByProductIdSpec(request.productId);

    var productReturn = await _productReturnRepository.FirstOrDefaultAsync(spec);

    var response = new IsExistActiveProductIssueResponse(productReturn != null, (productReturn != null) ? productReturn.Id : -1);

    return Ok(response);
  }
}
