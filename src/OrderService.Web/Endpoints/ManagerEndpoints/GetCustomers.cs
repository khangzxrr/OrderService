using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.Core.UserAggregate;
using OrderService.Core.UserAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Endpoints.Records;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class GetCustomers : EndpointBaseAsync
  .WithRequest<GetCustomersRequest>
  .WithActionResult<GetCustomersResponse>
{

  private readonly IRepository<Order> _orderRepository;
  private readonly IRepository<User> _userRepository; 

  public GetCustomers(IRepository<User> userRepository, IRepository<Order> orderRepository)
  {
    _userRepository = userRepository;
    _orderRepository = orderRepository;
  }

  [HttpGet(GetCustomersRequest.Route)]
  [SwaggerOperation(
    Summary = "Get customers list",
    Description = "Get customers list",
    OperationId = "Customer.all",
    Tags = new[] { "ManagerEndpoints" })
  ]
  [Authorize(Roles = "MANAGER")]
  public override async Task<ActionResult<GetCustomersResponse>> HandleAsync([FromQuery] GetCustomersRequest request, CancellationToken cancellationToken = default)
  {
    var spec = new UserPaginatedSpec(request.skip, request.take, "CUSTOMER");
    var totalSpec = new UserPaginatedSpec(request.totalSkip, request.totalTake, "CUSTOMER");

    var totalCount = await _userRepository.CountAsync(totalSpec);
    var users = await _userRepository.ListAsync(spec);

    var customerRecords =  users.Select(async user =>
    {

      //===== count orders  and calculate total payments
      var orderWithPaymentsSpec = new OrderWithPaymentsByUserIdSpec(user.Id);
      var orders = await _orderRepository.ListAsync(orderWithPaymentsSpec);

      var totalPaymentAmount = orders.Sum(o => o.GetTotalPaymentsAmount());
      var totalOrders = orders.Count;
      //================

      var customerRecord = CustomerRecord.FromEntity(user, totalOrders, totalPaymentAmount);

      return customerRecord;
    })
    .Select(task => task.Result);


    var response = new GetCustomersResponse(totalCount, request.pageSize, customerRecords);

    return Ok(response);
  }
}
