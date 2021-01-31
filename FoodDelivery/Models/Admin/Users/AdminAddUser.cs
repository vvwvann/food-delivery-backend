namespace FoodDelivery.Models.Admin.Users
{
  public class AdminAddUser : AdminUser
  {
    public string Password { get; set; }

    public string ConfirmPassword { get; set; }

    public int? RestaurantId { get; set; }
  }
}
