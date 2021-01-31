using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Data.Tables
{
  public class CuisineEntity
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string Photo { get; set; }

    [JsonIgnore]
    public List<RestaurantСuisineEntity> Restaurants { get; set; }

  }
}
