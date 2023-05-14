using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class GetTotalPaymentRequest
{
  public const string Route = "/manager/payments/total";

  [DataType(DataType.DateTime)]
  public DateTime? startDate { get; init; }
  [DataType(DataType.DateTime)]
  public DateTime? endDate { get; init; }

}
