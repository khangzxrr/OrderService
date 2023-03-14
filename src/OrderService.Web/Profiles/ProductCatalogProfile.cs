using AutoMapper;
using OrderService.Core.ProductAggregate;
using OrderService.Web.Endpoints.ProductCatalogEndpoints;

namespace OrderService.Web.Profiles;

public class ProductCatalogProfile : Profile
{
  public ProductCatalogProfile()
  {
    CreateMap<ProductCategory, ProductCategoryRecord>()
      .ForCtorParam(nameof(ProductCategoryRecord.id), options => options.MapFrom(pc => pc.Id))
      .ForCtorParam(nameof(ProductCategoryRecord.categoryName), options => options.MapFrom(pc => pc.productCategoryName));
  }
}
