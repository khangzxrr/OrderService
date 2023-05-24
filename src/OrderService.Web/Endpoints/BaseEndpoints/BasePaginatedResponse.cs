using OrderService.Core.ShipperAggregate;

namespace OrderService.Web.Endpoints.BaseEndpoints;

public abstract class BasePaginatedResponse<T>
{

  public int totalCount { get; set; }
  public IEnumerable<T> records { get; set; }

  public BasePaginatedResponse(int totalCount, int pageSize, IEnumerable<T> records)
  {
    this.totalCount = totalCount;
    this.records = records;
  }

}
