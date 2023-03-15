using System.Collections.Generic;
using Ardalis.Result;
using Newtonsoft.Json;
using OrderService.Core.Interfaces;
using OrderService.Core.OrderAggregate;
using OrderService.Core.ProductAggregate;
using OrderService.SharedKernel.Interfaces;
using RulesEngine;
using RulesEngine.Extensions;
using RulesEngine.Models;

namespace OrderService.Core.Services;
public class AddOrderDetailService : IAddOrderDetailService
{

  private readonly IRepository<Product> _productRepository;
  private readonly IRepository<Order> _orderRepository;

  public AddOrderDetailService(IRepository<Product> productRepository, IRepository<Order> orderRepository)
  {
    _productRepository = productRepository;
    _orderRepository = orderRepository;
  }

  public async Task<Result> AddOrderDetail(Order order, Product product, int quantity)
  {
    if (order == null)
    {
      return Result.Error($"{nameof(order)} cannot be null.");
    }
    if (product == null)
    {
      return Result.Error($"{nameof(product)} cannot be null.");
    }
    if (quantity < 1)
    {
      return Result.Error($"{nameof(quantity)} cannot be 0 or negative.");
    }

    var productHistory = new ProductHistory(product.productName,
        product.productImageUrl,
        product.productDescription,
        product.productPrice,
        product.productURL,
        product.productWeight,
        product.productSellerAddress,
        product.productSellerEmail,
        product.productWarrantable,
        product.productWarrantyDescription,
        product.productWarrantyDuration,
        product.productReturnable,
        product.productReturnDescription,
        product.productReturnDuration);

    productHistory.SetCurrencyExchange(product.currencyExchange);
    productHistory.SetProductCategory(product.productCategory);

    product.addProductHistory(productHistory);
    await _productRepository.UpdateAsync(product);

    await _productRepository.SaveChangesAsync();


    var orderDetail = new OrderDetail();
    orderDetail.setProduct(productHistory);
    orderDetail.setQuantity(quantity);


    var conditionString = productHistory.productCategory.productShipCost.additionalCostCondition;
    var workflow = JsonConvert.DeserializeObject<List<Workflow>>(conditionString);

    var re = new RulesEngine.RulesEngine(workflow!.ToArray(), null);



    var inputs = new dynamic[]
    {
      orderDetail
    };

    List<RuleResultTree> result = await re.ExecuteAllRulesAsync("AdditionalCost", inputs);

    bool isValidRule = result.TrueForAll(r => r.IsSuccess);

    result.OnSuccess((successEvent) =>
    {
      Console.WriteLine(successEvent);
      orderDetail.setAdditionalCost(5);
    });

    result.OnFail(() =>
    {
      orderDetail.setAdditionalCost(0);
    });


    order.addOrderDetail(orderDetail);
    await _orderRepository.UpdateAsync(order);

    await _orderRepository.SaveChangesAsync();

    return Result.Success();
  }
}
