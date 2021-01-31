using FoodDelivery.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Models.Data
{
  public class DeliveryTypesResponseModel
  {
    public List<DeliveryTypeEntity> Items { get; set; }
  }
}
