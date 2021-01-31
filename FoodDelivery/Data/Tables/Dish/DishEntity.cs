using FoodDelivery.Models.Favorites;
using FoodDelivery.Models.Order;
using Newtonsoft.Json;
using System.Collections.Generic;
using static FoodDelivery.Models.Favorites.GetFavoriteDishesModel;

namespace FoodDelivery.Data.Tables
{
  public class DishEntity
  {
    public int Id { get; set; }

    public bool IsBlock { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public string[] Photo { get; set; }

    [JsonIgnore]
    public int Minutes { get; set; }

    [JsonIgnore]
    public int MenuCategoryId { get; set; }

    [JsonIgnore]
    public int RestaurantId { get; set; }

    [JsonIgnore]
    public string Description { get; set; } 

    [JsonIgnore]
    public string Ingridients { get; set; }

    [JsonIgnore]
    public int Index { get; set; }

    [JsonIgnore]
    public bool IsAdditional { get; set; }

    [JsonIgnore]
    public int? CuisineId { get; set; }

    [JsonIgnore]
    public CuisineEntity Cuisine { get; set; }

    [JsonIgnore]
    public MenuCategoryEntity MenuCategory { get; set; }

    [JsonIgnore]
    public List<DishAdditionalEntity> Additionals { get; set; }

    [JsonIgnore]
    public List<DishAdditionalEntity> Dishes { get; set; }

    [JsonIgnore]
    public List<OrderDishEntity> Orders { get; set; }

    [JsonIgnore]
    public List<FavoriteDishEntity> Favorites { get; set; }

    [JsonIgnore]
    public List<BasketDishEntity> Baskets { get; set; }

    [JsonIgnore]
    public RestaurantEntity Restaurant { get; set; }

    [JsonIgnore]
    public List<PromotionDishEntity> Promotions { get; set; }

    public static implicit operator DishListItem(DishEntity dish)
    {
      return new DishListItem {
        Id = dish.Id,
        Name = dish.Name,
        Photo = dish.Photo?[0],
        Price = dish.Price
      };
    }
  }
}
