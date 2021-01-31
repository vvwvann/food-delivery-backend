using FoodDelivery.Data.Tables;
using System.Collections.Generic;
using static FoodDelivery.Models.Order.BasketResponseModel;

namespace FoodDelivery.Models.Order
{
  public class OrderResponseModel
  {
    public int Id { get; set; }

    public AddressEntity Address { get; set; }

    public int DeliveryMinutes { get; set; }

    public int StatusId { get; set; }

    public decimal Sum { get; set; }

    public decimal DeliverySum { get; set; }

    public List<BasketResponseItem> Dishes { get; set; }
  }
}
