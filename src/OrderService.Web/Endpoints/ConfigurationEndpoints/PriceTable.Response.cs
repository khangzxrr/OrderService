using OrderService.Web.Endpoints.Records;

namespace OrderService.Web.Endpoints.ConfigurationEndpoints;

public class PriceTableResponse
{
  public IEnumerable<PriceTableRecord> priceTables { get; set; }

  public PriceTableResponse(IEnumerable<PriceTableRecord> priceTables)
  {
    this.priceTables = priceTables;
  }
}
