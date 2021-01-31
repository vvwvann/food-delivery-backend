using FoodDelivery.Data.Tables;
using System.Collections.Generic;

namespace FoodDelivery.Models.Admin.Orders
{
  public class AdminOrderReponseModel
  {
    public int Id { get; set; }

    public decimal Sum { get; set; }

    public decimal Tip { get; set; }

    public List<AdminOrderDishModel> Dishes { get; set; }

    public int StatusId { get; set; }

    public int DeliveryMinutes { get; set; }

    public bool IsPaid { get; set; }

    public decimal DeliverySum { get; set; }

    public int PaymentTypeId { get; set; }

    public int DeliveryTypeId { get; set; }

    public AddressEntity Address { get; set; }

    public AdminManagerModel Manager { get; set; }

  }
}
