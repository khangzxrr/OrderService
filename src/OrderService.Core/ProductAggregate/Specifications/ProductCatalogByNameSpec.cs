using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace OrderService.Core.ProductAggregate.Specifications;
public class ProductCatalogByNameSpec : Specification<ProductCategory>, ISingleResultSpecification
{
  public ProductCatalogByNameSpec(string name)
  {
    Query
      .Where(pc => pc.productCategoryName == name);
  }
}
