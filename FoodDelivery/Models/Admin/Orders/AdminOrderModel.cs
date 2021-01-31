using FoodDelivery.Models.Admin.Orders;
using System;

namespace FoodDelivery.Services.Admin
{
  public class AdminOrderModel
  {
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public int StatusId { get; set; }

    public decimal Sum { get; set; }

    public AdminManagerModel Manager { get; set; }
  }
}
