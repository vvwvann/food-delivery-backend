using System;

namespace FoodDelivery.Models.Admin.Restaurant
{
  public class AdminRestaurantItem
  {
    public int Id { get; set; }

    public string Photo { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsBlock { get; set; }

    // тип кухни
  }
}
