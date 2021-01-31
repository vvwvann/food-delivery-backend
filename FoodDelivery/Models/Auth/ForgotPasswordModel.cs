using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.Auth
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
