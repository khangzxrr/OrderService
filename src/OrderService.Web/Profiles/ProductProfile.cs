using AutoMapper;
using OrderService.Core.ProductAggregate;
using OrderService.Web.Endpoints.ProductEndpoints;

namespace OrderService.Web.Profiles;

public class ProductProfile : Profile
{
  public ProductProfile() {
    CreateMap<Product, ProductRecord>()
      .ForCtorParam(nameof(ProductRecord.id), options => options.MapFrom(p => p.Id))
      .ForCtorParam(nameof(ProductRecord.category), options => options.MapFrom(p => p.productCategory.productCategoryName))
      .ForCtorParam(nameof(ProductRecord.name), options => options.MapFrom(p => p.productName))
      .ForCtorParam(nameof(ProductRecord.imageUrl), options => options.MapFrom(p => p.productImageUrl))
      .ForCtorParam(nameof(ProductRecord.description), options => options.MapFrom(p => p.productDescription))

      .ForCtorParam(nameof(ProductRecord.price), options => options.MapFrom(p => p.productPrice))
      .ForCtorParam(nameof(ProductRecord.shipCost), options => options.MapFrom(p => p.productCategory.productShipCost.shipCost))
      .ForCtorParam(nameof(ProductRecord.costPerWeight), options => options.MapFrom(p => p.productCategory.productShipCost.costPerWeight))

      .ForCtorParam(nameof(ProductRecord.url), options => options.MapFrom(p => p.productURL))
      .ForCtorParam(nameof(ProductRecord.weight), options => options.MapFrom(p => p.productWeight))
      .ForCtorParam(nameof(ProductRecord.sellerAddress), options => options.MapFrom(p => p.productSellerAddress))
      .ForCtorParam(nameof(ProductRecord.sellerEmail), options => options.MapFrom(p => p.productSellerEmail))
      .ForCtorParam(nameof(ProductRecord.warrantable), options => options.MapFrom(p => p.productWarrantable))
      .ForCtorParam(nameof(ProductRecord.warrantyDescription), options => options.MapFrom(p => p.productWarrantyDescription))
      .ForCtorParam(nameof(ProductRecord.warrantyDuration), options => options.MapFrom(p => p.productWarrantyDuration))
      .ForCtorParam(nameof(ProductRecord.returnable), options => options.MapFrom(p => p.productReturnable))
      .ForCtorParam(nameof(ProductRecord.returnDescription), options => options.MapFrom(p => p.productReturnDescription))
      .ForCtorParam(nameof(ProductRecord.returnDuration), options => options.MapFrom(p => p.productReturnDuration));
  }
}
