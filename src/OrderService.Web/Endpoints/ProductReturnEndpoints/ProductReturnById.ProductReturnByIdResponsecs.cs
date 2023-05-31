using OrderService.Web.Endpoints.Records;

namespace OrderService.Web.Endpoints.ProductReturnEndpoints;

public class ProductReturnByIdResponse
{
  public ProductReturnRecord productReturnRecord { get; set; }

  public ProductReturnByIdResponse(ProductReturnRecord productReturnRecord)
  {
    this.productReturnRecord = productReturnRecord;
  }
}
