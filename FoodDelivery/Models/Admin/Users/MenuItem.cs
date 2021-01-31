using FoodDelivery.Models.Admin.Restaurant;
using System.Collections.Generic;

namespace FoodDelivery.Models.Admin.Users
{
  public class MenuItem : AdminDishModel
  {
    public int Id { get; set; }

    public List<MenuDishModel> Additionals { get; set; }

  }
}
