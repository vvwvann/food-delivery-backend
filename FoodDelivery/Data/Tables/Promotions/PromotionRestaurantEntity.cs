using System;

namespace FoodDelivery.Data.Tables
{
  public class PromotionRestaurantEntity
  {
    public int PromotionId { get; set; }

    public int RestaurantId { get; set; }

    public decimal MinValue { get; set; }

    public double Percent { get; set; }

    public DateTime From { get; set; }

    public DateTime To { get; set; }

    public PromotionEntity Promotion { get; set; }

    public RestaurantEntity Restaurant { get; set; }
  }
}
