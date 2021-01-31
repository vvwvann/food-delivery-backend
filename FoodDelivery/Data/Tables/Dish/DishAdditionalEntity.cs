using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Data.Tables
{
  public class DishAdditionalEntity
  {
    public int DishId { get; set; }

    public int AdditionalId { get; set; }


    public DishEntity Dish { get; set; }

    public DishEntity Additional { get; set; }
  }
}
