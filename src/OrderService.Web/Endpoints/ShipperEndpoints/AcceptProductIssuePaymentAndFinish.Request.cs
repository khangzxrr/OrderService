using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.ShipperEndpoints;

public class AcceptProductIssuePaymentAndFinishRequest
{
  public const string Route = "/shipper/productIssues/acceptPaymentAndFinish";

  [Required]
  public int productIssueId { get; set; }
}
