using OrderService.Web.Endpoints.OrderEndpoints;

namespace OrderService.Web.Endpoints.Records;

public record OrderRecord(
  int orderId,
  long orderDate,
  string status,
  string customerName,
  int progressStatus,
  string customerDescription,
  string deliveryAddress,
  string contactPhoneNumber,
  int shipEstimatedDays,
  float price,
  float remainCost,
  float firstPaymentAmount,
  string localOrderShippingStatus,
  string employeeName,
  IEnumerable<OrderDetailRecord>? orderDetails
  )
{
}
