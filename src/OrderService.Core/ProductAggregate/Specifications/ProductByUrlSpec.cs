using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace OrderService.Core.ProductAggregate.Specifications;
public class ProductByUrlSpec : Specification<Product>, ISingleResultSpecification
{
  public ProductByUrlSpec(string url)
  {
    Query
      .Where(p => p.productURL == url)
      .Include(p => p.currencyExchange)
      .Include(p => p.productCategory);
      
  }
}
