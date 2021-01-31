using FoodDelivery.Data.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.Data.Configurations
{
  public class DishAdditionConfiguration : IEntityTypeConfiguration<DishAdditionalEntity>
  {
    public void Configure(EntityTypeBuilder<DishAdditionalEntity> builder)
    {
      builder.ToTable("DishAdditionalEntity")
        .HasKey(t => new { t.AdditionalId, t.DishId });

      builder.ToTable("DishAdditionalEntity")
      .HasOne(pt => pt.Dish)
      .WithMany(p => p.Additionals)
      .OnDelete(DeleteBehavior.Restrict)
      .HasForeignKey(pt => pt.DishId);

      builder.ToTable("DishAdditionalEntity")
      .HasOne(pt => pt.Additional)
      .WithMany(p => p.Dishes)
      .OnDelete(DeleteBehavior.Restrict)
      .HasForeignKey(pt => pt.AdditionalId);
    }
  }

  public class BasketDishesConfiguration : IEntityTypeConfiguration<BasketDishEntity>
  {
    public void Configure(EntityTypeBuilder<BasketDishEntity> builder)
    {
      builder.ToTable("BasketDishEntity")
        .HasKey(t => new { t.UserId, t.DishId });

      builder.ToTable("BasketDishEntity")
      .HasOne(pt => pt.Dish)
      .WithMany(p => p.Baskets)
      .OnDelete(DeleteBehavior.Restrict)
      .HasForeignKey(pt => pt.DishId);

      builder.ToTable("BasketDishEntity")
      .HasOne(pt => pt.User)
      .WithMany(p => p.BasketDishes)
      .OnDelete(DeleteBehavior.Restrict)
      .HasForeignKey(pt => pt.UserId);
    }
  }
}
