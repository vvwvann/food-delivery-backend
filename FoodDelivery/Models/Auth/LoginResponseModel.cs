using FoodDelivery.Data.Tables;
using System;

namespace FoodDelivery.Models.Auth
{
  public class LoginResponseModel
  {
    public string AccessToken { get; set; }

    public Guid RefreshToken { get; set; }

    public long ExpiresIn { get; set; }

    public UserAuthModel User { get; set; }

    public LoginResponseModel(ApplicationUser user, string token, Guid refresh, long expiresIn, string role)
    {
      AccessToken = token;
      RefreshToken = refresh;
      ExpiresIn = expiresIn;
      User = new UserAuthModel {
        Email = user.Email,
        Id = user.Id,
        Role = role
      };
    }
  }

  public class AdminLoginResponseModel : LoginResponseModel
  {
    public AdminLoginResponseModel(ApplicationUser user, string token, Guid refresh, long expiresIn, string role, int? restaurantId)
      : base(user, token, refresh, expiresIn, role)
    {
      RestaurantId = restaurantId;
    }

    public int? RestaurantId { get; set; }
  }
}
