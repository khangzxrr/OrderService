using OrderService.Core.OrderAggregate;
using OrderService.Core.ProductAggregate;
using OrderService.Web.Endpoints.ProductEndpoints;

namespace OrderService.Web.Endpoints.Records;

public record ProductRecord(
  int id,
  string category,
  string name,
  string imageUrl,
  string description,

  float price,

  string url,
  float weight,
  string sellerAddress,
  string sellerEmail,
  bool warrantable,
  string warrantyDescription,
  int warrantyDuration,
  bool returnable,
  string returnDescription,
  int returnDuration,

  ProductCurrencyExchangeRecord productCurrencyExchangeRecord
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
      product.productURL,
      product.productWeight,
      product.productSellerAddress,
      product.productSellerEmail,
      product.productWarrantable,
      product.productWarrantyDescription,
      product.productWarrantyDuration,
      product.productReturnable,
      product.productReturnDescription,
      product.productReturnDuration,
      ProductCurrencyExchangeRecord.FromEntity(product.currencyExchange)
      );
  }

}
