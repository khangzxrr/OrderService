using Ardalis.GuardClauses;
using OrderService.SharedKernel;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.ChatAggregate;
public class ChatMessage : EntityBase
{
  public bool isFromEmployee { get; set; }
  public string message { get; set; }

  public ChatMessage(bool isFromEmployee, string message)
  {
    this.isFromEmployee = isFromEmployee;
    this.message = Guard.Against.NullOrEmpty(message);
  }
}
