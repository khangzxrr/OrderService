using System.CodeDom;
using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class GetOrdersRequest
{

  public const string Route = "/manager/orders";

  public string? statusName {  get; set; }

  [DataType(DataType.DateTime)]
  public DateTime? startDate { get; set; }
  [DataType(DataType.DateTime)]
  public DateTime? endDate { get; set; }

  public int pageIndex { get; set; }
  public int pageSize { get; set; }

}
