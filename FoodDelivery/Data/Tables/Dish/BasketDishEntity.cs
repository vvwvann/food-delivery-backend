using FoodDelivery.Models.Order;
using System.Collections.Generic;

namespace FoodDelivery.Data.Tables
{
  public class BasketDishEntity
  {
    public int Count { get; set; }

   // public List<string> Description { get; set; } = new List<string>();

    public int DishId { get; set; }

    public string UserId { get; set; }

    public int RestaurantId { get; set; }

    public DishEntity Dish { get; set; }

    public ApplicationUser User { get; set; }

    public RestaurantEntity Restaurant { get; set; }

    public BasketDishEntity(string userId, int dishId, int restaurantId)
    {
      DishId = dishId;
      UserId = userId;
      RestaurantId = restaurantId;
    }

    public static implicit operator BasketResponseItem(BasketDishEntity item) => new BasketResponseItem {
      Price = item.Dish.Price,
      Count = item.Count,
      Id = item.DishId,
      Photo = item.Dish.Photo?[0]
    };
  }
}
