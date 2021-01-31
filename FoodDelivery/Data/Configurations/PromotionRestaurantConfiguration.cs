using FoodDelivery.Data.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.Data.Configurations
{
  public class PromotionRestaurantConfiguration : IEntityTypeConfiguration<PromotionRestaurantEntity>
  {
    public void Configure(EntityTypeBuilder<PromotionRestaurantEntity> builder)
    {
      builder.ToTable("PromotionRestaurantEntity")
        .HasKey(t => new { t.RestaurantId, t.PromotionId });

      builder.ToTable("PromotionRestaurantEntity")
      .HasOne(pt => pt.Promotion)
      .WithMany(p => p.Restaurants)
      .OnDelete(DeleteBehavior.Restrict)
      .HasForeignKey(pt => pt.PromotionId);

      builder.ToTable("PromotionRestaurantEntity")
      .HasOne(pt => pt.Restaurant)
      .WithMany(p => p.Promotions)
      .OnDelete(DeleteBehavior.Restrict)
      .HasForeignKey(pt => pt.RestaurantId);
    }
  }
}
