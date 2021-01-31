using System.Collections.Generic;
using static FoodDelivery.Models.Restaurants.GetAllRestaurantsResponseModel;

namespace FoodDelivery.Models
{
  public partial class IndexResponseModel
  {
    public class WidgetItem
    {
      public int Id { get; set; }

      public string Name { get; set; }

      public List<RestaurantListItem> Restaurants { get; set; }
    }

  }
}
