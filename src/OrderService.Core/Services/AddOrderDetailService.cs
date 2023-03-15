
using System.Dynamic;
using Ardalis.Result;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using OrderService.Core.Interfaces;
using OrderService.Core.OrderAggregate;
using OrderService.Core.ProductAggregate;
using OrderService.Core.ProductAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;
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

  public async Task<Result> AddOrderDetail(Order order, string productUrl, int quantity)
  {
    if (order == null)
    {
      return Result.Error($"{nameof(order)} cannot be null.");
    }
    if (productUrl.IsNullOrEmpty())
    {
      return Result.Error($"{nameof(productUrl)} cannot be null or empty");
    }
    if (quantity < 1)
    {
      return Result.Error($"{nameof(quantity)} cannot be 0 or negative.");
    }

    var productSpec = new ProductByUrlSpec(productUrl);
    var product = await _productRepository.FirstOrDefaultAsync(productSpec);

    if (product == null)
    {
      return Result.Error($"{nameof(product)} is not found.");
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

    var orderDetail = new OrderDetail();
    orderDetail.setProduct(productHistory);
    orderDetail.setQuantity(quantity);

    

    var conditionString = productHistory.productCategory.productShipCost.additionalCostCondition;
    var workflow = JsonConvert.DeserializeObject<List<Workflow>>(conditionString);

    var re = new RulesEngine.RulesEngine(workflow!.ToArray(), null);


    var rule1 = new RuleParameter("orderDetail", new
    {
      productCost = orderDetail.productCost
    });
    List<RuleParameter> ruleParameters = new List<RuleParameter>
    {
      rule1 
    };

    var result = await re.ExecuteActionWorkflowAsync("AdditionalCost", "Price_over", ruleParameters.ToArray());

    Console.WriteLine(result.Output);
    float additionalCost = (float)(double)result.Output;
    orderDetail.setAdditionalCost(additionalCost);

    order.addOrderDetail(orderDetail);


    //clone product
    product.addProductHistory(productHistory);
    //await _productRepository.UpdateAsync(product);
    //await _productRepository.SaveChangesAsync();

    //await _orderRepository.AddAsync(order);
    //await _orderRepository.SaveChangesAsync();

    return Result.Success();
  }
}
