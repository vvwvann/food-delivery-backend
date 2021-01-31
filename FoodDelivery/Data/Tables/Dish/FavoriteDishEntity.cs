using System;

namespace FoodDelivery.Data.Tables
{
  public class FavoriteDishEntity
  {
    public string UserId { get; set; }

    public int DishId { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DishEntity Dish { get; set; }

    public ApplicationUser User { get; set; }

    public FavoriteDishEntity(string userId, int dishId)
    {
      DishId = dishId;
      UserId = userId;
    }
  }

}
