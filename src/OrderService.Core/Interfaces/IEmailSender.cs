using System.Threading.Tasks;

namespace OrderService.Core.Interfaces;

public interface IEmailSender
{
  void SendEmail(string to, string subject, string body);
}
