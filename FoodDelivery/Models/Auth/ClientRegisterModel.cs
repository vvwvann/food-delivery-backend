using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.Auth
{
  public class RegisterModel
  {
    [Required]
    [MaxLength(40)]
    public string Email { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The password must be at least 6 and at max 40 characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [MaxLength(40)]
    public string Password { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The password must be at least 6 and at max 40 characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [MaxLength(40)]
    public string ConfirmPassword { get; set; }

    public string PlayerId { get; set; }
  }
}
