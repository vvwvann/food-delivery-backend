using FoodDelivery.Data.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.Data.Configurations
{
  public class OrderDishConfiguration : IEntityTypeConfiguration<OrderDishEntity>
  {
    public void Configure(EntityTypeBuilder<OrderDishEntity> builder)
    {
      builder.ToTable("OrderDishEntity")
        .HasKey(t => new { t.OrderId, t.DishId });

      builder.ToTable("OrderDishEntity")
      .HasOne(pt => pt.Order)
      .WithMany(p => p.Dishes)
      .OnDelete(DeleteBehavior.Restrict)
      .HasForeignKey(pt => pt.OrderId);

      builder.ToTable("OrderDishEntity")
      .HasOne(pt => pt.Dish)
      .WithMany(p => p.Orders)
      .OnDelete(DeleteBehavior.Restrict)
      .HasForeignKey(pt => pt.DishId);
    }
  }
}
