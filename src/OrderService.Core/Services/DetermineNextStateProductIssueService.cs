
using Ardalis.Result;
using OrderService.Core.Interfaces;
using OrderService.Core.ProductReturnAggregate;

namespace OrderService.Core.Services;
public class DetermineNextStateProductIssueService : IDetermineNextStateProductIssueService
{
  private Result<IEnumerable<ProductIssueStatus>> DetermineForReturn(ProductIssueStatus productIssueStatus)
  {
    var nextStates = new List<ProductIssueStatus>();

    if (productIssueStatus == ProductIssueStatus.request)
    {
      nextStates.Add(ProductIssueStatus.acceptEmployeeFault);
      nextStates.Add(ProductIssueStatus.acceptCustomerFault);
      nextStates.Add(ProductIssueStatus.acceptSellerFault);
    }

    else if (productIssueStatus == ProductIssueStatus.acceptEmployeeFault ||
             productIssueStatus == ProductIssueStatus.acceptCustomerFault)
    {
      nextStates.Add(ProductIssueStatus.refund);
    }

    else if (productIssueStatus == ProductIssueStatus.acceptSellerFault)
    {
      nextStates.Add(ProductIssueStatus.exchangeforNew);
      nextStates.Add(ProductIssueStatus.refund);
    }

    else if (productIssueStatus == ProductIssueStatus.refund)
    {
      nextStates.Add(ProductIssueStatus.finish);
    }

    else if (productIssueStatus == ProductIssueStatus.exchangeforNew)
    {
      nextStates.Add(ProductIssueStatus.sentToSeller);
    }

    else if (productIssueStatus == ProductIssueStatus.sentToSeller)
    {
      nextStates.Add(ProductIssueStatus.successExchangeReturnToVN);
      nextStates.Add(ProductIssueStatus.failedExchangeSellerRejectReturnToVN);
    }

    else if (productIssueStatus == ProductIssueStatus.successExchangeReturnToVN)
    {
      nextStates.Add(ProductIssueStatus.shippingToCustomer);
    }

    else if (productIssueStatus == ProductIssueStatus.shippingToCustomer)
    {
      nextStates.Add(ProductIssueStatus.shipperReceived);
    }

    else if (productIssueStatus == ProductIssueStatus.shipperReceived)
    {
      nextStates.Add(ProductIssueStatus.customerReceived);
    }

    else if (productIssueStatus == ProductIssueStatus.customerReceived)
    {
      nextStates.Add(ProductIssueStatus.finish);
    }

    else if (productIssueStatus == ProductIssueStatus.failedExchangeSellerRejectReturnToVN)
    {
      nextStates.Add(ProductIssueStatus.refund);
      nextStates.Add(ProductIssueStatus.shippingToCustomer);
    }

    return new Result<IEnumerable<ProductIssueStatus>>(nextStates);
  }

  private Result<IEnumerable<ProductIssueStatus>> DetermineForWarranty(ProductIssueStatus productIssueStatus)
  {
    List<ProductIssueStatus> nextStates = new List<ProductIssueStatus>();


    if (productIssueStatus == ProductIssueStatus.request)
    {
      nextStates.Add(ProductIssueStatus.sentToSeller);
    }
    else if (productIssueStatus == ProductIssueStatus.sentToSeller)
    {
      nextStates.Add(ProductIssueStatus.successExchangeReturnToVN);
      nextStates.Add(ProductIssueStatus.failedExchangeSellerRejectReturnToVN);
    }
    else if (productIssueStatus == ProductIssueStatus.successExchangeReturnToVN)
    {
      nextStates.Add(ProductIssueStatus.shippingToCustomer);
    }

    else if (productIssueStatus == ProductIssueStatus.shippingToCustomer)
    {
      nextStates.Add(ProductIssueStatus.shipperReceived);
    }

    else if (productIssueStatus == ProductIssueStatus.shipperReceived)
    {
      nextStates.Add(ProductIssueStatus.customerReceived);
    }

    else if (productIssueStatus == ProductIssueStatus.customerReceived)
    {
      nextStates.Add(ProductIssueStatus.finish);
    }

    else if (productIssueStatus == ProductIssueStatus.failedExchangeSellerRejectReturnToVN)
    {
      nextStates.Add(ProductIssueStatus.shippingToCustomer);
    }

    else if (productIssueStatus == ProductIssueStatus.shipperReceived)
    {
      nextStates.Add(ProductIssueStatus.customerReceived);
    }

    else if (productIssueStatus == ProductIssueStatus.customerReceived)
    {
      nextStates.Add(ProductIssueStatus.finish);
    }


    return new Result<IEnumerable<ProductIssueStatus>>(nextStates);
  }

  public Result<IEnumerable<ProductIssueStatus>> getNextStatesOf(ProductIssue productIssue)
  {
    if (productIssue.isWarranty)
    {
      return DetermineForWarranty(productIssue.status);
    }

    return DetermineForReturn(productIssue.status);
  }
}
