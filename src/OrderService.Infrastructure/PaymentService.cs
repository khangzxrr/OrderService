using System.Security.Cryptography;
using System.Text;
using Ardalis.Result;
using Microsoft.Extensions.Configuration;
using OrderService.Core.Interfaces;
using OrderService.Core.OrderAggregate;

namespace OrderService.Infrastructure;
public class PaymentService : IPaymentService
{

  private readonly IConfiguration _configuration;
  public PaymentService(IConfiguration configuration)
  {
    _configuration = configuration;
  }

  //

  public static String HmacSHA512(string key, String inputData)
  {
    var hash = new StringBuilder();
    byte[] keyBytes = Encoding.UTF8.GetBytes(key);
    byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);
    using (var hmac = new HMACSHA512(keyBytes))
    {
      byte[] hashValue = hmac.ComputeHash(inputBytes);
      foreach (var theByte in hashValue)
      {
        hash.Append(theByte.ToString("x2"));
      }
    }

    return hash.ToString();
  }

  public Result<string> GeneratePaymentUrl(Order order)
  {
    string hashKey = _configuration["VnPay:HashSecret"]!;
    string TmnCode = _configuration["VnPay:TmnCode"]!;
    string payUrl = _configuration["VnPay:Url"]!;


    double amount = 0.0f;
    if (order.status == OrderStatus.noPayYet)
    {
      amount = 80.0f * order.price / 100.0f;
    } 
    else
    if (order.status == OrderStatus.deliverToCustomer)
    {
      amount = 20.0f * order.price / 100.0f;
    }
    else
    {
      return Result<string>.Error("Not correct time to pay");
    }

    amount *= 25000;
    amount *= 100;

    long roundAmount = (long)amount;



    string query = $"vnp_Amount={roundAmount}&vnp_BankCode=VNBANK&vnp_Command=pay&vnp_CreateDate={DateTime.Now.ToString("yyyyMMddHHmmss")}&vnp_CurrCode=VND&vnp_IpAddr=127.0.0.1&vnp_Locale=vn&vnp_OrderInfo=Thanhtoandonhang{order.Id}&vnp_OrderType=other&vnp_ReturnUrl=http%3A%2F%2Flocalhost%3A3000%2Fcallback&vnp_TmnCode={TmnCode}&vnp_TxnRef={DateTime.Now.Ticks}&vnp_Version=2.1.0";

    string hashSecure = HmacSHA512(hashKey, query);

    query += $"&vnp_SecureHash=" + hashSecure;


    return $"{payUrl}?{query}";
  }
}
