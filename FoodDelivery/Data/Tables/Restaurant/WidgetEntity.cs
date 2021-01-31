using Newtonsoft.Json;
using System.Collections.Generic;

namespace FoodDelivery.Data.Tables
{
  public class WidgetEntity
  {
    public int Id { get; set; }

    public string Name { get; set; }

    [JsonIgnore]
    public List<WidgetRestaurantEntity> Restaurants { get; set; }
  }
}
