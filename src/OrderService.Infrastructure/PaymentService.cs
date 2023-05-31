using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Ardalis.Result;
using Microsoft.Extensions.Configuration;
using OrderService.Core.Interfaces;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Infrastructure;
public class PaymentService : IPaymentService
{

  private readonly IRepository<Order> _orderRepository;
  private readonly IConfiguration _configuration;
  public PaymentService(IConfiguration configuration, IRepository<Order> orderRepository)
  {
    _configuration = configuration;
    _orderRepository = orderRepository;
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

  public async Task<Result<string>> GeneratePaymentUrl(int orderId, string hostname)
  {

    var orderSpec = new OrderByIdSpec(orderId);
    var order = await _orderRepository.FirstOrDefaultAsync(orderSpec);
    if (order == null)
    {
      return Result.Error("Order is null");
    }


    string? hashKey = _configuration["VnPay:HashSecret"];
    string? TmnCode = _configuration["VnPay:TmnCode"];
    string? payUrl = _configuration["VnPay:Url"];

    if (string.IsNullOrEmpty(hashKey) || string.IsNullOrEmpty(payUrl) || string.IsNullOrEmpty(TmnCode)) {
      return Result.Error("Vnpay config is null");
    }

    double amount = 0.0f;
    bool isFirstPayment = false;

    var currentOrderPaymentStatus = order.orderPayments.Where(od => od.paymentStatus == PaymentStatus.SecondPayment).FirstOrDefault();
    if (currentOrderPaymentStatus != null) {
      return Result.Error("already paid");
    }

    currentOrderPaymentStatus = order.orderPayments.Where(od => od.paymentStatus == PaymentStatus.firstPayment).FirstOrDefault();

    

    if (currentOrderPaymentStatus == null) {
      amount = order.GetFirstPaymentAmount();
      isFirstPayment = true;
    }
    else //second payment
    {
      amount = order.GetSecondPaymentAmount();
      isFirstPayment = false;
    }

    amount *= 100000; //add nghin` VND vao price * 100 (eliminate , )

    long roundAmount = (long)amount;

    string paymentTurn = (isFirstPayment) ? PaymentStatus.firstPayment.Name : PaymentStatus.SecondPayment.Name; //determine if this is the first or the second payment

    
    string encodedCallback = WebUtility.UrlEncode($"{hostname}/count-redirect-payment");

    string query = $"vnp_Amount={roundAmount}&vnp_BankCode=VNBANK&vnp_Command=pay&vnp_CreateDate={DateTime.Now.ToString("yyyyMMddHHmmss")}&vnp_CurrCode=VND&vnp_IpAddr=127.0.0.1&vnp_Locale=vn&vnp_OrderInfo={paymentTurn}_{order.Id}&vnp_OrderType=other&vnp_ReturnUrl={encodedCallback!}&vnp_TmnCode={TmnCode}&vnp_TxnRef={order.Id}{DateTime.Now.Ticks}&vnp_Version=2.1.0";

    string hashSecure = HmacSHA512(hashKey, query);

    query += $"&vnp_SecureHash=" + hashSecure;


    return new Result<string>($"{payUrl}?{query}");
  }
}
