using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class SetCurrencyRateRequest
{
  public const string Route = "/manager/currencies/rate/update";

  [Required]
  public int currencyId { get; set; }
  [Required]
  public float rate { get; set; }
}
