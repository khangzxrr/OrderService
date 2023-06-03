using Ardalis.GuardClauses;
using Newtonsoft.Json;
using OrderService.Core.CurrencyAggregate;
using OrderService.SharedKernel;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.ProductAggregate;
public class Product: EntityBase, IAggregateRoot
{
  public ProductCategory productCategory { get; private set; }

  public string productName { get; set; }
  public string productImageUrl { get; set; }
  public string productDescription { get; set; }
  public float productPrice { get; set; }
  public float productShipCost { get; set; }
  public float productCostPerWeight { get; set; }
  public string productURL { get; set; }
  public float productWeight { get; private set; }

  public bool productWarrantable { get; set; }
  public string productWarrantyDescription { get; set; }
  public int productWarrantyDuration { get; set; }
  
  public bool productReturnable { get; set; }
  public string productReturnDescription { get; set; }
  public int productReturnDuration { get; set; }

  public DateTime productCreateAt { get; set; }

  public ProductStatus productStatus { get; private set; }

  [JsonIgnore]
  public ProductCurrencyExchange currencyExchange { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public Product(
    string productName, 
    string productImageUrl, 
    string productDescription, 
    float productPrice, 
    float productShipCost,
    float productCostPerWeight,
    string productURL, 
    float productWeight,
    bool productWarrantable, 
    string productWarrantyDescription, 
    int productWarrantyDuration, 
    bool productReturnable, 
    string productReturnDescription, 
    int productReturnDuration)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  {
    
    this.productName = Guard.Against.NullOrEmpty(productName);
    this.productImageUrl = Guard.Against.NullOrEmpty(productImageUrl);
    this.productDescription = Guard.Against.NullOrEmpty(productDescription);
    this.productPrice = Guard.Against.Negative(productPrice);
    this.productURL = Guard.Against.NullOrEmpty(productURL);
    this.productWeight = Guard.Against.Negative(productWeight);
    this.productShipCost = Guard.Against.Negative(productShipCost);
    this.productCostPerWeight = Guard.Against.Negative(productCostPerWeight);
    
    this.productWarrantable = productWarrantable;
    this.productWarrantyDescription = Guard.Against.Null(productWarrantyDescription);
    this.productWarrantyDuration = Guard.Against.Negative(productWarrantyDuration);
    this.productReturnable = productReturnable;

    if (productReturnable)
    {
      this.productReturnDescription = Guard.Against.Null(productReturnDescription);
      this.productReturnDuration = Guard.Against.Negative(productReturnDuration);
    } else
    {
      this.productReturnDescription = "  ";
      this.productReturnDuration = 0;
    }
    
    
    productCreateAt = DateTime.Now;
    productStatus = ProductStatus.notForSale;
  }


  public string generateRedisHashKey()
  {
    return "product_" + productURL;
  }

  public void disable()
  {
    productStatus = ProductStatus.disable;
    productShipCost = 0;
    productCostPerWeight = 0;
    productWeight = 0;
    productPrice = 0;
  }


  public void setProductCategory(ProductCategory productCategory)
  {
    this.productCategory = Guard.Against.Null(productCategory);
  }

  public void setCostPerWeight(float  costPerWeight)
  {
    this.productCostPerWeight = Guard.Against.Negative(costPerWeight);
  }

  public void setShipCost(float shipCost) {
    this.productShipCost = Guard.Against.Negative(shipCost);
  }

  public void setProductWeight(float weight)
  {
    this.productWeight = Guard.Against.Negative(weight);
  }
  public void setCurrencyExchange(ProductCurrencyExchange currencyExchange)
  {
    this.currencyExchange = Guard.Against.Null(currencyExchange);
  }

  public void setPrice(float price)
  {
    productPrice = Guard.Against.Negative(price);
  }

  public void setProductDescription(string description) {
    productDescription = Guard.Against.NullOrEmpty(description);
  }

  public void setProductWarrantable(bool warrantable)
  {
    productWarrantable = Guard.Against.Null(warrantable);
  }

  public void setProductWarrantyDescription(string warrantyDescription)
  {
    productWarrantyDescription = Guard.Against.NullOrEmpty(warrantyDescription);
  }

  public void setProductReturnable(bool returnable)
  {
    productReturnable = Guard.Against.Null(returnable);  
  }

  public void setProductReturnDescription(string returnDescription)
  {
    productReturnDescription = Guard.Against.Null(returnDescription);
  }

  public void setProductResellingStatus(ProductStatus productResellStatus)
  {
    this.productStatus = Guard.Against.Null(productResellStatus);
  }
}
