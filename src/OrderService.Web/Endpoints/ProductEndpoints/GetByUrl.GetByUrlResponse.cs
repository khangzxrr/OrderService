using OrderService.Web.Endpoints.Records;

namespace OrderService.Web.Endpoints.ProductEndpoints;

public class GetByUrlResponse
{
  public IEnumerable<ProductRecord> products { get; set; }

  public GetByUrlResponse(IEnumerable<ProductRecord> productRecords)
  {
    this.products = productRecords;
  }
}
