﻿using System.Collections.Generic;
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
    Summary = "Get a product by urls",
    Description = "Get if a product by urls",
    OperationId = "Product.Get",
    Tags = new[] { "ProductEndpoints" })
  ]
  public override async Task<ActionResult<ProductRecord>> HandleAsync([FromQuery] GetByUrlRequest request, CancellationToken cancellationToken = default)
  {
    if (request.Urls == null) { 
      return BadRequest(request.Urls);
    }


    List<Product> products = new List<Product>();

    foreach(string url in request.Urls)
    {
      var spec = new ProductByUrlSpec(url);
      var product = await _productRepository.FirstOrDefaultAsync(spec);

      if (product != null)
      {
        products.Add(product);
      }
    }

    if (products.Count < request.Urls.Length)
    {
      return NotFound($"server is fetching product {products.Count}/{request.Urls.Length}");
    }

    var productRecords = products.Select(p => _mapper.Map<ProductRecord>(p));
    var response = new GetByUrlResponse(productRecords);

    return Ok(response);
  }
}
