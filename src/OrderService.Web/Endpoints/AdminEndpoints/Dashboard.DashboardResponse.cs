namespace OrderService.Web.Endpoints.AdminEndpoints;

public class DashboardResponse
{
  public int totalUserCount { get; set; }

  public DashboardResponse(int totalUserCount)
  {
    this.totalUserCount = totalUserCount;
  }
}
