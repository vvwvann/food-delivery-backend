using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery
{
  public enum OrderStatus
  {
    NEW = 1,
    PROCESSING = 2,
    ON_THE_ROAD = 3,
    DONE = 4,
    CANCEL = 5
  }
}
