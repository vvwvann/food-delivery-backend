using System.Collections.Generic;

namespace FoodDelivery.Models.Favorites
{
  public class GetFavoriteDishesModel
  {
    public int Total { get; set; }

    public DishListItem[] Items { get; set; }
  }


  public class DishListItem
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string Photo { get; set; }

    public decimal Price { get; set; }
  }
}
