using OrderService.Core.OrderAggregate;
using OrderService.Web.Endpoints.Records;

namespace OrderService.Web.Endpoints.OrderEndpoints;

public record OrderDetailRecord(
  int orderDetailId, 
  float additionalCost, 
  float shipCost,
  float costPerWeight,
  float productCost,
  float processCost,
  float totalCost,
  int quantity,
  ProductRecord product
  )
{
  public static OrderDetailRecord FromEntity(OrderDetail orderDetail)
  {
    return new OrderDetailRecord(orderDetail.Id, orderDetail.additionalCost, orderDetail.shipCost, orderDetail.costPerWeight, orderDetail.product.productPrice, orderDetail.processCost, orderDetail.totalCost, orderDetail.quantity, ProductRecord.FromEntity(orderDetail.product));
  }
}
