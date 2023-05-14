using OrderService.Core.OrderShippingAggregate;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public record OrderShippingRecord(int id, bool shippingUsing3rd, string shippingStatus, string shippingDescription, string signatureImageUrl, string shipperName, int orderId)
{
  public static OrderShippingRecord FromEntity(OrderShipping orderShipping)
  {
    return new OrderShippingRecord(orderShipping.Id,
        orderShipping.shippingUsing3rd,
        orderShipping.orderShippingStatus.Name,
        orderShipping.shippingDescription,
        orderShipping.signatureImageUrl,
        (orderShipping.shippingUsing3rd) ? "3rd" : orderShipping.shipper!.user.getFullname(),
        orderShipping.orderId
      );
  }
}
