using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.OrderAggregate;
using OrderService.Core.ProductAggregate;
using OrderService.Core.ProductAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Endpoints.Records;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.ProductEndpoints;

public class GetResells : EndpointBaseAsync
  .WithRequest<GetResellsRequest>
  .WithActionResult<GetResellsResponse>
{

  private readonly IRepository<Order> _orderRepository;

  private readonly IRepository<Product> _productRepository;

  public GetResells(IRepository<Order> orderRepository, IRepository<Product> productRepository)
  {
    _orderRepository = orderRepository;
    _productRepository = productRepository;
  }


  [HttpGet(GetResellsRequest.Route)]
  [SwaggerOperation(
    Summary = "Get resell products",
    Description = "Get resell products",
    OperationId = "Product.GetResell",
    Tags = new[] { "ProductEndpoints" })
  ]
  public override async Task<ActionResult<GetResellsResponse>> HandleAsync([FromQuery] GetResellsRequest request, CancellationToken cancellationToken = default)
  {

    var countSpec = new ProductResellPaginatedSpec(request.totalTake, request.totalSkip, ProductStatus.selling);
    var totalCount = await _productRepository.CountAsync(countSpec);

    var listSpec = new ProductResellPaginatedSpec(request.take, request.skip, ProductStatus.selling);
    var products = await _productRepository.ListAsync(listSpec);

    var productRecords = products.Select(ProductRecord.FromEntity);

    var response = new GetResellsResponse(totalCount, request.pageSize, productRecords);

    return Ok(response);
  }
}
