using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static FoodDelivery.Models.Restaurants.GetAllRestaurantsResponseModel;

namespace FoodDelivery.Models.Favorites
{
  public class GetFavoriteRestaurantsModel
  {
    public int Total { get; set; }

    public List<RestaurantListItem> Items { get; set; }
  }
}
