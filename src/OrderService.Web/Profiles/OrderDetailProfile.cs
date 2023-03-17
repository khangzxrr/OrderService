﻿using AutoMapper;
using OrderService.Core.OrderAggregate;
using OrderService.Web.Endpoints.OrderEndpoints;

namespace OrderService.Web.Profiles;

public class OrderDetailProfile : Profile
{
  public OrderDetailProfile()
  {
    CreateMap<OrderDetail, OrderDetailRecord>()
      .ForCtorParam(nameof(OrderDetailRecord.orderDetailId), options => options.MapFrom(od => od.Id))
      .ForCtorParam(nameof(OrderDetailRecord.processCost), options => options.MapFrom(od => od.processCost))
      .ForCtorParam(nameof(OrderDetailRecord.productCost), options => options.MapFrom(od => od.productCost))
      .ForCtorParam(nameof(OrderDetailRecord.additionalCost), options => options.MapFrom(od => od.additionalCost))
      .ForCtorParam(nameof(OrderDetailRecord.quantity), options => options.MapFrom(od => od.quantity))
      .ForCtorParam(nameof(OrderDetailRecord.shipCost), options => options.MapFrom(od => od.shipCost));

  }
}