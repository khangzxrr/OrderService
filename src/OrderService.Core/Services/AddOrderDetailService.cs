
using Ardalis.Result;
using Newtonsoft.Json;
using OrderService.Core.Interfaces;
using OrderService.Core.OrderAggregate;
using OrderService.Core.ProductAggregate;
using OrderService.Core.ProductAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;
using RulesEngine.Models;

namespace OrderService.Core.Services;
public class AddOrderDetailService : IAddOrderDetailService
{

  private readonly IRepository<Product> _productRepository;

  public AddOrderDetailService(IRepository<Product> productRepository)
  {
    _productRepository = productRepository;
  }

  public Result UpdateOrderDetails(Order order)
  {

    double totalOrderCost = 0.0f;

    foreach(var orderDetail in order.orderDetails)
    {

      var additionalCostWithWeightCost = (orderDetail.product.productWeight * orderDetail.costPerWeight);

      orderDetail.setAdditionalCost(additionalCostWithWeightCost);

      var totalOrderDetailCost = orderDetail.shipCost;
      totalOrderDetailCost += orderDetail.product.productPrice * orderDetail.quantity;
      totalOrderDetailCost += orderDetail.additionalCost;
      totalOrderDetailCost += orderDetail.processCost;

      orderDetail.setTotalCost(totalOrderDetailCost);

      totalOrderCost += totalOrderDetailCost * orderDetail.product.currencyExchange.rate;
    }

    totalOrderCost = Math.Ceiling(totalOrderCost);

    //IMPORTANT
    var newRemainCost = order.remainCost + (totalOrderCost - order.price);

    order.SetPrice(totalOrderCost);
    order.SetRemainCost(newRemainCost);

    return Result.Success();
  }


  public async Task<Result> AddOrderDetail(Order order, int productId, int quantity)
  {
    if (order == null)
    {
      return Result.Error($"{nameof(order)} cannot be null.");
    }
    if (quantity < 1)
    {
      return Result.Error($"{nameof(quantity)} cannot be 0 or negative.");
    }

    var productSpec = new ProductByIdSpec(productId);
    var product = await _productRepository.FirstOrDefaultAsync(productSpec);

    if (product == null)
    {
      return Result.Error($"{nameof(product)} is not found.");
    }

    var orderDetail = new OrderDetail();
    orderDetail.setProduct(product);
    orderDetail.setQuantity(quantity);

    orderDetail.setAdditionalCost(0);

    order.addOrderDetail(orderDetail);

    return Result.Success();
  }
}
