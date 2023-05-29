using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.AdminEndpoints;

public class UpdateUserRequest
{
  public const string Route = "/admin/users";

  [Required]
  public int userId { get; set; }


}
