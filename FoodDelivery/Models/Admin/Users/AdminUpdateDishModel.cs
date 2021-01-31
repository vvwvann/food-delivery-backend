using FoodDelivery.Models.Admin.Restaurant;
using System.Collections.Generic;

namespace FoodDelivery.Models.Admin.Users
{
  public class AdminUpdateDishModel : AdminDishModel
  {
    public List<int> Additionals { get; set; }

  }
}
