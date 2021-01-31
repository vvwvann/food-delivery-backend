using FoodDelivery.Data.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.Data.Configurations
{
  public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
  {
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
      builder.ToTable("OrderEntity")
        .HasKey(t => t.Id);

      builder.ToTable("OrderEntity")
      .HasOne(pt => pt.User)
      .WithMany(p => p.Orders)
      .OnDelete(DeleteBehavior.Restrict)
      .HasForeignKey(pt => pt.UserId);

      builder.ToTable("OrderEntity")
      .HasOne(pt => pt.Manager)
      .WithMany(p => p.ManagedOrders)
      .OnDelete(DeleteBehavior.Restrict)
      .HasForeignKey(pt => pt.ManagerId);
    }
  }
}
