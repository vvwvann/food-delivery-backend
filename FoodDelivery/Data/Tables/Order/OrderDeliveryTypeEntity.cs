using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Data.Tables
{
  public class OrderDeliveryTypeEntity
  {
    public int Id { get; set; }

    public string Description { get; set; }

    public List<RestaurantDeliveryEntity> Restaurants { get; set; }
  }
}
