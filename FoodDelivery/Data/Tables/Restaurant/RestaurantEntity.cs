using FoodDelivery.Models.Admin.Restaurant;
using FoodDelivery.Models.Reviews;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace FoodDelivery.Data.Tables
{
  public class RestaurantEntity
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string Category { get; set; }

    public string Description { get; set; }

    public string[] Photo { get; set; }

    public double Rating { get; set; }

    public string Delivery { get; set; }

    public bool IsBlock { get; set; }

    public DateTime ScheduleFrom { get; set; }

    public DateTime ScheduleTo { get; set; }

    public bool FreeDelivery { get; set; }

    public bool IsNew { get; set; }

    public bool IsPopular { get; set; }

    [JsonIgnore]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [JsonIgnore]
    public int PriceFrom { get; set; }

    [JsonIgnore]
    public int PriceTo { get; set; }

    public int AddressId { get; set; }

    [JsonIgnore]
    public List<MenuCategoryEntity> Menu { get; set; }

    [JsonIgnore]
    public List<RestaurantEntity> Reviews { get; set; }

    [JsonIgnore]
    public AddressEntity Address { get; set; }

    [JsonIgnore]
    public List<RestaurantСuisineEntity> Cuisines { get; set; }

    [JsonIgnore]
    public List<FavoriteRestaurantEntity> Favorites { get; set; }

    [JsonIgnore]
    public List<WidgetRestaurantEntity> Widgets { get; set; }

    [JsonIgnore]
    public List<RestaurantDeliveryEntity> Deliveries { get; set; }

    [JsonIgnore]
    public List<PromotionRestaurantEntity> Promotions { get; set; }

    [JsonIgnore]
    public List<ApplicationUser> Managers { get; set; }

    [JsonIgnore]
    public List<DishEntity> Dishes { get; set; }

    public static implicit operator AdminRestaurantItem(RestaurantEntity item)
    {
      return new AdminRestaurantItem {
        Id = item.Id,
        IsBlock = item.IsBlock,
        CreatedAt = item.CreatedAt,
        Photo = item.Photo?[0]
      };
    }
  }
}
