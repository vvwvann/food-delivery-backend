using FoodDelivery.Data.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.Data.Configurations
{
  public class BasketDishConfiguration : IEntityTypeConfiguration<BasketDishEntity>
  {
    public void Configure(EntityTypeBuilder<BasketDishEntity> builder)
    {
      builder.ToTable("BasketDishEntity")
        .HasKey(t => new { t.UserId, t.DishId });

      builder.ToTable("BasketDishEntity")
      .HasOne(pt => pt.User)
      .WithMany(p => p.BasketDishes)
      .OnDelete(DeleteBehavior.Restrict)
      .HasForeignKey(pt => pt.UserId);

      builder.ToTable("BasketDishEntity")
      .HasOne(pt => pt.Dish)
      .WithMany(p => p.Baskets)
      .OnDelete(DeleteBehavior.Restrict)
      .HasForeignKey(pt => pt.DishId);
    }
  }
}
