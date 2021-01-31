using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Models
{
  public class TotalResponseModel<T>
  {
    public int Total { get; set; }
    public List<T> Items { get; set; }

  }

}
