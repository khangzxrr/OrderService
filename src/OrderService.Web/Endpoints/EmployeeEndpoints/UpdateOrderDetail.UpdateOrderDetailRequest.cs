using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class UpdateOrderDetailRequest
{
  public const string Route = "/employee/orders";



  [Required]
  public int orderId { get; set; }

  [Required]
  public int orderDetailId { get; set; }

  public bool? disable { get; set; } = null;

  public string? productDescription { get; set; }

  public int? quantity { get; set; } = null; //set value to null prevent 0

  public float? shipCost { get; set; } = null;

  public float? productCost { get; set; } = null;

  public float? processCost { get; set; } = null;
  
  public float? additionalCost { get; set; } = null;

  public float? costPerWeight { get; set; } = null;

  public bool? productWarrantable { get; set; } = null;

  public string? warrantyDescription { get; set; }

  public bool? productReturnable { get; set; } = null;

  public string? returnDescription { get; set; }

}
