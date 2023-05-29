using Ardalis.SmartEnum;

namespace OrderService.Core.OrderAggregate;
public class PaymentStatus : SmartEnum<PaymentStatus>
{
  public static readonly PaymentStatus firstPayment = new(nameof(firstPayment), 0);
  public static readonly PaymentStatus SecondPayment = new(nameof(SecondPayment), 1);

  public PaymentStatus(string name, int value): base(name, value) { }
}
