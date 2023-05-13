using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using OrderService.Core.OrderPaymentAggregate;
using OrderService.Core.OrderPaymentAggregate.specifications;
using OrderService.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class GetPayments : EndpointBaseAsync
  .WithRequest<GetPaymentsRequest>
  .WithActionResult<GetPaymentsResponse>
{


  private readonly IRepository<OrderPayment> _orderPaymentRepository;

  public GetPayments(IRepository<OrderPayment> orderPaymentRepository)
  {
    _orderPaymentRepository = orderPaymentRepository;
  }

  [HttpGet(GetPaymentsRequest.Route)]
  [SwaggerOperation(
    Summary = "Get payments list",
    Description = "Get payments list",
    OperationId = "Payments.get",
    Tags = new[] { "ManagerEndpoints" })
  ]
  [Authorize(Roles = "MANAGER")]
  public override async Task<ActionResult<GetPaymentsResponse>> HandleAsync([FromQuery] GetPaymentsRequest request, CancellationToken cancellationToken = default)
  {
    var spec = new OrderPaymentFilterPaginatedSpec(
      skip: request.pageIndex * request.pageSize,
      take: request.pageSize,
      startDate: request.startDate,
      endDate: request.endDate
      );

    var payments = await _orderPaymentRepository.ListAsync(spec);
    var totalPayments = await _orderPaymentRepository.CountAsync(spec);

    double totalPaymentsCost = 0.0f;
    List<PaymentRecord> paymentRecords = new List<PaymentRecord>();

    foreach( OrderPayment payment in payments )
    {
      totalPaymentsCost += payment.paymentCost;
      paymentRecords.Add(PaymentRecord.FromEntity(payment));
    }

    int pageCount;

    if (request.pageSize > 0)
    {
      pageCount = int.Parse(Math.Ceiling((decimal)totalPayments / request.pageSize).ToString());
    } else
    {
      pageCount = totalPayments > 0 ? 1 : 0;
    }

    var response = new GetPaymentsResponse(paymentRecords, pageCount, totalPaymentsCost);


    return Ok(response);
  }
}
