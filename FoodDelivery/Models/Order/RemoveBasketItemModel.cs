using System.Collections.Generic;

namespace FoodDelivery.Models.Order
{
  public class RemoveBasketItemModel
  {
    public List<RemoveBasketDishItem> Dishes { get; set; }

    public class RemoveBasketDishItem
    {
      public int Id { get; set; }

      public int Count { get; set; }
    }
  }
}
