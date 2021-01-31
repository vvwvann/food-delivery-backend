using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Data.Tables
{
  public class FavoriteRestaurantEntity
  {
    public string UserId { get; set; }

    public int RestaurantId { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;

    public RestaurantEntity Restaurant { get; set; }

    public ApplicationUser User { get; set; }

    public FavoriteRestaurantEntity(string userId, int restaurantId)
    {
      RestaurantId = restaurantId;
      UserId = userId;
    }
  }

}
