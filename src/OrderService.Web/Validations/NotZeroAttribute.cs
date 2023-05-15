using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Validations;

public class NotZeroAttribute : ValidationAttribute
{
  public override bool IsValid(object? value)
  {
    return (float)value! <= 0;
  }
}
