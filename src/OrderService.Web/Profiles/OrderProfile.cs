using AutoMapper;
using OrderService.Core.OrderAggregate;
using OrderService.Core.ProductAggregate;
using OrderService.Web.Endpoints.OrderEndpoints;
using OrderService.Web.Endpoints.Records;

namespace OrderService.Web.Profiles;

public class OrderProfile : Profile
{
  public OrderProfile()
  {
    CreateMap<Order, OrderRecord>()
      .ForCtorParam(nameof(OrderRecord.progressStatus), options => options.MapFrom(or => or.status.Value))
      .ForCtorParam(nameof(OrderRecord.status), options => options.MapFrom(or => or.status.Name))
      .ForCtorParam(nameof(OrderRecord.customerName), options => options.MapFrom(or => or.user.fullName))
      .ForCtorParam(nameof(OrderRecord.contactPhoneNumber), options => options.MapFrom(or => or.contactPhonenumber))
      .ForCtorParam(nameof(OrderRecord.price), options => options.MapFrom(or => or.price))
      .ForCtorParam(nameof(OrderRecord.remainCost), options => options.MapFrom(or => or.remainCost))
      .ForCtorParam(nameof(OrderRecord.customerDescription), options => options.MapFrom(or => or.customerDescription))
      .ForCtorParam(nameof(OrderRecord.deliveryAddress), options => options.MapFrom(or => or.deliveryAddress))
      .ForCtorParam(nameof(OrderRecord.orderDate), options => options.MapFrom(or => Utils.Utils.toUnixTime(or.orderDate)))
      .ForCtorParam(nameof(OrderRecord.orderId), options => options.MapFrom(or => or.Id))
      .ForCtorParam(nameof(OrderRecord.shipEstimatedDays), options => options.MapFrom(or => or.shippingEstimatedDays))
      .ForCtorParam(nameof(OrderRecord.employeeName), options => options.MapFrom(or => or.chat.employee.fullName))
      .ForCtorParam(nameof(OrderRecord.orderDetails), options => options.MapFrom(or => (or.orderDetails == null) ? null : or.orderDetails.Where(od => od.product.productStatus != ProductStatus.disable).Select(od => OrderDetailRecord.FromEntity(od))))
      .ForCtorParam(nameof(OrderRecord.localOrderShippingStatus), options => options.MapFrom(or => or.localShippingStatus.Name))
      .ForCtorParam(nameof(OrderRecord.firstPaymentAmount), options => options.MapFrom(or => or.GetFirstPaymentAmount())); ;
      


  }
}
