using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.Interfaces;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.Core.ShipperAggregate.enums;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.ShipperEndpoints;

public class PayRemainCost : EndpointBaseAsync
  .WithRequest<PayRemainCostRequest>
  .WithActionResult<PayRemainCostResponse>
{

  private readonly IRepository<Order> _orderRepository;
  private readonly IOrderPaymentService _orderPaymentService;

  public PayRemainCost(IOrderPaymentService orderPaymentService, IRepository<Order> orderRepository)
  { 
    _orderPaymentService = orderPaymentService;
    _orderRepository = orderRepository;
  }

  [Authorize(Roles = "SHIPPER")]
  [HttpPost(PayRemainCostRequest.Route)]
  [SwaggerOperation(
    Summary = "Check and confirm payment for remain cost",
    Description = "Check and confirm payment for remain cost",
    OperationId = "Shipper.confirmRemainCostPayment",
    Tags = new[] { "ShipperEndpoints" })
  ]
  public override async Task<ActionResult<PayRemainCostResponse>> HandleAsync(PayRemainCostRequest request, CancellationToken cancellationToken = default)
  {

    ShipperPayingMethod shipperPayingMethod;
    var isSuccessParsedMethod = ShipperPayingMethod.TryFromName(request.payMethod, out shipperPayingMethod);   

    if (!isSuccessParsedMethod)
    {
      return BadRequest("Paying method is not found");
    }

    var orderSpec = new OrderPaymentByIdSpec(request.orderId);

    var order = await _orderRepository.FirstOrDefaultAsync(orderSpec);

    if (order == null)
    {
      return BadRequest("Order is not found");
    }

    if (!order.IsPaidFirstMilestone())
    {
      return BadRequest("User is not pay first milestone yet!");
    }

    if (shipperPayingMethod == ShipperPayingMethod.byCash)
    {
      var transactionId = $"shipper_cash_{DateTime.UtcNow}";
      var paymentCost = OrderPayment.ConvertDollarToVnPayVND(order.remainCost);

      var addPaymentResults = await _orderPaymentService.AddNewPayment(request.orderId, PaymentStatus.SecondPayment.Name, paymentCost, transactionId);

      if (addPaymentResults.Errors.Any())
      {
        return BadRequest(addPaymentResults);
      }

    } else
    {
      //PAY online

      if (!order.IsPaidAllMilestone())
      {
        return BadRequest("USER_NOT_PAY_ALL_YET");
      }
    
    }

    order.SetQueueInShipping(OrderLocalShippingStatus.delivered);
    order.SetStatus(OrderStatus.finished);

    //todo:
    //payment method is cash => update order payment table, process to finish
    //payment method is online => check payment is success => proces to finish
    //if not => return bad request 

    //problem: shipping cost, weight cost
    //user cannot pay if weight cost is not updated (!= 0)

    //3rd ship: employee will have to update shipping status to finish manually

    var response = new PayRemainCostResponse("SUCCESS");
    return Ok(response);
  }
}
