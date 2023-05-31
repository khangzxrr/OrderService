using OrderService.Core.ProductReturnAggregate;

namespace OrderService.Web.Endpoints.Records;

public record ProductIssueRecord( ProductRecord productRecord, int id, string status, int statusCode, IEnumerable<string> medias, DateTime returnDate, string returnReason, bool isWarranty, string customerEmail, string customerFullname, string customerPhonenumber, string series, string finishStatus)
{
  public static ProductIssueRecord FromEntity(ProductIssue productReturn)
  {
    return new ProductIssueRecord(
      ProductRecord.FromEntity(productReturn.product),
      productReturn.Id,
      productReturn.status.Name,
      productReturn.status.Value,
      productReturn.issueMedias.Select(m => m.mediaUrl),
      productReturn.returnDate,
      productReturn.returnReason,
      productReturn.isWarranty,
      productReturn.customerEmail,
      productReturn.customerFullname,
      productReturn.customerPhonenumber,
      productReturn.series,
      productReturn.finishStatus.Name
      );
  }
}
