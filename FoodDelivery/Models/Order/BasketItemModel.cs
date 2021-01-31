using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Models.Order
{
  public class BasketItemModel
  {
    public List<DishItem> Dishes { get; set; }   
  }

  public class DishItem
  {
    public int Id { get; set; }

    public int Count { get; set; }

    public string Description { get; set; }
  }
}
