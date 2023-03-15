using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace OrderService.Core.CurrencyAggregate.Specifications;
public class CurrencyExchangeByName : Specification<CurrencyExchange>, ISingleResultSpecification
{
  public CurrencyExchangeByName(string name)
  {
    Query
      .Where(c => c.fromCurrency == name);
  }
}
