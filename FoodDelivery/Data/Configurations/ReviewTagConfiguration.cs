using FoodDelivery.Data.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.Data.Configurations
{
  public class ReviewTagConfiguration : IEntityTypeConfiguration<ReviewTagEntity>
  {
    public void Configure(EntityTypeBuilder<ReviewTagEntity> builder)
    {
      builder.ToTable("ReviewTagEntity")
        .HasKey(t => new { t.ReviewId, t.TagId });

      builder.ToTable("ReviewTagEntity")
      .HasOne(pt => pt.Review)
      .WithMany(p => p.Tags)
      .OnDelete(DeleteBehavior.Restrict)
      .HasForeignKey(pt => pt.ReviewId);

      builder.ToTable("ReviewTagEntity")
      .HasOne(pt => pt.Tag)
      .WithMany(p => p.Reviews)
      .OnDelete(DeleteBehavior.Restrict)
      .HasForeignKey(pt => pt.TagId);
    }
  }
}
