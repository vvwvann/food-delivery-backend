using AutoMapper;
using FoodDelivery.Data;
using FoodDelivery.Data.Tables;
using FoodDelivery.Enums;
using FoodDelivery.Exceptions;
using FoodDelivery.Extensions;
using FoodDelivery.Models;
using FoodDelivery.Models.Admin.Orders;
using FoodDelivery.Models.Admin.Restaurant;
using FoodDelivery.Models.Admin.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FoodDelivery.Services.Admin
{
  public class AdminRestaurantService : IAdminRestaurantService
  {
    private ApplicationDbContext _context;
    private IMapper _mapper;

    public AdminRestaurantService(ApplicationDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task<TotalResponseModel<AdminRestaurantItem>> GetRestaurantsAsync(AdminFilterModel model)
    {
      string text = model.Search?.Trim();

      if (!string.IsNullOrEmpty(text))
        text = $"%{text}%";

      var query = _context.Restaurants
        .AsNoTracking()
        .Where(x => string.IsNullOrEmpty(model.Search)
        || EF.Functions.ILike(x.Name, text));

      Expression<Func<RestaurantEntity, object>> orderExp = x => x.Id;
      orderExp = model.Sort switch {
        "name" => x => x.Name,
        "popular" => x => x.Rating,
        _ => x => x.Id
      };

      query = model.Sort == "popular" ? query.OrderByDescending(orderExp)
       : query.OrderBy(orderExp);

      var restaurants = await query.PaginateAsync(model);

      return new TotalResponseModel<AdminRestaurantItem> {
        Total = await query.CountAsync(),
        Items = _mapper.Map<List<AdminRestaurantItem>>(restaurants)
      };
    }


    public async Task<TotalResponseModel<MenuItem>> GetDishes(int categoryId, PaginationInfo model)
    {
      var dishes = await _context.Dishes
               .AsNoTracking()
               .Where(x => x.MenuCategoryId == categoryId)
               .PaginateAsync(model);

      var result = new List<MenuItem>();

      foreach (var dish in dishes) {
        var dishItem = new MenuItem {
          Id = dish.Id,
          Name = dish.Name,
          Description = dish.Description,
          CuisineId = dish.CuisineId.Value,
          Ingridients = dish.Ingridients,
          Minutes = dish.Minutes,
          Photo = dish.Photo,
          Price = dish.Price,
          MenuCategoryId = dish.MenuCategoryId,
          Additionals = new List<MenuDishModel>()
        };

        var additionals = new List<MenuDishModel>();

        foreach (var item in dish.Additionals) {
          var add = item.Additional;

          additionals.Add(new MenuDishModel {
            Id = item.AdditionalId,
            Name = add.Name,
            Description = add.Description,
            Ingridients = add.Ingridients,
            Photo = add.Photo,
            Price = add.Price,
            CuisineId = add.CuisineId.Value,
            MenuCategoryId = add.MenuCategoryId,
            Minutes = add.Minutes
          });
        }

        result.Add(dishItem);
      }

      return new TotalResponseModel<MenuItem> {
        Total = await _context.Dishes
        .Where(x => x.MenuCategoryId == categoryId).CountAsync(),
        Items = result
      };
    }

    public async Task<AdminGetRestaurantModel> GetAsync(int id)
    {
      var rest = await GetRestaurantWithIncludesAsync(id);

      var result = _mapper.Map<AdminGetRestaurantModel>(rest);
      result.DeliveryTypes = rest.Deliveries.Select(x => x.DeliveryId).ToArray();
      result.Сuisines = rest.Cuisines.Select(x => x.CuisineId).ToArray();
      result.Managers = _mapper.Map<AdminManagerModel[]>(rest.Managers);

      return result;
    }

    public async Task UpdateAsync(int id, AdminRestaurantModel model)
    {
      var rest = await GetRestaurantWithIncludesAsync(id);
      rest = _mapper.Map(model, rest);

      await AddCuisines(rest.Id, rest.Cuisines, model.Cuisines);
      await AddDeliveries(rest.Id, rest.Deliveries, model.DeliveryTypes);
      await AddManagers(rest.Id, rest.Managers, model.Managers);

      await SetWidgetType(model.IsNew, rest.IsNew, id, WidgetType.New);
      await SetWidgetType(model.IsPopular, rest.IsPopular, id, WidgetType.Popular);
      await SetWidgetType(model.FreeDelivery, rest.FreeDelivery, id, WidgetType.FreeDelivery);

      await _context.SaveChangesAsync();
    }

    public async Task<SuccessResponseModel> AddAsync(AdminRestaurantModel model)
    {
      RestaurantEntity rest = _mapper.Map<RestaurantEntity>(model);

      await _context.Restaurants.AddAsync(rest);
      await _context.SaveChangesAsync();

      await AddCuisines(rest.Id, rest.Cuisines, model.Cuisines);
      await AddDeliveries(rest.Id, rest.Deliveries, model.DeliveryTypes);
      await AddManagers(rest.Id, rest.Managers, model.Managers);

      await SetWidgetType(model.IsNew, rest.IsNew, rest.Id, WidgetType.New);
      await SetWidgetType(model.IsPopular, rest.IsPopular, rest.Id, WidgetType.Popular);
      await SetWidgetType(model.FreeDelivery, rest.FreeDelivery, rest.Id, WidgetType.FreeDelivery);

      await _context.SaveChangesAsync();

      return new SuccessResponseModel { Id = rest.Id };
    }

    public async Task RemoveAsync(int id)
    {
      var rest = await GetRestaurantAsync(id);

      _context.Restaurants.Remove(rest);
      await _context.SaveChangesAsync();
    }

    public async Task BlockAsync(int id)
    {
      var rest = await GetRestaurantAsync(id);

      rest.IsBlock = true;
      await _context.SaveChangesAsync();
    }

    public async Task UnBlockAsync(int id)
    {
      var rest = await GetRestaurantAsync(id);

      rest.IsBlock = false;
      await _context.SaveChangesAsync();
    }

    private async Task<RestaurantEntity> GetRestaurantAsync(int id)
    {
      var rest = await _context.Restaurants.FirstOrDefaultAsync(x => x.Id == id);
      if (rest == null) throw new ApiException("Ресторан не найден", 400);

      return rest;
    }

    private async Task<RestaurantEntity> GetRestaurantWithIncludesAsync(int id)
    {
      var rest = await _context.Restaurants
        .Include(x => x.Cuisines)
        .Include(x => x.Managers)
        .Include(x => x.Address)
        .FirstOrDefaultAsync(x => x.Id == id);
      if (rest == null) throw new ApiException("Ресторан не найден", 400);

      return rest;
    }

    private async Task SetWidgetType(bool newValue, bool oldValue, int restId, WidgetType widget)
    {
      if (oldValue == newValue) return;

      var entity = await _context.WidgetRestaurants.FirstOrDefaultAsync(x => x.WidgetId == (int)widget && x.RestaurantId == restId);

      if (newValue) {
        if (entity != null) return;

        await _context.WidgetRestaurants.AddAsync(new WidgetRestaurantEntity {
          RestaurantId = restId,
          WidgetId = (int)widget
        });
      }
      else {
        if (entity == null) return;

        _context.WidgetRestaurants.Remove(entity);
      }
    }

    private async Task AddCuisines(int restId, List<RestaurantСuisineEntity> oldRestaurantСuisines, List<int> newRestaurantСuisines)
    {
      if (oldRestaurantСuisines != null)
        _context.RestaurantCategories.RemoveRange(oldRestaurantСuisines);

      if (newRestaurantСuisines != null) {
        foreach (var item in newRestaurantСuisines) {
          await _context.AddAsync(new RestaurantСuisineEntity {
            CuisineId = item,
            RestaurantId = restId
          });
        }
      }
    }

    private async Task AddDeliveries(int restId, List<RestaurantDeliveryEntity> oldRestaurantDeliveries, List<int> newRestaurantDeliveries)
    {
      if (oldRestaurantDeliveries != null)
        _context.RestaurantDeliveries.RemoveRange(oldRestaurantDeliveries);

      if (newRestaurantDeliveries != null) {
        foreach (var item in newRestaurantDeliveries) {
          await _context.AddAsync(new RestaurantDeliveryEntity {
            DeliveryId = item,
            RestaurantId = restId
          });
        }
      }
    }

    private async Task AddManagers(int restId, List<ApplicationUser> oldManagers, List<string> newManagers)
    {
      if (oldManagers != null) {
        foreach (var item in oldManagers) {
          item.ManagedRestaurantId = null;
        }
      }

      if (newManagers != null) {
        foreach (var item in newManagers) {
          var manager = await _context.Users.FirstOrDefaultAsync(x => x.Id == item);
          manager.ManagedRestaurantId = restId;
        }
      }
    }
  }

}
