using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class GetPaymentsRequest
{
  public const string Route = "/manager/payments";

  public int pageSize { get; set; }
  public int pageIndex { get; set; }

  [Required]
  [DataType(DataType.DateTime)]
  public DateTime? startDate { get; set; }
  [Required]
  [DataType(DataType.DateTime)]
  public DateTime? endDate { get; set; }
}
