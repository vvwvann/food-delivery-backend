using System;

namespace FoodDelivery.Data.Tables
{
  public class PromotionDishEntity
  {
    public int PromotionId { get; set; }

    public int DishId { get; set; }

    public int? Count { get; set; }

    public double Percent { get; set; }

    public DateTime From { get; set; }

    public DateTime To { get; set; }

    public PromotionEntity Promotion { get; set; }

    public DishEntity Dish { get; set; }
  }
}
