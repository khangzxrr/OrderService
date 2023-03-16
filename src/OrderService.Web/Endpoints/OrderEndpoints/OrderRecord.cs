namespace OrderService.Web.Endpoints.OrderEndpoints;

public record OrderRecord(
  int orderId, 
  DateTime orderDate, 
  string status,
  string customerDescription,
  string deliveryAddress,
  string contactPhoneNumber,
  int shipEstimatedDays,
  float price,
  IEnumerable<OrderDetailRecord>? orderDetails
  )
{
}
