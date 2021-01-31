using System;
using System.Collections.Generic;

namespace FoodDelivery.Models.Admin.Restaurant
{
  public class AdminRestaurantModel
  {
    public string Name { get; set; }

    public string[] Photo { get; set; }

    public string Delivery { get; set; }

    public string Category { get; set; }

    public string Description { get; set; }

    public DateTime ScheduleFrom { get; set; }

    public DateTime ScheduleTo { get; set; }

    public int AddressId { get; set; }

    public bool IsNew { get; set; }

    public bool IsPopular { get; set; }

    public bool FreeDelivery { get; set; }

    public List<string> Managers { get; set; }

    public List<int> Cuisines { get; set; }

    public List<int> DeliveryTypes { get; set; }
  }
}
