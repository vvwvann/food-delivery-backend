using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.Data.Tables
{
  public class AddressEntity
  {
    public int Id { get; set; }

    public string City { get; set; }

    public string Street { get; set; }

    public string HouseNumber { get; set; }

    public string ApartmentNumber { get; set; }

    public string Description { get; set; }

    public int? DeliveryTypeId { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    [JsonIgnore]
    [Column(TypeName = "geography")]
    public Point Location { get; set; }

    [JsonIgnore]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [JsonIgnore]
    public DeliveryTypeEntity DeliveryType { get; set; }

    [JsonIgnore]
    public string UserId { get; set; }

    [JsonIgnore]
    public ApplicationUser User { get; set; }
  }
}
