using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Data.Tables
{
  public class WidgetRestaurantEntity
  {
    public int WidgetId { get; set; }

    public int RestaurantId { get; set; }

    public WidgetEntity Widget { get; set; }

    public RestaurantEntity Restaurant { get; set; }
  }
}
