

using Ardalis.GuardClauses;
using OrderService.Core.OrderShippingAggregate;
using OrderService.Core.ShipperAggregate;
using OrderService.SharedKernel;

namespace OrderService.Core.ProductIssueAggregate;
public class ProductIssueShipping : EntityBase
{
  public Shipper shipper { get; private set; }

  public OrderShippingStatus shippingStatus { get; private set; }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public ProductIssueShipping()
  {

    shippingStatus = OrderShippingStatus.inWarehouse;
  }

  public void SetShipper(Shipper shipper)
  {
    this.shipper = Guard.Against.Null(shipper);
  }

  public void SetShippingStatus(OrderShippingStatus shippingStatus)
  {
    this.shippingStatus = Guard.Against.Null(shippingStatus);
  }

}
