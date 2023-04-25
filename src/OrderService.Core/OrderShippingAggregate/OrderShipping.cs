using Ardalis.GuardClauses;
using OrderService.Core.OrderAggregate;
using OrderService.Core.ShipperAggregate;
using OrderService.SharedKernel;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.OrderShippingAggregate;
public class OrderShipping : EntityBase, IAggregateRoot
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public int orderId { get; private set; }
  public Order order { get; private set; }

  public Shipper? shipper { get; private set; }

  public bool shippingUsing3rd { get; private set; }

  public OrderShippingStatus orderShippingStatus { get; private set; }

  public string shippingDescription { get; private set; }

  public string signatureImageUrl { get; private set; }

  public OrderShipping(bool shippingUsing3rd, string shippingDescription)
  {
    this.shippingUsing3rd = shippingUsing3rd;
    this.shippingDescription = shippingDescription;
    signatureImageUrl = "";
    orderShippingStatus = OrderShippingStatus.inWarehouse;
  }


  public void setOrder(int orderId)
  {
    this.orderId = orderId;
  }

  public void setShipper(Shipper shipper)
  {
    this.shipper = Guard.Against.Null(shipper);
  }


}
