using System.Globalization;
using Ardalis.Result;
using MediatR;
using OrderService.Core.Interfaces;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Events;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.Core.OrderPaymentAggregate;
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

  public async Task<Result<OrderPayment>> AddNewPayment(int orderId, string paymentTurn, long amount, string transactionId)
  {
    if (paymentTurn != PaymentStatus.firstPayment.Name && paymentTurn != PaymentStatus.SecondPayment.Name)
    {
      return Result.Error("paymentTurn is not valid");
    }

    if (string.IsNullOrEmpty(transactionId))
    {
      return Result.Error("transactional Id cannot be null or empty");
    }


    var orderSpec = new OrderByIdSpec(orderId);
    var order = await _repository.FirstOrDefaultAsync(orderSpec);

    if (order == null)
    {
      return Result.Error("order is null");
    }

    var existTransactionalWithId = order.orderPayments.Where(p => p.transactionalId == transactionId).FirstOrDefault();

    //handle case IPN callback multiple time with the same transactional ID (1 transaction)
    if (existTransactionalWithId != null) { 
      return new Result<OrderPayment>(existTransactionalWithId);
    }

    double dbAmount;
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

    VNPAYmustPayAmount = OrderPayment.ConvertVNDToVNPayVND(dbAmount); //convert to VNPAY amount


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
      DateTime.Now);

    order.AddPayment(payment);

    await _repository.SaveChangesAsync();

    var orderPaymentCreatedEvent = new OrderPaymentCreatedEvent(orderId, payment.Id);
    await _mediator.Publish(orderPaymentCreatedEvent);

    return new Result<OrderPayment>(payment);
  }
}
