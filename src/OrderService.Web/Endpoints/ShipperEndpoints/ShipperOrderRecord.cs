using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderShippingAggregate;

namespace OrderService.Web.Endpoints.ShipperEndpoints;

public record ShipperOrderRecord(int orderShippingId, string customerName, string customerAddress, string customerPhoneNumber, string orderShipingStatus, double remainCost, int orderId)
{

  public static ShipperOrderRecord FromEntity(OrderShipping orderShipping)
  {
    return new ShipperOrderRecord(orderShipping.Id, orderShipping.order.user.firstname, orderShipping.order.user.address, orderShipping.order.user.phoneNumber, orderShipping.orderShippingStatus.Name, orderShipping.order.remainCost, orderShipping.orderId);
  }
}
