using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.ProductReturnAggregate;
using OrderService.Core.ProductReturnAggregate.specifications;
using OrderService.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.ProductReturnEndpoints;

public class IsExistActiveProductReturn : EndpointBaseAsync
  .WithRequest<IsExistActiveProductReturnRequest>
  .WithActionResult<IsExistActiveProductReturnResponse>
{

  private readonly IRepository<ProductReturn> _productReturnRepository;

  public IsExistActiveProductReturn(IRepository<ProductReturn> productReturnRepository)
  {
    _productReturnRepository = productReturnRepository;
  }

  [HttpGet(IsExistActiveProductReturnRequest.Route)]
  [SwaggerOperation(
    Summary = "check exist product return by product id",
    Description = "check exist product return by product id",
    OperationId = "ProductReturn.CheckExist",
    Tags = new[] { "ProductReturnEndpoints" })
  ]
  public override async Task<ActionResult<IsExistActiveProductReturnResponse>> HandleAsync([FromQuery] IsExistActiveProductReturnRequest request, CancellationToken cancellationToken = default)
  {
    var spec = new ProductReturnByProductIdSpec(request.productId);

    var productReturn = await _productReturnRepository.FirstOrDefaultAsync(spec);

    var response = new IsExistActiveProductReturnResponse(productReturn != null, (productReturn != null) ? productReturn.Id : -1);

    return Ok(response);
  }
}
