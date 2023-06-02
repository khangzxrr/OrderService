using OrderService.SharedKernel;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.ConfigurationAggregates;
public class PriceTableConfiguration : EntityBase, IAggregateRoot
{
  public string categoryName { get; set; }
  public string priceWeightDescription { get; set; }
  public string additionalCostDescription { get; set; }

  public PriceTableConfiguration(string categoryName, string priceWeightDescription, string additionalCostDescription)
  {
    this.categoryName = categoryName;
    this.priceWeightDescription = priceWeightDescription;
    this.additionalCostDescription = additionalCostDescription;
  }
}
