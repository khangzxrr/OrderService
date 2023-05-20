using OrderService.Core.OrderShippingAggregate;

namespace OrderService.Web.Endpoints.Records;

public record OrderShippingRecord(int id, bool shippingUsing3rd, string shippingStatus, string shippingDescription, string signatureImageUrl, string shipperName, int orderId)
{
  public static OrderShippingRecord FromEntity(OrderShipping orderShipping)
  {
    return new OrderShippingRecord(orderShipping.Id,
        orderShipping.shippingUsing3rd,
        orderShipping.orderShippingStatus.Name,
        orderShipping.shippingDescription,
        orderShipping.signatureImageUrl,
        orderShipping.shipper != null ? orderShipping.shipper.user.fullname : "",
        orderShipping.orderId
      );
  }
}
