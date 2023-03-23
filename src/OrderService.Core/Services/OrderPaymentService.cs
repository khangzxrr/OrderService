using System.Globalization;
using Ardalis.Result;
using MediatR;
using OrderService.Core.Interfaces;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Events;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.Services;
public class OrderPaymentService : IOrderPaymentService
{

  private IRepository<Order> _repository;
  private IMediator _mediator;

  public OrderPaymentService(IRepository<Order> repository, IMediator mediator)
  {
    _repository = repository;
    _mediator = mediator;
  }

  public async Task<Result<OrderPayment>> AddNewPayment(int orderId, string paymentTurn, long amount, string transactionId, string createdAt)
  {
    if (paymentTurn != PaymentStatus.firstPayment.Name && paymentTurn != PaymentStatus.SecondPayment.Name)
    {
      return Result.Error("paymentTurn is not valid");
    }

    if (string.IsNullOrEmpty(transactionId))
    {
      return Result.Error("transactional Id cannot be null or empty");
    }

    if (string.IsNullOrEmpty(createdAt))
    {
      return Result.Error("createAt cannot be null or empty");
    }

    var createAtDateTime = DateTime.ParseExact(createdAt, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);


    var orderSpec = new OrderByIdSpec(orderId);
    var order = await _repository.FirstOrDefaultAsync(orderSpec);

    if (order == null)
    {
      return Result.Error("order is null");
    }

    float dbAmount;
    long VNPAYmustPayAmount;
    PaymentStatus paymentStatus;

    if (paymentTurn == PaymentStatus.firstPayment.Name)
    {
      dbAmount = order.GetFirstPaymentAmount();
      paymentStatus = PaymentStatus.firstPayment;
    }
    else
    {
      dbAmount = order.GetSecondPaymentAmount();
      paymentStatus = PaymentStatus.SecondPayment;
    }

    VNPAYmustPayAmount = (long)((double)dbAmount * (double)100000); //convert to VNPAY amount


    if (VNPAYmustPayAmount != amount)
    {
      return Result.Error($"payment amount is not valid require: {VNPAYmustPayAmount}");
    }
     
    var payment = new OrderPayment(
      paymentStatus,
      dbAmount
      , 
      transactionId, 
      $"order:{order.Id} left: {order.price - dbAmount}", 
      createAtDateTime);

    order.AddPayment(payment);

    await _repository.SaveChangesAsync();

    var orderPaymentCreatedEvent = new OrderPaymentCreatedEvent(orderId, payment.Id);
    await _mediator.Publish(orderPaymentCreatedEvent);

    return new Result<OrderPayment>(payment);
  }
}
