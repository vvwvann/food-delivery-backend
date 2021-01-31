using FoodDelivery.Data.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.Data.Configurations
{
  public class FavoriteRestaurantsConfiguration : IEntityTypeConfiguration<FavoriteRestaurantEntity>
  {
    public void Configure(EntityTypeBuilder<FavoriteRestaurantEntity> builder)
    {
      builder.ToTable("FavoriteEntity")
        .HasKey(t => new { t.UserId, t.RestaurantId });

      builder.ToTable("FavoriteEntity")
      .HasOne(pt => pt.User)
      .WithMany(p => p.FavoriteRestaurants)
      .OnDelete(DeleteBehavior.Restrict)
      .HasForeignKey(pt => pt.UserId);

      builder.ToTable("FavoriteEntity")
      .HasOne(pt => pt.Restaurant)
      .WithMany(p => p.Favorites)
      .OnDelete(DeleteBehavior.Restrict)
      .HasForeignKey(pt => pt.RestaurantId);
    }
  }
}
