using FoodDelivery.Data;
using FoodDelivery.Data.Tables;
using FoodDelivery.Exceptions;
using FoodDelivery.Models;
using FoodDelivery.Models.Restaurants;
using FoodDelivery.Models.Reviews;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static FoodDelivery.Models.Restaurants.GetAllRestaurantsResponseModel;
using static FoodDelivery.Models.Reviews.GetAllReviewsResponseModel;

namespace FoodDelivery.Services
{
  public class RestaurantService : IRestaurantService
  {
    private ApplicationDbContext _context;

    public RestaurantService(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<GetAllRestaurantsResponseModel> GetMapAsync(string userId, MapRestaurantPaginationModel filter)
    {
      IQueryable<RestaurantEntity> a = null;

      if (filter.Cuisines != null && filter.Cuisines.Length > 0) {
        a = _context.Restaurants
        .AsNoTracking()
        .Include(x => x.Reviews)
        .Include(x => x.Cuisines)
        .Where(x => !x.IsBlock && x.Cuisines
        .Any(x => filter.Cuisines.Contains(x.CuisineId)));
      }
      else {
        a = _context.Restaurants
        .AsNoTracking()
        .Include(x => x.Reviews)
        .Include(x => x.Cuisines)
        .Where(x => !x.IsBlock);
      }

      if (filter.DeliveryTypes != null && filter.DeliveryTypes.Length > 0) {
        a = a.Where(x => x.Deliveries
        .Any(x => filter.DeliveryTypes.Contains(x.DeliveryId)));
      }

      if (filter.Latitude > 0 && filter.Longitude > 0) {
        Point point = new Point(filter.Latitude, filter.Longitude) { SRID = 4326 };
        a = a.Where(c => c.Address.Location.Distance(point) < filter.Radius);
      }

      var restaurants = await a.Where(x => x.PriceFrom <= filter.PriceFrom && x.PriceTo >= filter.PriceTo).ToListAsync();

      return new GetAllRestaurantsResponseModel {
        Restaurants = await GetRestaurantsAsync(restaurants, userId),
        Total = restaurants.Count
      };
    }

    private async Task<List<RestaurantListItem>> GetRestaurantsAsync(List<RestaurantEntity> restaurants, string userId)
    {
      List<RestaurantListItem> list = new List<RestaurantListItem>();

      foreach (var item in restaurants) {
        list.Add(new RestaurantListItem {
          Id = item.Id,
          Delivery = item.Delivery,
          Name = item.Name,
          Photo = item.Photo,
          Rating = new RestaurantItem.RatingItem {
            Total = item.Reviews == null ? 0 : item.Reviews.Count,
            Value = item.Rating
          },
          IsFavorite = userId != null && (await _context.FavoriteRestaurants.FirstOrDefaultAsync(x => x.RestaurantId == item.Id && x.UserId == userId) != null),
          Cuisines = item.Cuisines.Select(x => x.CuisineId).ToList()
        });
      }

      return list;
    }

    public async Task<GetAllRestaurantsResponseModel> GetAllAsync(string userId, RestaurantPaginationModel filter)
    {
      List<RestaurantListItem> list = new List<RestaurantListItem>();
      List<RestaurantEntity> restaurants = new List<RestaurantEntity>();
      IQueryable<RestaurantEntity> a = null;

      if (filter.Cuisines != null && filter.Cuisines.Length > 0) {
        a = _context.Restaurants
        .AsNoTracking()
        .Include(x => x.Reviews)
        .Include(x => x.Cuisines)
        .Where(x => !x.IsBlock && x.Cuisines
        .Any(x => filter.Cuisines.Contains(x.CuisineId)));
      }
      else {
        a = _context.Restaurants
        .AsNoTracking()
        .Include(x => x.Reviews)
        .Include(x => x.Cuisines)
        .Where(x => !x.IsBlock);
      }

      if (filter.DeliveryTypes != null && filter.DeliveryTypes.Length > 0) {
        a = a.Where(x => x.Deliveries
        .Any(x => filter.DeliveryTypes.Contains(x.DeliveryId)));
      }

      a = a.Where(x => x.PriceFrom <= filter.PriceFrom && x.PriceTo >= filter.PriceTo);
      Expression<Func<RestaurantEntity, object>> orderExp = x => x.Id;
      int total = 0;
      if (filter.Sort == "near" && filter.Latitude > 0 && filter.Longitude > 0) {
        Point point = new Point(filter.Latitude, filter.Longitude) { SRID = 4326 };
        restaurants = await a.Where(c => c.Address.Location.Distance(point) < 100).ToListAsync();
      }
      else if (filter.Sort == "rating" || filter.Sort == "new") {
        orderExp = filter.Sort switch {
          "new" => x => x.CreatedAt,
          "popular" => x => x.Rating,
          _ => x => x.Id
        };

        total = await a.CountAsync();

        restaurants = await a.OrderByDescending(orderExp).ToListAsync();
      }
      else {
        orderExp = filter.Sort switch {
          "abc" => x => x.Name[0],
          "price" => x => x.PriceFrom,
          _ => x => x.Id
        };
        total = await a.CountAsync();

        restaurants = await a.OrderBy(orderExp).ToListAsync();
      }

      return new GetAllRestaurantsResponseModel {
        Restaurants = await GetRestaurantsAsync(restaurants, userId),
        Total = total
      };
    }

    public async Task<RestaurantItem> GetByIdAsync(string userId, int id)
    {
      var restaurant = await _context.Restaurants
        .Include(x => x.Address)
        .Include(x => x.Reviews)
        .Include(x => x.Menu)
        .ThenInclude(x => x.Dishes)
        .FirstOrDefaultAsync(r => r.Id == id);

      if (restaurant == null || restaurant.IsBlock)
        throw new ApiException("Ресторан не найден", 400);


      RestaurantItem result = new RestaurantItem {
        Id = restaurant.Id,
        Description = restaurant.Description,
        Name = restaurant.Name,
        Photo = restaurant.Photo,
        FreeDelivery = restaurant.FreeDelivery,
        Rating = new RestaurantItem.RatingItem {
          Total = restaurant.Reviews == null ? 0 : restaurant.Reviews.Count(),
          Value = restaurant.Rating
        },
        Address = restaurant.Address == null ? null : new RestaurantItem.AddressItem {
          ApartmentNumber = restaurant.Address.ApartmentNumber,
          City = restaurant.Address.City,
          HouseNumber = restaurant.Address.HouseNumber,
          Street = restaurant.Address.Street,
          Latitude = restaurant.Address.Latitude,
          Longitude = restaurant.Address.Longitude
        },
        Delivery = restaurant.Delivery,
        Schedule = new RestaurantItem.ScheduleItem {
          From = restaurant.ScheduleFrom,
          To = restaurant.ScheduleTo
        },
        IsFavorite = userId != null && (await _context.FavoriteRestaurants.FirstOrDefaultAsync(x => x.RestaurantId == restaurant.Id && x.UserId == userId) != null),
        Cuisines = await _context.RestaurantCategories.Where(x => x.RestaurantId == restaurant.Id).Select(x => x.CuisineId).ToListAsync()
      };


      var reviews = new List<ReviewItem>();
      var items = _context.Reviews
        .Include(x => x.Tags)
        .Where(x => x.RestaurantId == id)
        .OrderByDescending(x => x.CreatedAt)
        .Take(20);

      foreach (var item in items) {
        reviews.Add(new ReviewItem {
          Id = item.Id,
          CreatedAt = item.CreatedAt,
          Rating = item.Rating,
          Text = item.Text,
          Tags = item.Tags.Select(x => x.TagId).ToList(),
          User = new ReviewItem.UserReviewItem {
            Id = item.UserId,
            Name = item.User.Name,
            Photo = item.User.Photo
          }
        });
      }

      result.Reviews = new GetAllReviewsResponseModel {
        Total = restaurant.Reviews == null ? 0 : restaurant.Reviews.Count,
        Items = reviews
      };

      restaurant.Menu.ForEach(x => x.Dishes.All(x => !x.IsBlock));
      restaurant.Menu.ForEach(x => x.Dishes.OrderBy(x => x.Index));

      result.Menu = restaurant.Menu;
      return result;
    }


    public async Task<List<CuisineEntity>> GetCategoriesAsync()
    {
      return await _context.Categories.ToListAsync();
    }
  }
}
