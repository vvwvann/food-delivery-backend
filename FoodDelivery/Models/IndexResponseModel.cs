using FoodDelivery.Data.Tables;
using System.Collections.Generic;
using static FoodDelivery.Models.Restaurants.GetAllRestaurantsResponseModel;

namespace FoodDelivery.Models
{
  public partial class IndexResponseModel
  {
    public List<HomePromotionEntity> Promotions { get; set; }

    public List<RestaurantListItem> Popular { get; set; }

    public List<WidgetItem> Widgets { get; set; }
  }
}
