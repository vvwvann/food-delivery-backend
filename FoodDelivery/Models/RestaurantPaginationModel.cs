namespace FoodDelivery.Models
{
  public class RestaurantPaginationModel : PaginationModel
  {
    public string Sort { get; set; }

    public int[] Cuisines { get; set; }

    public int[] DeliveryTypes { get; set; }

    public double PriceFrom { get; set; }

    public double PriceTo { get; set; } = 100000;

    public double Latitude { get; set; }

    public double Longitude { get; set; }
  }
}
