using Ardalis.ApiEndpoints;
using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.ProductAggregate;
using OrderService.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.ProductCatalogEndpoints;

public class Create : EndpointBaseAsync
  .WithRequest<CreateProductCatalogRequest>
  .WithActionResult<CreateProductCatalogResponse>
{

  private readonly IRepository<ProductCategory> _repository;
  private readonly IMapper _mapper;

  public Create(IRepository<ProductCategory> repository, IMapper mapper)
  {
    _repository = repository;
    _mapper = mapper;
  }


  [HttpPost(CreateProductCatalogRequest.Route)]
  [SwaggerOperation(
    Summary = "Creates a new product category",
    Description = "Creates a new product category",
    OperationId = "Category.Create",
    Tags = new[] { "ProductEndpoints" })
  ]
  public override async Task<ActionResult<CreateProductCatalogResponse>> HandleAsync(CreateProductCatalogRequest request, CancellationToken cancellationToken = default)
  {
    if (request.CategoryName == null)
    {
      return BadRequest(nameof(request.CategoryName));
    }

    var category = new ProductCategory(request.CategoryName);
    await _repository.AddAsync(category);
    await _repository.SaveChangesAsync();


    var productCategoryRecord = _mapper.Map<ProductCategoryRecord>(category);
    var response = new CreateProductCatalogResponse(productCategoryRecord);
    return Ok(response);
  }
}
