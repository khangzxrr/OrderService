using OrderService.Core.ProductReturnAggregate;

namespace OrderService.Web.Endpoints.Records;

public record ProductIssueRecord( ProductRecord productRecord, int id, string status, int statusCode, IEnumerable<string> medias, DateTime returnDate, string returnReason, bool isWarranty, string customerEmail, string customerFullname, string customerPhonenumber, string series, IEnumerable<ProductIssueStateTrackingRecord> stateTracking)
{
  public static ProductIssueRecord FromEntity(ProductIssue productIssue)
  {
    return new ProductIssueRecord(
      ProductRecord.FromEntity(productIssue.product),
      productIssue.Id,
      productIssue.status.Name,
      productIssue.status.Value,
      productIssue.issueMedias.Select(m => m.mediaUrl),
      productIssue.returnDate,
      productIssue.returnReason,
      productIssue.isWarranty,
      productIssue.customerEmail,
      productIssue.customerFullname,
      productIssue.customerPhonenumber,
      productIssue.series,
      productIssue.issueStateTrackings.Select(ProductIssueStateTrackingRecord.FromEntity)
      );
  }
}
