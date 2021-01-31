using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Data.Tables
{
  public class PromotionEntity
  {
    public int Id { get; set; }

    public string Name { get; set; }

    [JsonIgnore]
    public List<PromotionDishEntity> Dishes { get; set; }

    [JsonIgnore]
    public List<PromotionRestaurantEntity> Restaurants { get; set; }
  }
}
