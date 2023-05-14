﻿namespace OrderService.Web.SignalR;

public static class SignalRStartup
{
  public static void MapSignalR(this WebApplication app)
  {
    app.MapHub<ProductFetchingHub>("/hub");
  } 

  public static void AddSignal(this IServiceCollection services)
  {
    services.AddSignalR();
  }
}
