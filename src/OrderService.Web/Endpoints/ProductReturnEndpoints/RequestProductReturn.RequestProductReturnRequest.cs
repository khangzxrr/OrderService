
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace OrderService.Web.Endpoints.ProductReturnEndpoints;

public class RequestProductReturnRequest
{
  public const string Route = "/productReturn/request";

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


  [Required]
  public string[] medias { get; set; }

  [Required]
  public bool isWarranty { get; set; }

  [Required]
  public int orderDetailId { get; set; }

  public string? series { get; set; }
  public string? description { get; set; }



}
