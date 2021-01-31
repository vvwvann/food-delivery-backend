using AutoMapper;
using FoodDelivery.Data;
using FoodDelivery.Data.Tables;
using FoodDelivery.Exceptions;
using FoodDelivery.Models;
using FoodDelivery.Models.Favorites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static FoodDelivery.Models.Restaurants.GetAllRestaurantsResponseModel;

namespace FoodDelivery.Services
{
  public class FavoriteRestaurantService : IFavoriteRestaurantService
  {
    private ApplicationDbContext _context;
    private IMapper _mapper;

    public FavoriteRestaurantService(ApplicationDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task<GetFavoriteRestaurantsModel> GetRestaurantsAsync(string userId, PaginationModel model)
    {
      var date = model.Id > 0 ? await _context.FavoriteRestaurants
          .Where(x => x.UserId == userId)
          .Select(x => x.CreatedAt)
          .FirstOrDefaultAsync() : DateTime.MaxValue;

      var favorites = await _context.FavoriteRestaurants
         .Include(x => x.Restaurant)
         .ThenInclude(x => x.Reviews)
         .Where(x => x.UserId == userId)
         .OrderByDescending(x => x.CreatedAt)
         .Take(model.Count)
         .ToListAsync();

      List<RestaurantListItem> result = new List<RestaurantListItem>();

      foreach (var fav in favorites) {
        var item = fav.Restaurant;
        result.Add(new RestaurantListItem {
          Id = item.Id,
          Delivery = item.Delivery,
          Name = item.Name,
          Photo = item.Photo,
          Rating = new RestaurantItem.RatingItem {
            Total = item.Reviews == null ? 0 : item.Reviews.Count,
            Value = item.Rating
          },
          IsFavorite = true,
          Cuisines = await _context.RestaurantCategories
          .Where(x => x.RestaurantId == item.Id)
          .Select(x => x.CuisineId)
          .ToListAsync()
        });
      }

      return new GetFavoriteRestaurantsModel {
        Total = await _context.FavoriteRestaurants.Where(x => x.UserId == userId).CountAsync(),
        Items = result
      };
    }

    public async Task AddRestaurantAsync(string userId, AddRestaurantsModel model)
    {
      foreach (var id in model.Items) {
        var rest = await GetRestaurantAsync(id);

        var fav = await _context.FavoriteRestaurants.FirstOrDefaultAsync(x => x.RestaurantId == id && x.UserId == userId);
        if (fav == null) {
          await _context.FavoriteRestaurants.AddAsync(new FavoriteRestaurantEntity(userId, id));
        }
      }
      await _context.SaveChangesAsync();
    }

    public async Task AddRestaurantAsync(string userId, int restaurantId)
    {
      var rest = await GetRestaurantAsync(restaurantId);

      var fav = await _context.FavoriteRestaurants.FirstOrDefaultAsync(x => x.RestaurantId == restaurantId && x.UserId == userId);
      if (fav != null) return;

      await _context.FavoriteRestaurants.AddAsync(new FavoriteRestaurantEntity(userId, restaurantId));
      await _context.SaveChangesAsync();
    }

    public async Task RemoveRestaurantAsync(string userId, int restaurantId)
    {
      var rest = await GetRestaurantAsync(restaurantId);

      var fav = await _context.FavoriteRestaurants.FirstOrDefaultAsync(x => x.RestaurantId == restaurantId && x.UserId == userId);
      if (fav == null) return;

      _context.FavoriteRestaurants.Remove(fav);
      await _context.SaveChangesAsync();
    }

    private async Task<RestaurantEntity> GetRestaurantAsync(int id)
    {
      var rest = await _context.Restaurants.FirstOrDefaultAsync(x => x.Id == id);
      if (rest == null)
        throw new ApiException("Неверный ID", 400);

      return rest;
    }
  }
}
