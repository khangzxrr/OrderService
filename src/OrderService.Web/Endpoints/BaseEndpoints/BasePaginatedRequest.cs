using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.BaseEndpoints;

public abstract class BasePaginatedRequest
{
  [Required]
  public int pageIndex { get; set; }
  [Required]
  public int pageSize { get; set; } = 10;

  public int skip => pageIndex * pageSize;
  public int take => (pageSize == 0) ? int.MaxValue : pageSize;


}
