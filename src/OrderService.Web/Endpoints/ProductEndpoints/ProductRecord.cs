namespace OrderService.Web.Endpoints.ProductEndpoints;

public record ProductRecord(
  int id, 
  string category,
  string name,
  string imageUrl,
  string description,

  float price,
  float shipCost,
  float costPerWeight,

  string url,
  float weight,
  string sellerAddress,
  string sellerEmail,
  bool warrantable,
  string warrantyDescription,
  int warrantyDuration,
  bool returnable,
  string returnDescription,
  int returnDuration
  )
{
}
