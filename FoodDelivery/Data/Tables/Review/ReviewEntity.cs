using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FoodDelivery.Data.Tables
{
  public class ReviewEntity
  {
    public int Id { get; set; }

    public string Text { get; set; }

    public string UserId { get; set; }

    public int RestaurantId { get; set; }

    public double Rating { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;

    [JsonIgnore]
    public RestaurantEntity Restaurant { get; set; }

    [JsonIgnore]
    public ApplicationUser User { get; set; }

    public List<ReviewTagEntity> Tags { get; set; }
  }
}
