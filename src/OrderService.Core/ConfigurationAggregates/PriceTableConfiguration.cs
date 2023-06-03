using OrderService.SharedKernel;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.ConfigurationAggregates;
public class PriceTableConfiguration : EntityBase, IAggregateRoot
{
  public string categoryName { get; set; }
  public string processCostDescription { get; set; }
  public string additionalCostDescription { get; set; }

  public PriceTableConfiguration(string categoryName, string processCostDescription, string additionalCostDescription)
  {
    this.categoryName = categoryName;
    this.processCostDescription = processCostDescription;
    this.additionalCostDescription = additionalCostDescription;
  }
}
