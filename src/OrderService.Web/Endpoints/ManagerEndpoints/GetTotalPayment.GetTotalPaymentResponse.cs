namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class GetTotalPaymentResponse
{
  public double totalPayment { get; set; }

  public GetTotalPaymentResponse(double totalPayment)
  {
    this.totalPayment = totalPayment;
  }
}
