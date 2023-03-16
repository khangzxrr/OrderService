using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.IpnEndpoints;

public class IpnCallbackRequest
{
  public const string Route = "/ipn/callback";

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  [Required]
  public string vnp_TmnCode { get; set; }

  [Required]
  public long vnp_Amount { get; set; }

  [Required]
  public string vnp_BankCode { get; set; }

  [Required]
  public string vnp_BankTranNo { get; set; }

  public string? vnp_CardType { get; set; }
  [Required]
  public string vnp_PayDate { get; set; }
  [Required]
  public string vnp_OrderInfo { get; set; }
  [Required]
  public string vnp_TransactionNo { get; set; }
  [Required]

  public string vnp_ResponseCode { get; set; }

  public string? vnp_TransactionStatus { get; set; }
  [Required]
  public string vnp_TxnRef { get; set; }
  [Required]
  public string vnp_SecureHash { get; set; }


}
