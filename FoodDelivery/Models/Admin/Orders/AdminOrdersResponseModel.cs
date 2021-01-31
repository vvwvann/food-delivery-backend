using System.Collections.Generic;

namespace FoodDelivery.Services.Admin
{
  public class AdminOrdersResponseModel
  {
    public int Total { get; set; }

    public List<AdminOrderModel> Items { get; set; }   
  }
}
