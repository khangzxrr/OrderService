using AutoMapper;
using OrderService.Core.OrderAggregate;
using OrderService.Web.Endpoints.OrderEndpoints;

namespace OrderService.Web.Profiles;

public class OrderProfile : Profile
{
  public OrderProfile()
  {
    CreateMap<Order, OrderRecord>()
      .ForCtorParam(nameof(OrderRecord.progressStatus), options => options.MapFrom(or => or.status.Value))
      .ForCtorParam(nameof(OrderRecord.status), options => options.MapFrom(or => or.status.Name))
      .ForCtorParam(nameof(OrderRecord.contactPhoneNumber), options => options.MapFrom(or => or.contactPhonenumber))
      .ForCtorParam(nameof(OrderRecord.price), options => options.MapFrom(or => or.price))
      .ForCtorParam(nameof(OrderRecord.customerDescription), options => options.MapFrom(or => or.customerDescription))
      .ForCtorParam(nameof(OrderRecord.deliveryAddress), options => options.MapFrom(or => or.deliveryAddress))
      .ForCtorParam(nameof(OrderRecord.orderDate), options => options.MapFrom(or => or.orderDate))
      .ForCtorParam(nameof(OrderRecord.orderId), options => options.MapFrom(or => or.Id))
      .ForCtorParam(nameof(OrderRecord.shipEstimatedDays), options => options.MapFrom(or => or.shippingEstimatedDays))
      .ForCtorParam(nameof(OrderRecord.orderDetails), options => options.MapFrom(or => (or.orderDetails == null) ? null : or.orderDetails.Select(od => OrderDetailRecord.FromEntity(od))));


  }
}
