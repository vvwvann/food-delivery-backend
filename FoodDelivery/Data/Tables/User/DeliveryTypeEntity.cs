using Newtonsoft.Json;
using System.Collections.Generic;

namespace FoodDelivery.Data.Tables
{
  public class DeliveryTypeEntity
  {
    public int Id { get; set; }

    public string Description { get; set; }

    [JsonIgnore]
    public List<AddressEntity> Addresses { get; set; }
  }
}
