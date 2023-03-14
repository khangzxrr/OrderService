using OrderService.Core.ProductAggregate;
using Xunit;

namespace OrderService.UnitTests.Core.ProductCatalogAggregate;
public class ProductCatalogConstructor
{
  private string _categoryName = "category name";
  private ProductCategory? _productCategory;

  private ProductCategory CreateProductCategory()
  {
    return new ProductCategory(_categoryName);
  }

  [Fact]
  public void InitializesName()
  {
    _productCategory = CreateProductCategory();
    Assert.Equal(_categoryName, _productCategory.productCategoryName);
  }

  [Fact]
  public void ThrowsExceptionGivenNullName()
  {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
    Action action = () => _productCategory = new ProductCategory(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    var ex = Assert.Throws<ArgumentNullException>(() => action());

    Assert.Equal("productCategoryName", ex.ParamName);
  }

  [Fact]
  public void ThrowsExceptionGivenEmptyName()
  {
    Action action = () => _productCategory = new ProductCategory("");
    var ex = Assert.Throws<ArgumentException>(() => action());

    Assert.Equal("productCategoryName", ex.ParamName);
  }
}
