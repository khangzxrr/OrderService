using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.CurrencyAggregate;
using OrderService.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class GetCurrencies : EndpointBaseAsync
  .WithoutRequest
  .WithActionResult<GetCurrenciesResponse>
{

  private readonly IRepository<CurrencyExchange> _currencyExchangeRepository;

  public GetCurrencies(IRepository<CurrencyExchange> currencyExchangeRepository)
  {
    _currencyExchangeRepository = currencyExchangeRepository;
  }


  [HttpGet(GetCurrenciesRequest.Route)]
  [SwaggerOperation(
    Summary = "Get currency list",
    Description = "Get currency list",
    OperationId = "Currrencies.get",
    Tags = new[] { "ManagerEndpoints" })
  ]
  [Authorize(Roles = "MANAGER")]
  public override async Task<ActionResult<GetCurrenciesResponse>> HandleAsync(CancellationToken cancellationToken = default)
  {

    var currencies = await _currencyExchangeRepository.ListAsync();

    var currencyRecords = currencies.Select(CurrencyRecord.fromEntity);

    var response = new GetCurrenciesResponse(currencyRecords);

    return Ok(response);
  }
}
