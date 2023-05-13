namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class GetPaymentsResponse
{
  public int pageCount { get; set; }
  public IEnumerable<PaymentRecord> paymentRecords { get; set; }

  public double totalPayment { get; set; }

  public GetPaymentsResponse(IEnumerable<PaymentRecord> paymentRecords, int pageCount, double totalPayment)
  {
    this.paymentRecords = paymentRecords;
    this.pageCount = pageCount;
    this.totalPayment = totalPayment;
  }
}
