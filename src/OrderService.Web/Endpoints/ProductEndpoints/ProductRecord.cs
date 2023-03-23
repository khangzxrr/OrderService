using OrderService.Core.OrderAggregate;
using OrderService.Core.ProductAggregate;

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

  public static ProductRecord FromEntity(Product product)
  {
    return new ProductRecord(
      product.Id,
      product.productCategory.productCategoryName,
      product.productName,
      product.productImageUrl,
      product.productDescription,
      product.productPrice,
      product.productCategory.productShipCost.shipCost,
      product.productCategory.productShipCost.costPerWeight,
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
  }

  public static ProductRecord FromEntity(ProductHistory product)
  {
    return new ProductRecord(
      product.Id,
      product.productCategory.productCategoryName,
      product.productName,
      product.productImageUrl,
      product.productDescription,
      product.productPrice,
      product.productCategory.productShipCost.shipCost,
      product.productCategory.productShipCost.costPerWeight,
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
  }
}
