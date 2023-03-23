using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.IpnEndpoints;

public class IpnCallback : EndpointBaseAsync
  .WithRequest<IpnCallbackRequest>
  .WithActionResult<IpnCallbackResponse>
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
  public override async Task<ActionResult<IpnCallbackResponse>> HandleAsync([FromQuery] IpnCallbackRequest request, CancellationToken cancellationToken = default)
  {

    if (request.vnp_ResponseCode != "00") {
      return Ok(); //Dont process if response code is not successfully
    }


    string[] splitedOrderInfo = request.vnp_OrderInfo.Split("_"); 
    if (splitedOrderInfo.Length < 2)
    {
      return BadRequest("Order info doesnt fit");
    }

    string paymentTurn = splitedOrderInfo[0];

    int orderId = int.Parse(splitedOrderInfo[1]);

    var payment = await _orderPaymentService.AddNewPayment(orderId, paymentTurn, request.vnp_Amount, request.vnp_TxnRef, request.vnp_PayDate);

    if (payment.Errors.Any())
    {
      return Ok(new IpnCallbackResponse("00", "Order already confirmed"));
    }
    if (payment == null)
    {
      return Ok(new IpnCallbackResponse("01", "Order not found"));
    }

    return Ok(new IpnCallbackResponse("00", "Confirm success"));
  }
}
