using Newtonsoft.Json;

namespace FoodDelivery.Models.Fondy
{
  public class FondyMerchantDataField
  {
    [JsonProperty("label")]
    public string Lable { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("value")]
    public string Value { get; set; }
  }
}
