using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.IpnEndpoints;

public class IpnCallback : EndpointBaseAsync
  .WithRequest<IpnCallbackRequest>
  .WithActionResult
{

  private readonly IOrderPaymentService _orderPaymentService;

  public IpnCallback(IOrderPaymentService orderPaymentService)
  {
    _orderPaymentService = orderPaymentService;
  }

  [HttpGet(IpnCallbackRequest.Route)]
  [SwaggerOperation(
    Summary = "Ipn callback Endpoint",
    Description = "Ipn callback Endpoint",
    OperationId = "Ipn.Callback",
    Tags = new[] { "IpnEndpoints" })
  ]
  public override async Task<ActionResult> HandleAsync([FromQuery] IpnCallbackRequest request, CancellationToken cancellationToken = default)
  {

    if (request.vnp_ResponseCode != "00") {
      return Ok(); //Dont process if response code is not successfully
    }


    string[] splitedOrderInfo = request.vnp_OrderInfo.Split("_");

    string paymentTurn = splitedOrderInfo[0];
    int orderId = int.Parse(splitedOrderInfo[1]);

    var payment = await _orderPaymentService.AddNewPayment(orderId, paymentTurn, request.vnp_Amount, request.vnp_TxnRef, request.vnp_PayDate);

    if (payment.Errors.Any())
    {
      return BadRequest(payment.Errors);
    }
    if (payment == null)
    {
      return StatusCode(500, "Cannot add new payment, please contact developer");
    }

    return Ok();
  }
}
