using FoodDelivery.Data.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.Data.Configurations
{
  public class RestaurantDeliveryConfiguration : IEntityTypeConfiguration<RestaurantDeliveryEntity>
  {
    public void Configure(EntityTypeBuilder<RestaurantDeliveryEntity> builder)
    {
      builder.ToTable("RestaurantDeliveryEntity")
        .HasKey(t => new { t.RestaurantId, t.DeliveryId });

      builder.ToTable("RestaurantDeliveryEntity")
      .HasOne(pt => pt.Restaurant)
      .WithMany(p => p.Deliveries)
      .OnDelete(DeleteBehavior.Restrict)
      .HasForeignKey(pt => pt.RestaurantId);

      builder.ToTable("RestaurantDeliveryEntity")
      .HasOne(pt => pt.Delivery)
      .WithMany(p => p.Restaurants)
      .OnDelete(DeleteBehavior.Restrict)
      .HasForeignKey(pt => pt.DeliveryId);
    }
  }
}
