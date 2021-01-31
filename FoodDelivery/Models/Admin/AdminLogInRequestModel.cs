using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.Auth
{
  public class AdminLogInRequestModel
  {
    [Required]
    [MaxLength(40)]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [MaxLength(40)]
    public string Password { get; set; }
  }
}
