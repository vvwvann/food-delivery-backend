using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Models.Order
{
  public class OrderModel
  {
    public decimal Tip { get; set; }

    public decimal DeliverySum { get; set; }

    public int DeliveryMinutes { get; set; }

    public int DeliveryTypeId { get; set; }

    public int PaymentTypeId { get; set; }

    public int AddressId { get; set; }

    public List<OrderDishItem> Dishes { get; set; }
  }

  public class OrderDishItem
  {
    public int Id { get; set; }

    public int Count { get; set; }

    public decimal Sum { get; set; }

    public string Description { get; set; }
  }
}
