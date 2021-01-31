using FoodDelivery.Data.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.Data.Configurations
{
  public class RestaurantCategoriesConfiguration : IEntityTypeConfiguration<RestaurantСuisineEntity>
  {
    public void Configure(EntityTypeBuilder<RestaurantСuisineEntity> builder)
    {
      builder.ToTable("RestaurantСuisineEntity")
        .HasKey(t => new { t.RestaurantId, t.CuisineId });

      builder.ToTable("RestaurantСuisineEntity")
      .HasOne(pt => pt.Restaurant)
      .WithMany(p => p.Cuisines)
      .OnDelete(DeleteBehavior.Restrict)
      .HasForeignKey(pt => pt.RestaurantId);

      builder.ToTable("RestaurantСuisineEntity")
      .HasOne(pt => pt.Cuisine)
      .WithMany(p => p.Restaurants)
      .OnDelete(DeleteBehavior.Restrict)
      .HasForeignKey(pt => pt.CuisineId);
    }
  }
}
