using Ardalis.Specification;

namespace OrderService.Core.CurrencyAggregate.Specifications;
public class CurrencyExchangeById: Specification<CurrencyExchange>, ISingleResultSpecification
{
  public CurrencyExchangeById(int id) { 
    Query
      .Where(c => c.Id == id);
  }
}
