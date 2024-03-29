﻿using Autofac;
using OrderService.Core.Interfaces;
using OrderService.Core.Services;

namespace OrderService.Core;

public class DefaultCoreModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {

    builder.RegisterType<GetMostFreeEmployeeService>()
        .As<IGetMostFreeEmployeeService>().InstancePerLifetimeScope();

    builder.RegisterType<CreateOrderService>()
        .As<ICreateOrderService>().InstancePerLifetimeScope();

    builder.RegisterType<AddOrderDetailService>()
        .As<IAddOrderDetailService>().InstancePerLifetimeScope();

    builder.RegisterType<OrderPaymentService>()
        .As<IOrderPaymentService>().InstancePerLifetimeScope();

    builder.RegisterType<AuthenticationService>()
        .As<IAuthenticationService>().InstancePerLifetimeScope();

    builder.RegisterType<DetermineNextStateProductIssueService>()
        .As<IDetermineNextStateProductIssueService>().InstancePerLifetimeScope();
  }
}
