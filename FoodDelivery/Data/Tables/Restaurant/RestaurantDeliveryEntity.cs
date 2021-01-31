namespace FoodDelivery.Data.Tables
{
  public class RestaurantDeliveryEntity
  {
    public int DeliveryId { get; set; }

    public int RestaurantId { get; set; }

    public RestaurantEntity Restaurant { get; set; }

    public OrderDeliveryTypeEntity Delivery { get; set; }
  }
}
