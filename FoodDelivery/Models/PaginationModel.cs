using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Models
{
  public class PaginationModel
  {
    public int Count { get; set; } = 20;

    public int Id { get; set; }
  }
}
