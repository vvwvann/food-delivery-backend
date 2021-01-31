using FoodDelivery.Data.Tables.User;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FoodDelivery.Data.Tables
{
  public class ApplicationUser : IdentityUser
  {
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string Name { get; set; }

    public string Photo { get; set; }

    public int? ManagedRestaurantId { get; set; }

    public bool IsBlock { get; set; }


    [JsonIgnore]
    public List<AddressEntity> Addresses { get; set; }

    [JsonIgnore]
    public List<FavoriteRestaurantEntity> FavoriteRestaurants { get; set; }

    [JsonIgnore]
    public List<FavoriteDishEntity> FavoriteDishes { get; set; }

    [JsonIgnore]
    public List<BasketDishEntity> BasketDishes { get; set; }

    [JsonIgnore]
    public List<OrderEntity> Orders { get; set; }

    [JsonIgnore]
    public List<OrderEntity> ManagedOrders { get; set; }

    [JsonIgnore]
    public RestaurantEntity ManagedRestaurant { get; set; }

  }
}
