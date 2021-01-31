using Newtonsoft.Json;
using System.Collections.Generic;

namespace FoodDelivery.Models.Admin.Restaurant
{
  public class AdminDishModel
  {
    public string[] Photo { get; set; }

    [JsonRequired]
    public string Name { get; set; }

    [JsonRequired]
    public decimal Price { get; set; }
    public string Ingridients { get; set; }
    public string Description { get; set; }

    [JsonRequired]
    public int MenuCategoryId { get; set; }

    public int Minutes { get; set; }
    public int CuisineId { get; set; }
  }
}
