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

namespace FoodDelivery.Services
{
  public class FavoriteDishService : IFavoriteDishService
  {
    private ApplicationDbContext _context;
    private IMapper _mapper;

    public FavoriteDishService(ApplicationDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task<GetFavoriteDishesModel> GetAsync(string userId, PaginationModel model)
    {
      var date = model.Id > 0 ? await _context.FavoriteDishes
        .AsNoTracking()
        .Where(x => x.UserId == userId)
        .Select(x => x.CreatedAt)
        .FirstOrDefaultAsync() : DateTime.MaxValue;

      var favorites = await _context.FavoriteDishes
        .AsNoTracking()
        .Include(x => x.Dish)
        .Where(x => x.UserId == userId && x.CreatedAt < date)
        .OrderByDescending(x => x.CreatedAt)
        .Take(model.Count)
        .ToListAsync();

      return new GetFavoriteDishesModel {
        Total = await _context.FavoriteDishes.Where(x => x.UserId == userId).CountAsync(),
        Items = _mapper.Map<DishListItem[]>(favorites)
      };
    }

    public async Task AddAsync(string userId, int dishId)
    {
      var dish = await GetDishAsync(dishId);

      var fav = await _context.FavoriteDishes.FirstOrDefaultAsync(x => x.DishId == dishId && x.UserId == userId);
      if (fav != null) return;

      await _context.FavoriteDishes.AddAsync(new FavoriteDishEntity(userId, dishId));
      await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(string userId, int dishId)
    {
      var fav = await _context.FavoriteDishes.FirstOrDefaultAsync(x => x.DishId == dishId && x.UserId == userId);
      if (fav == null) return;

      _context.FavoriteDishes.Remove(fav);
      await _context.SaveChangesAsync();
    }

    public async Task AddAsync(string userId, int[] dishIds)
    {
      var favs = new List<FavoriteDishEntity>();
      foreach (var id in dishIds) {
        await GetDishAsync(id);

        var fav = await _context.FavoriteDishes.FirstOrDefaultAsync(x => x.DishId == id && x.UserId == userId);
        if (fav == null) {
          favs.Add(new FavoriteDishEntity(userId, id));
        }
      }

      await _context.FavoriteDishes.AddRangeAsync(favs);
      await _context.SaveChangesAsync();
    }

    private async Task<DishEntity> GetDishAsync(int id)
    {
      var dish = await _context.Dishes.FirstOrDefaultAsync(x => x.Id == id);
      if (dish == null)
        throw new ApiException("Неверный ID блюда", 400);

      return dish;
    }
  }
}
