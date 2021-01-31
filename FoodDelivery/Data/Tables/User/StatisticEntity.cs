using System;

namespace FoodDelivery.Data.Tables.User
{
  public class StatisticEntity
  {
    public int Id { get; set; }

    public int RegisterCount { get; set; }

    public DateTime Date { get; set; } = DateTime.UtcNow;

    public int OrdersCount { get; set; }

    public decimal TotalSum { get; set; }
  }
}
