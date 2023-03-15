using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.Interfaces;
using OrderService.Core.ProductAggregate;
using OrderService.Core.ProductAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.ProductEndpoints;

public class Peek : EndpointBaseAsync
  .WithRequest<PeekProductRequest>
  .WithActionResult
{

  private readonly IConsumeProductResultHostedService _consumeProductResultHostedService;
  private readonly IRepository<Product> _productRepository;
  private readonly IMapper _mapper;

  public Peek(IConsumeProductResultHostedService consumeProductResultHostedService, IRepository<Product> productRepository, IMapper mapper)
  {
    _mapper = mapper;
    _consumeProductResultHostedService = consumeProductResultHostedService;
    _productRepository = productRepository;
  }

  [HttpGet(PeekProductRequest.Route)]
  [SwaggerOperation(
    Summary = "Peek if a product exist by url",
    Description = "Peek if a product exist y url",
    OperationId = "Product.Peek",
    Tags = new[] { "ProductEndpoints" })
  ]
  public override async Task<ActionResult> HandleAsync([FromQuery] PeekProductRequest request, CancellationToken cancellationToken = default)
  {
    if (request.ProductUrl == null)
    {
      return BadRequest(request.ProductUrl);
    }

    var productSpec = new ProductByUrlSpec(request.ProductUrl);
    var product = await _productRepository.FirstOrDefaultAsync(productSpec);

    if (product == null)
    {
      _consumeProductResultHostedService.SendToQueue(request.ProductUrl);
      return Ok("product is not exist, sent to queue, please hold on");
    }

    var productRecord = _mapper.Map<ProductRecord>(product);

    return Ok(productRecord);
  }
}
