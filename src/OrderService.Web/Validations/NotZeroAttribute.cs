using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Validations;

public class NotZeroAttribute : ValidationAttribute
{
  public override bool IsValid(object? value)
  {
    return (double)value! <= 0;
  }
}
