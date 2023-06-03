using Ardalis.GuardClauses;
using OrderService.Core.OrderShippingAggregate;
using OrderService.Core.ProductIssueAggregate;
using OrderService.Core.UserAggregate;
using OrderService.SharedKernel;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.ShipperAggregate;
public class Shipper : EntityBase, IAggregateRoot
{

  public ShippingStatus shippingStatus { get; set; }

  private List<OrderShipping> _orderShippings = new List<OrderShipping>();
  public IReadOnlyCollection<OrderShipping> OrderShippings => _orderShippings.AsReadOnly();

  private List<ProductIssueShipping> _productIssueShippings = new List<ProductIssueShipping>();
  public IReadOnlyCollection<ProductIssueShipping> ProductIssueShippings => _productIssueShippings.AsReadOnly();

  public int userId { get; set; }
  public User user { get; set; }

  #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public Shipper()
  {
    this.shippingStatus = ShippingStatus.online;
  } 
}
