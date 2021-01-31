using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Models.Order
{
  public class BasketResponseModel
  {
    public BasketResponseItem[] Items { get; set; }   
  }

  public class BasketResponseItem
  {
    public int Id { get; set; }

    public int Count { get; set; }

    public string Photo { get; set; }

    public decimal Price { get; set; }
  }
}
