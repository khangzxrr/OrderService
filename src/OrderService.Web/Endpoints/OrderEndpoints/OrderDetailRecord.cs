using OrderService.Core.OrderAggregate;

namespace OrderService.Web.Endpoints.OrderEndpoints;

public record OrderDetailRecord(
  int orderDetailId, 
  float additionalCost, 
  float shipCost,
  float productCost,
  float processCost,
  int quantity
  )
{
  public static OrderDetailRecord FromEntity(OrderDetail orderDetail)
  {
    return new OrderDetailRecord(orderDetail.Id, orderDetail.additionalCost, orderDetail.shipCost, orderDetail.productCost, orderDetail.processCost, orderDetail.quantity);
  }
}
