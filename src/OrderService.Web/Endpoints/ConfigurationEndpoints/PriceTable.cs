using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.ConfigurationAggregates;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Endpoints.Records;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.ConfigurationEndpoints;

public class PriceTable : EndpointBaseAsync
  .WithoutRequest
  .WithActionResult<PriceTableResponse>
{

  public IRepository<PriceTableConfiguration> _priceTableRepository;

  public PriceTable(IRepository<PriceTableConfiguration> priceTableRepository)
  {
    _priceTableRepository = priceTableRepository;
  }

  [HttpGet(PriceTableRequest.Route)]
  [SwaggerOperation(
    Summary = "Get price tables",
    Description = "Get price tables",
    OperationId = "PriceTable",
    Tags = new[] { "PriceTAbleEndpoints" })
  ]
  public override async Task<ActionResult<PriceTableResponse>> HandleAsync(CancellationToken cancellationToken = default)
  {

    var priceTables = await _priceTableRepository.ListAsync();

    var priceTableRecords = priceTables.Select(PriceTableRecord.FromEntity);

    var response = new PriceTableResponse(priceTableRecords);

    return Ok(response);
  }
}
