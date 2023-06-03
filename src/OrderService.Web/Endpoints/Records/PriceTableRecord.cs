using OrderService.Core.ConfigurationAggregates;

namespace OrderService.Web.Endpoints.Records;

public record PriceTableRecord(int id, string category, string addtionalCostDescription, string processCostDescription)
{
  public static PriceTableRecord FromEntity(PriceTableConfiguration priceTableConfiguration)
  {
    return new PriceTableRecord(priceTableConfiguration.Id, priceTableConfiguration.categoryName, priceTableConfiguration.additionalCostDescription, priceTableConfiguration.processCostDescription);
  }
}
