using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FoodDelivery.Data.Tables;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.Data.Configurations;
using FoodDelivery.Data.Tables.Reference;
using FoodDelivery.Enums;
using FoodDelivery.Data.Tables.User;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FoodDelivery.Data
{
  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  {
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public ApplicationDbContext() : base(new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(nameof(ApplicationDbContext)).ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)).Options)
    {
      Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      //builder.Entity<AddressEntity>().Property(b => b.Location).HasColumnType("geography (point)");
      builder.HasPostgresExtension("postgis");

      builder.Entity<DishEntity>(builder =>
      {
        if (!Database.IsNpgsql()) 
        {
          builder.Property(p => p.Photo)
              .HasConversion(
                  v => string.Join("'", v),
                  v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
        }
      });


      builder.Entity<RestaurantEntity>(builder => {
        if (!Database.IsNpgsql())
        {
          builder.Property(p => p.Photo)
              .HasConversion(
                  v => string.Join("'", v),
                  v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
        }
      });


      #region HasData

      builder.Entity<OrderStatusEntity>().HasData(
       new OrderStatusEntity {
         Id = (int)OrderStatus.NEW,
         Description = "Новый"
       });

      builder.Entity<OrderStatusEntity>().HasData(
       new OrderStatusEntity {
         Id = (int)OrderStatus.PROCESSING,
         Description = "В обработке"
       });

      builder.Entity<OrderStatusEntity>().HasData(
      new OrderStatusEntity {
        Id = (int)OrderStatus.ON_THE_ROAD,
        Description = "У курьера"
      });

      builder.Entity<OrderStatusEntity>().HasData(
      new OrderStatusEntity {
        Id = (int)OrderStatus.DONE,
        Description = "Исполнен"
      });

      builder.Entity<OrderStatusEntity>().HasData(
      new OrderStatusEntity {
        Id = (int)OrderStatus.CANCEL,
        Description = "Отменён"
      });

      builder.Entity<DeliveryTypeEntity>().HasData(
      new DeliveryTypeEntity {
        Id = (int)DeliveryType.ENTRANCE,
        Description = "К входу в здание"
      });

      builder.Entity<OrderPaymentTypeEntity>().HasData(
      new OrderPaymentTypeEntity {
        Id = (int)OrderPaymentType.CASH,
        Description = "Наличными"
      });

      builder.Entity<OrderPaymentTypeEntity>().HasData(
     new OrderPaymentTypeEntity {
       Id = (int)OrderPaymentType.BANK,
       Description = "Банковской картой"
     });

      builder.Entity<OrderDeliveryTypeEntity>().HasData(
      new OrderDeliveryTypeEntity {
        Id = (int)OrderDeliveryType.COURIER,
        Description = "Курьером"
      });

      builder.Entity<OrderDeliveryTypeEntity>().HasData(
      new OrderDeliveryTypeEntity {
        Id = (int)OrderDeliveryType.PICKUP,
        Description = "Самовывоз"
      });

      builder.Entity<WidgetEntity>().HasData(
      new WidgetEntity {
        Id = (int)WidgetType.Popular,
        Name = "Популярные заведеня города"
      });

      builder.Entity<WidgetEntity>().HasData(
           new WidgetEntity {
             Id = (int)WidgetType.FreeDelivery,
             Name = "С бесплатной доставкой"
           });

      builder.Entity<WidgetEntity>().HasData(
      new WidgetEntity {
        Id = (int)WidgetType.New,
        Name = "Новые рестораны"
      });

      builder.Entity<WidgetEntity>().HasData(
      new WidgetEntity {
        Id = (int)WidgetType.Near,
        Name = "Рядом с вами"
      });

      builder.Entity<PromotionEntity>().HasData(
      new PromotionEntity {
        Id = (int)PromotionType.Sum,
        Name = "Скидка в зависимости от суммы заказа"
      });

      builder.Entity<PromotionEntity>().HasData(
      new PromotionEntity {
        Id = (int)PromotionType.FreeDelivery,
        Name = "Бесплатная доставка в зависимости от суммы заказа"
      });

      builder.Entity<PromotionEntity>().HasData(
      new PromotionEntity {
        Id = (int)PromotionType.Several,
        Name = "При заказе нескольких порций"
      });

      builder.Entity<PromotionEntity>().HasData(
      new PromotionEntity {
        Id = (int)PromotionType.Dish,
        Name = "Акция для блюда"
      });

      #endregion

      base.OnModelCreating(builder);

      #region Configurations

      builder.ApplyConfiguration(new OrderConfiguration());
      builder.ApplyConfiguration(new DishAdditionConfiguration());
      builder.ApplyConfiguration(new OrderDishConfiguration());
      builder.ApplyConfiguration(new RestaurantCategoriesConfiguration());
      builder.ApplyConfiguration(new FavoriteRestaurantsConfiguration());
      builder.ApplyConfiguration(new FavoriteDishesConfiguration());
      builder.ApplyConfiguration(new BasketDishConfiguration());
      builder.ApplyConfiguration(new ReviewTagConfiguration());
      builder.ApplyConfiguration(new WidgetRestaurantsConfiguration());
      builder.ApplyConfiguration(new RestaurantDeliveryConfiguration());
      builder.ApplyConfiguration(new PromotionRestaurantConfiguration());
      builder.ApplyConfiguration(new PromotionDishConfiguration());

      #endregion
    }

    #region DbSet

    public DbSet<StatisticEntity> Statistics { get; set; }
    public DbSet<ConnectionEntity> Connections { get; set; }
    public DbSet<SessionEntity> Sessions { get; set; }
    public DbSet<AddressEntity> Addresses { get; set; }
    public DbSet<DeliveryTypeEntity> DeliveryTypes { get; set; }
    public DbSet<DishEntity> Dishes { get; set; }
    public DbSet<OrderStatusEntity> OrderStatuses { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<OrderDishEntity> OrderDishes { get; set; }
    public DbSet<ReviewEntity> Reviews { get; set; }
    public DbSet<RestaurantEntity> Restaurants { get; set; }
    public DbSet<DishTypeEntity> DishTypes { get; set; }
    public DbSet<CuisineEntity> Categories { get; set; }
    public DbSet<HomePromotionEntity> HomePromotions { get; set; }
    public DbSet<PromotionEntity> Promotions { get; set; }
    public DbSet<RestaurantСuisineEntity> RestaurantCategories { get; set; }
    public DbSet<FavoriteRestaurantEntity> FavoriteRestaurants { get; set; }
    public DbSet<FavoriteDishEntity> FavoriteDishes { get; set; }
    public DbSet<BasketDishEntity> BasketDishes { get; set; }
    public DbSet<OrderDeliveryTypeEntity> OrderDeliveryTypes { get; set; }
    public DbSet<OrderPaymentTypeEntity> OrderPaymentTypes { get; set; }
    public DbSet<TagEntity> Tags { get; set; }
    public DbSet<ReviewTagEntity> ReviewTags { get; set; }
    public DbSet<WidgetRestaurantEntity> WidgetRestaurants { get; set; }
    public DbSet<WidgetEntity> Widgets { get; set; }
    public DbSet<RestaurantDeliveryEntity> RestaurantDeliveries { get; set; }
    public DbSet<MenuCategoryEntity> MenuCategories { get; set; }
    public DbSet<DishAdditionalEntity> DishAdditionals { get; set; }
    public DbSet<NewsEntity> News { get; set; }
    public DbSet<SettingsEntity> Settings { get; set; }
    public DbSet<PromotionDishEntity> PromotionDishes { get; set; }
    public DbSet<PromotionRestaurantEntity> PromotionRestaurants { get; set; }

    #endregion
  }
}
