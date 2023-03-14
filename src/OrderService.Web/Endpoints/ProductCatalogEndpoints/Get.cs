using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.ProductAggregate;
using OrderService.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.ProductCatalogEndpoints;

public class Get : EndpointBaseAsync
  .WithoutRequest
  .WithActionResult<GetProductCategoriesResponse>
{

  private readonly IRepository<ProductCategory> _repository;
  private readonly IMapper _mapper;

  public Get(IRepository<ProductCategory> repository, IMapper mapper)
  {
    _repository = repository;
    _mapper = mapper;
  }

  [HttpGet("/categories")]
  [SwaggerOperation(
    Summary = "Get all product categories",
    Description = "Get all product categories",
    OperationId = "Category.Get",
    Tags = new[] { "ProductEndpoints" })
  ]
  public override async Task<ActionResult<GetProductCategoriesResponse>> HandleAsync(CancellationToken cancellationToken = default)
  {

    var categories = await _repository.ListAsync();
    var categoryRecords = categories.Select(c => _mapper.Map<ProductCategoryRecord>(c));


    var response = new GetProductCategoriesResponse(categoryRecords);
    return Ok(response);
  }
}
