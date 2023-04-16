
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

  public AddOrderDetailService(IRepository<Product> productRepository)
  {
    _productRepository = productRepository;
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


    var conditionString = product.productCategory.productShipCost.additionalCostCondition.Replace('\t',' ');
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

    float additionalCost = (float)Convert.ToDouble(result.Output);
    orderDetail.setAdditionalCost(additionalCost);

    order.addOrderDetail(orderDetail);

    return Result.Success();
  }
}
