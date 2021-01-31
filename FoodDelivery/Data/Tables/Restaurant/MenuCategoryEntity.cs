using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FoodDelivery.Data.Tables
{
  public class MenuCategoryEntity
  {
    public int Id { get; set; }

    public string Name { get; set; }

    [JsonIgnore]
    public int RestaurantId { get; set; }

    [JsonIgnore]
    public RestaurantEntity Restaurant { get; set; }

    public List<DishEntity> Dishes { get; set; }
  }
}
