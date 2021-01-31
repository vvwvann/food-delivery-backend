using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Data.Tables
{  public class OrderEntity
  {
    public int Id { get; set; }

    public decimal Sum { get; set; }

    [JsonIgnore]
    public decimal Tip { get; set; }

    [JsonIgnore]
    public int DeliveryMinutes { get; set; }

    public decimal DeliverySum { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [JsonIgnore]
    public int AddressId { get; set; }

    [JsonIgnore]
    public AddressEntity Address { get; set; }

    public int StatusId { get; set; } = 1;

    public int PaymentTypeId { get; set; }

    public int DeliveryTypeId { get; set; }

    public bool IsPaid { get; set; }

    [JsonIgnore]
    public string UserId { get; set; }

    [JsonIgnore]
    public int RestaurantId { get; set; }

    [JsonIgnore]
    public string ManagerId { get; set; }

    [JsonIgnore]
    public ApplicationUser User { get; set; }

    [JsonIgnore]
    public ApplicationUser Manager { get; set; }

    [JsonIgnore]
    public OrderStatusEntity Status { get; set; }

    [JsonIgnore]
    public OrderPaymentTypeEntity PaymentType { get; set; }

    [JsonIgnore]
    public OrderDeliveryTypeEntity OrderDeliveryType { get; set; }

    [JsonIgnore]
    public List<OrderDishEntity> Dishes { get; set; }

    [JsonIgnore]
    public RestaurantEntity Restaurant { get; set; }
  }

}
