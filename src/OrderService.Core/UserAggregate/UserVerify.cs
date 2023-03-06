using System;
using Ardalis.SmartEnum;
using OrderService.Core.ProjectAggregate;

namespace OrderService.Core.UserAggregate
{
  public class UserVerify : SmartEnum<UserVerify>
  {
    public static readonly UserVerify actived = new(nameof(actived),1);
    public static readonly UserVerify disabled = new(nameof(disabled), 0);

    protected UserVerify(string name, int value) : base(name, value) { }

  }
}

