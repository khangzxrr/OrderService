using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.ProductReturnAggregate;
using OrderService.Core.ProductReturnAggregate.specifications;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Endpoints.Records;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.ProductReturnEndpoints;

public class ProductReturnById : EndpointBaseAsync
  .WithRequest<ProductReturnByIdRequest>
  .WithActionResult<ProductReturnByIdResponse>
{

  private readonly IRepository<ProductReturn> _productReturnRepository;

  public ProductReturnById(IRepository<ProductReturn> productReturnRepository)
  {
    _productReturnRepository = productReturnRepository;
  }

  [HttpGet(ProductReturnByIdRequest.Route)]
  [SwaggerOperation(
    Summary = "get product return by id",
    Description = "get product return by id",
    OperationId = "ProductReturn.GetById",
    Tags = new[] { "ProductReturnEndpoints" })
  ]
  [Authorize(Roles = "CUSTOMER")]
  public override async Task<ActionResult<ProductReturnByIdResponse>> HandleAsync([FromQuery] ProductReturnByIdRequest request, CancellationToken cancellationToken = default)
  {

    var spec = new ProductReturnByIdSpec(request.id);

    var productReturn = await _productReturnRepository.FirstOrDefaultAsync(spec);
      
    if (productReturn == null)
    {
      return BadRequest("product return not found");
    }

    var response = new ProductReturnByIdResponse(ProductReturnRecord.FromEntity(productReturn));

    return Ok(response);
  }
}
