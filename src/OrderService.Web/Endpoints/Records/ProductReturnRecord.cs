using OrderService.Core.ProductReturnAggregate;

namespace OrderService.Web.Endpoints.Records;

public record ProductReturnRecord(ProductRecord productRecord, int id, string status, int statusCode, IEnumerable<string> medias, DateTime returnDate, string returnReason, bool isWarranty)
{
  public static ProductReturnRecord FromEntity(ProductReturn productReturn)
  {
    return new ProductReturnRecord(
      ProductRecord.FromEntity(productReturn.product),
      productReturn.Id,
      productReturn.status.Name,
      productReturn.status.Value,
      productReturn.ReturnMedias.Select(m => m.mediaUrl),
      productReturn.returnDate,
      productReturn.returnReason,
      productReturn.isWarranty
      );
  }
}
