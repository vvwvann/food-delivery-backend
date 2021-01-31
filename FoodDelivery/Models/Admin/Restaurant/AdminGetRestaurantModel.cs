using FoodDelivery.Data.Tables;
using FoodDelivery.Models.Admin.Orders;
using System;
using System.Collections.Generic;

namespace FoodDelivery.Models.Admin.Restaurant
{
  public class AdminGetRestaurantModel
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string[] Photo { get; set; }

    public string Delivery { get; set; }

    public string Category { get; set; }

    public bool FreeDelivery { get; set; }

    public bool IsNew { get; set; }

    public bool IsPopular { get; set; }

    public string Description { get; set; }

    public DateTime ScheduleFrom { get; set; }

    public DateTime ScheduleTo { get; set; }

    public AddressEntity Address { get; set; }

    public int[] Сuisines { get; set; }

    public AdminManagerModel[] Managers { get; set; }

    public int[] DeliveryTypes { get; set; }

  }
}
