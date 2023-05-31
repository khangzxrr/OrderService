using OrderService.Web.Endpoints.Records;

namespace OrderService.Web.Endpoints.ProductReturnEndpoints;

public class RequestProductReturnResponse
{
  public ProductReturnRecord productReturnRecord { get; set; }

  public RequestProductReturnResponse(ProductReturnRecord productReturnRecord)
  {
    this.productReturnRecord = productReturnRecord;
  }
}
