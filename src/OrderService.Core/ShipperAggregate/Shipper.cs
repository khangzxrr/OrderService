using Ardalis.GuardClauses;
using OrderService.Core.OrderShippingAggregate;
using OrderService.Core.UserAggregate;
using OrderService.SharedKernel;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.ShipperAggregate;
public class Shipper : EntityBase, IAggregateRoot
{
  public string shippingDistrict { get; set; }
  public DateTime shippingStartTime { get; set; }
  public DateTime shippingEndTime { get; set; }

  public ShippingStatus shippingStatus { get; set; }

  private List<OrderShipping> _orderShippings = new List<OrderShipping>();
  public IReadOnlyCollection<OrderShipping> OrderShippings => _orderShippings.AsReadOnly();

  public int userId { get; set; }
  public User user { get; set; }

  #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public Shipper(
    string shippingDistrict, 
    DateTime shippingStartTime, 
    DateTime shippingEndTime, 
    ShippingStatus shippingStatus)
  {
    this.shippingDistrict = Guard.Against.NullOrEmpty(shippingDistrict);
    this.shippingStartTime = shippingStartTime;
    this.shippingEndTime = shippingEndTime;
    this.shippingStatus = Guard.Against.Null(shippingStatus);
  } 
}
