using OrderService.Core.OrderAggregate;
using OrderService.Core.ShipperAggregate;
using OrderService.SharedKernel;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.OrderShippingAggregate;
public class OrderShipping : EntityBase, IAggregateRoot
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public Order order { get; set; }
  public Shipper shipper { get; set; }
  public bool shippingUsing3rd { get; set; }
  public OrderShippingStatus orderShippingStatus { get; set; }
  public string shippingDescription { get; set; }
  public string signatureImageUrl { get; set; }

}
