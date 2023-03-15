using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace OrderService.Core.ProductAggregate.Specifications;
public class ProductByNameSpec: Specification<Product>, ISingleResultSpecification
{
  public ProductByNameSpec(string name)
  {
    Query
      .Where(p => p.productName == name)
      .Include(p => p.productCategory)
      .ThenInclude(pc => pc.productShipCost);
  }
}
