using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.OrderEndpoints;

public class PayRequest
{
  public const string Route = "/orders/pay";

  #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  [Required]
  public int OrderId { get; set; }
  [Required]
  [DataType(DataType.Url)]
  public string Hostname { get; set; }


}
