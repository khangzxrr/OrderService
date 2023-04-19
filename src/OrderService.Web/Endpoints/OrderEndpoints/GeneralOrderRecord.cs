using OrderService.Core.OrderAggregate;

namespace OrderService.Web.Endpoints.OrderEndpoints;

public record GeneralOrderRecord(
  int orderId,
  DateTime orderDate, 
  string orderStatus, 
  string customerName,
  string orderDescription, 
  string customerDescription,
  string deliveryAddress,
  string contactPhoneNumber,
  int shippingEstimatedDays,
  float price, 
  float remainCost
  )
{
  public static GeneralOrderRecord FromEntity(Order order)
  {
    return new GeneralOrderRecord(order.Id, order.orderDate, order.status.Name, order.user.lastname + " " + order.user.firstname,order.orderDescription, order.customerDescription, order.deliveryAddress, order.contactPhonenumber, order.shippingEstimatedDays, order.price, order.remainCost);
  }
}
