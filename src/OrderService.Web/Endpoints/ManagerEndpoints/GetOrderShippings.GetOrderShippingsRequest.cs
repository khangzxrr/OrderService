
using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class GetOrderShippingsRequest
{
  public const string Route = "/manager/ordershippings";

  [Required]
  public int pageIndex {  get; set; }
  [Required]
  public int pageSize { get; set; }


}
