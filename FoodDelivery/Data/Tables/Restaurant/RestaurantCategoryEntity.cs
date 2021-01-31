using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Data.Tables
{
  public class RestaurantСuisineEntity
  {
    public int RestaurantId { get; set; }

    public int CuisineId { get; set; }

    public RestaurantEntity Restaurant { get; set; }

    public CuisineEntity Cuisine { get; set; }
  }
}
