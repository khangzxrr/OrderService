using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.ProductAggregate;
using OrderService.Core.ProductAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.ProductEndpoints;

public class GetByUrl : EndpointBaseAsync
  .WithRequest<GetByUrlRequest>
  .WithActionResult<ProductRecord>
{

  private readonly IRepository<Product> _productRepository;
  private readonly IMapper _mapper;
  public GetByUrl(IRepository<Product> productRepository, IMapper mapper)
  {
    _productRepository = productRepository;
    _mapper = mapper;
  }


  [HttpGet(GetByUrlRequest.Route)]
  [SwaggerOperation(
    Summary = "Get a product by url",
    Description = "Get if a product by url",
    OperationId = "Product.Get",
    Tags = new[] { "ProductEndpoints" })
  ]
  public override async Task<ActionResult<ProductRecord>> HandleAsync([FromQuery] GetByUrlRequest request, CancellationToken cancellationToken = default)
  {
    if (request.Url == null) { 
      return BadRequest(request.Url);
    }

    var spec = new ProductByUrlSpec(request.Url);
    var product = await _productRepository.FirstOrDefaultAsync(spec);

    if (product == null)
    {
      return NotFound("please hold on, if you already peek a product then waiting a little more...server is fetching product");
    }

    var productRecord = _mapper.Map<ProductRecord>(product);
    return Ok(productRecord);
  }
}
