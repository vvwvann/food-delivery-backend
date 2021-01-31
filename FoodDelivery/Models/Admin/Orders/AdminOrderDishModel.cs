namespace FoodDelivery.Models.Admin.Orders
{
  public class AdminOrderDishModel
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public decimal Sum { get; set; }

    public int Count { get; set; }
  }
}
