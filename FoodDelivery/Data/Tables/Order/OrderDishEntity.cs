using System.Collections.Generic;

namespace FoodDelivery.Data.Tables
{
  public class OrderDishEntity
  {
    public int Count { get; set; }

  //  public string[] Description { get; set; }

    public int OrderId { get; set; }

    public int DishId { get; set; }

    public decimal Sum { get; set; }

    public DishEntity Dish { get; set; }

    public OrderEntity Order { get; set; }
  }
}
