using FoodDelivery.Data.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.Data.Configurations
{
  public class WidgetRestaurantsConfiguration : IEntityTypeConfiguration<WidgetRestaurantEntity>
  {
    public void Configure(EntityTypeBuilder<WidgetRestaurantEntity> builder)
    {
      builder.ToTable("WidgetRestaurantEntity")
        .HasKey(t => new { t.RestaurantId, t.WidgetId });

      builder.ToTable("WidgetRestaurantEntity")
      .HasOne(pt => pt.Restaurant)
      .WithMany(p => p.Widgets)
      .OnDelete(DeleteBehavior.Restrict)
      .HasForeignKey(pt => pt.RestaurantId);

      builder.ToTable("WidgetRestaurantEntity")
      .HasOne(pt => pt.Widget)
      .WithMany(p => p.Restaurants)
      .OnDelete(DeleteBehavior.Restrict)
      .HasForeignKey(pt => pt.WidgetId);
    }
  }
}
