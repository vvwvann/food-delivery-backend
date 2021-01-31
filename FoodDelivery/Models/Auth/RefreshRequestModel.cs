using System;
using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.Auth
{
    public class RefreshRequestModel
    {
        [Required]
        public Guid RefreshToken { get; set; }
    }
}
