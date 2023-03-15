using Ardalis.GuardClauses;
using OrderService.Core.CurrencyAggregate;
using OrderService.SharedKernel;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.ProductAggregate;
public class ProductHistory : EntityBase
{
  public ProductCategory productCategory { get; set; }
  public string productName { get; set; }
  public string productImageUrl { get; set; }
  public string productDescription { get; set; }
  public float productPrice { get; set; }
  public string productURL { get; set; }
  public float productWeight { get; set; }

  public string productSellerAddress { get; set; }
  public string productSellerEmail { get; set; }

  public bool productWarrantable { get; set; }
  public string productWarrantyDescription { get; set; }
  public int productWarrantyDuration { get; set; }

  public bool productReturnable { get; set; }
  public string productReturnDescription { get; set; }
  public int productReturnDuration { get; set; }

  private List<ProductTax> _productTaxes = new List<ProductTax>();
  public IReadOnlyCollection<ProductTax> productTaxes => _productTaxes.AsReadOnly();

  public CurrencyExchange currencyExchange { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public ProductHistory(
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    string productName,
    string productImageUrl,
    string productDescription,
    float productPrice,
    string productURL,
    float productWeight,
    string productSellerAddress,
    string productSellerEmail,
    bool productWarrantable,
    string productWarrantyDescription,
    int productWarrantyDuration,
    bool productReturnable,
    string productReturnDescription,
    int productReturnDuration
)
  {

    this.productName = Guard.Against.NullOrEmpty(productName);
    this.productImageUrl = Guard.Against.NullOrEmpty(productImageUrl);
    this.productDescription = Guard.Against.NullOrEmpty(productDescription);
    this.productPrice = Guard.Against.Negative(productPrice);
    this.productURL = Guard.Against.NullOrEmpty(productURL);
    this.productWeight = Guard.Against.Negative(productWeight);

    this.productSellerAddress = Guard.Against.NullOrEmpty(productSellerAddress);
    this.productSellerEmail = Guard.Against.NullOrEmpty(productSellerEmail);
    this.productWarrantable = productWarrantable;
    this.productWarrantyDescription = Guard.Against.NullOrEmpty(productWarrantyDescription);
    this.productWarrantyDuration = Guard.Against.Negative(productWarrantyDuration);
    this.productReturnable = productReturnable;
    this.productReturnDescription = Guard.Against.NullOrEmpty(productReturnDescription);
    this.productReturnDuration = Guard.Against.Negative(productReturnDuration);
  }

  public void SetProductCategory(ProductCategory productCategory)
  {
    this.productCategory = Guard.Against.Null(productCategory);
  }

  public void SetCurrencyExchange(CurrencyExchange currencyExchange)
  {
    this.currencyExchange = currencyExchange;
  }
}
