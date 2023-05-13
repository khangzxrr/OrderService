using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.OrderPaymentAggregate;
using OrderService.Core.OrderPaymentAggregate.specifications;
using OrderService.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class GetTotalPayment : EndpointBaseAsync
  .WithRequest<GetTotalPaymentRequest>
  .WithActionResult<GetTotalPaymentResponse>
{

  private readonly IRepository<OrderPayment> _orderPaymentRepository;

  public GetTotalPayment(IRepository<OrderPayment> orderPaymentRepository)
  {
    _orderPaymentRepository = orderPaymentRepository;
  }

  [HttpGet(GetTotalPaymentRequest.Route)]
  [SwaggerOperation(
    Summary = "Get total payment cost",
    Description = "Get total payment cost",
    OperationId = "Payments.getTotal",
    Tags = new[] { "ManagerEndpoints" })
  ]
  [Authorize(Roles = "MANAGER")]
  public override async Task<ActionResult<GetTotalPaymentResponse>> HandleAsync([FromQuery] GetTotalPaymentRequest request, CancellationToken cancellationToken = default)
  {



    var spec = new OrderPaymentByDateRangeSpec(request.startDate, request.endDate);

    var payments = await _orderPaymentRepository.ListAsync(spec);

    double totalCost = 0;

    foreach (var payment in payments)
    {
      totalCost += payment.paymentCost;
    }

    var response = new GetTotalPaymentResponse(totalCost);

    return Ok(response);
  }
}
