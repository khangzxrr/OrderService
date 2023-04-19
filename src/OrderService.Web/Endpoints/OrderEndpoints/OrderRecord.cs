namespace OrderService.Web.Endpoints.OrderEndpoints;

public record OrderRecord(
  int orderId, 
  DateTime orderDate, 
  string status,
  string customerName,
  int progressStatus,
  string customerDescription,
  string deliveryAddress,
  string contactPhoneNumber,
  int shipEstimatedDays,
  float price,
  float remainCost,
  IEnumerable<OrderDetailRecord>? orderDetails
  )
{
}
