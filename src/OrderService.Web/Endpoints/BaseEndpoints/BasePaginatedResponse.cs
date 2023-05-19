using OrderService.Core.ShipperAggregate;

namespace OrderService.Web.Endpoints.BaseEndpoints;

public abstract class BasePaginatedResponse<T>
{

  public int pageCount { get; set; }
  public IEnumerable<T> records { get; set; }

  public BasePaginatedResponse(int totalCount, int pageSize, IEnumerable<T> records)
  {
    this.pageCount = Utils.Utils.getPageCount(totalCount, pageSize);
    this.records = records;
  }

}
