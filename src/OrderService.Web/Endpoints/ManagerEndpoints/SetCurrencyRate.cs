using Ardalis.ApiEndpoints;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.CurrencyAggregate;
using OrderService.Core.CurrencyAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class SetCurrencyRate : EndpointBaseAsync
  .WithRequest<SetCurrencyRateRequest>
  .WithActionResult<SetCurrencyRateResponse>
{

  private readonly IRepository<CurrencyExchange> _currencyExchangeRepository;

  public SetCurrencyRate(IRepository<CurrencyExchange> currencyExchangeRepository)
  {
    _currencyExchangeRepository = currencyExchangeRepository;
  }

  [HttpPost(SetCurrencyRateRequest.Route)]
  [SwaggerOperation(
    Summary = "Set currency rate by id",
    Description = "Set currency rate by id",
    OperationId = "Currencies.setRate",
    Tags = new[] { "ManagerEndpoints" })
  ]
  [Authorize(Roles = "MANAGER")]
  public override async Task<ActionResult<SetCurrencyRateResponse>> HandleAsync(SetCurrencyRateRequest request, CancellationToken cancellationToken = default)
  {
    var spec = new CurrencyExchangeById(request.currencyId);

    var currency = await _currencyExchangeRepository.FirstOrDefaultAsync(spec);
    if (currency == null)
    {
      return BadRequest("currency is not found");
    }

    currency.setRate(request.rate);

    await _currencyExchangeRepository.SaveChangesAsync();

    var response = new SetCurrencyRateResponse("update successfully");

    return Ok(response);
  }
}
