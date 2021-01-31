using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Models.Order
{
  public class ConfirmOrderRequestModel
  {
    public int DeliveryId { get; set; }

    public int PaymentId { get; set; }

    public TipItem Tip { get; set; }

    public class TipItem
    {
      public int TypeId { get; set; }

      public decimal Value { get; set; }
    }
  } 
}
