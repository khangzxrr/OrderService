using System.Threading.Tasks;

namespace OrderService.Core.Interfaces;

public interface IEmailSender
{
  Task SendEmailAsync(string to, string subject, string body);
}
