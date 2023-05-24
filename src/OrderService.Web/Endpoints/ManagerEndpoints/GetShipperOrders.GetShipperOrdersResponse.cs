using OrderService.Web.Endpoints.BaseEndpoints;
using OrderService.Web.Endpoints.Records;

namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class GetShipperOrdersResponse : BasePaginatedResponse<GeneralOrderRecord>
{

  public float totalPaymentReceived { get; set; }

  public GetShipperOrdersResponse(int totalCount, int pageSize, IEnumerable<GeneralOrderRecord> records, float totalPaymentReceived) : base(totalCount, pageSize, records)
  {
    this.totalPaymentReceived = totalPaymentReceived;
  }
}
