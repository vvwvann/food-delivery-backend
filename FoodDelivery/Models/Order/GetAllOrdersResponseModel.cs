using FoodDelivery.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Models.Order
{
  public class GetAllOrdersResponseModel
  {
    public int Total { get; set; }

    public List<OrderEntity> Items { get; set; }
  }
}
