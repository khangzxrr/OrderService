﻿using OrderService.Core.ProductIssueAggregate;

namespace OrderService.Web.Endpoints.Records;

public record ProductIssueStateTrackingRecord(int id, string status, long changeDate)
{
  public static ProductIssueStateTrackingRecord FromEntity(IssueStateTracking issueStateTracking)
  {
    return new ProductIssueStateTrackingRecord(issueStateTracking.Id, issueStateTracking.productIssueStatus.Name, Utils.Utils.toUnixTime(issueStateTracking.changeDate));
  }
}
