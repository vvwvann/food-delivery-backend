using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Services.Interfaces
{
  public interface IPaginationInfo
  {
    int PageNumber { get; }
    int PageSize { get; }
  }
}

