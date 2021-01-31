using Newtonsoft.Json;

namespace FoodDelivery.Data.Tables
{
  public class ReviewTagEntity
  {
    public int ReviewId { get; set; }

    public int TagId { get; set; }

    [JsonIgnore]
    public TagEntity Tag { get; set; }

    [JsonIgnore]
    public ReviewEntity Review { get; set; }
  }
}
