using FoodDelivery.Data.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.Data.Configurations
{
  public class FavoriteDishesConfiguration : IEntityTypeConfiguration<FavoriteDishEntity>
  {
    public void Configure(EntityTypeBuilder<FavoriteDishEntity> builder)
    {
      builder.ToTable("FavoriteDishEntity")
        .HasKey(t => new { t.UserId, t.DishId });

      builder.ToTable("FavoriteDishEntity")
      .HasOne(pt => pt.User)
      .WithMany(p => p.FavoriteDishes)
      .OnDelete(DeleteBehavior.Restrict)
      .HasForeignKey(pt => pt.UserId);

      builder.ToTable("FavoriteDishEntity")
      .HasOne(pt => pt.Dish)
      .WithMany(p => p.Favorites)
      .OnDelete(DeleteBehavior.Restrict)
      .HasForeignKey(pt => pt.DishId);
    }
  }
}
