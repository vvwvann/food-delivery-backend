using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models
{
  public class MapRestaurantPaginationModel
  {
    public string Search { get; set; }

    public int[] Cuisines { get; set; }

    public int[] DeliveryTypes { get; set; }

    public double PriceFrom { get; set; }

    public double PriceTo { get; set; } = 100000;

    [Required]
    public double Latitude { get; set; }

    [Required]
    public double Longitude { get; set; }

    [Required]
    public int Radius { get; set; }
  }
}
