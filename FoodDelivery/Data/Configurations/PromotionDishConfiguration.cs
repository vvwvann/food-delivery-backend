using FoodDelivery.Data.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.Data.Configurations
{
  public class PromotionDishConfiguration : IEntityTypeConfiguration<PromotionDishEntity>
  {
    public void Configure(EntityTypeBuilder<PromotionDishEntity> builder)
    {
      builder.ToTable("PromotionDishEntity")
        .HasKey(t => new { t.DishId, t.PromotionId });

      builder.ToTable("PromotionDishEntity")
      .HasOne(pt => pt.Promotion)
      .WithMany(p => p.Dishes)
      .OnDelete(DeleteBehavior.Restrict)
      .HasForeignKey(pt => pt.PromotionId);

      builder.ToTable("PromotionDishEntity")
      .HasOne(pt => pt.Dish)
      .WithMany(p => p.Promotions)
      .OnDelete(DeleteBehavior.Restrict)
      .HasForeignKey(pt => pt.DishId);
    }
  }
}
