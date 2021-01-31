using AutoMapper;
using FoodDelivery.Data;
using FoodDelivery.Data.Tables;
using FoodDelivery.Exceptions;
using FoodDelivery.Models.Order;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Services
{
  public class BasketService : IBasketService
  {
    private ApplicationDbContext _context;
    private IMapper _mapper;

    public BasketService(ApplicationDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task<BasketResponseModel> GetAsync(string userId)
    {
      var items = await _context.BasketDishes
        .Include(x => x.Dish)
        .Where(x => x.UserId == userId)
        .ToListAsync();

      return new BasketResponseModel {
        Items = _mapper.Map<BasketResponseItem[]>(items)
      };
    }

    public async Task AddItemAsync(string userId, BasketItemModel model)
    {
      foreach (var dish in model.Dishes) {
        var entity = await _context.Dishes.FirstOrDefaultAsync(x => x.Id == dish.Id);
        if (entity.IsBlock)
          throw new ApiException("Блюдо заблокировано", 400);

        var bd = await _context.BasketDishes.FirstOrDefaultAsync(x => x.UserId == userId);
        if (bd != null && bd.RestaurantId != entity.RestaurantId)
          throw new ApiException("Невозможно добавить блюдо. В корзине уже есть блюда из другого ресторана.", 403);

        var basketDish = await _context.BasketDishes.FirstOrDefaultAsync(x => x.UserId == userId && x.DishId == dish.Id);
        if (basketDish == null) {
          basketDish = new BasketDishEntity(userId, dish.Id, entity.MenuCategory.RestaurantId);
          await _context.BasketDishes.AddAsync(basketDish);
        }

        basketDish.Count += dish.Count;
      }

      await _context.SaveChangesAsync();
    }


    public async Task RemoveItemAsync(string userId, RemoveBasketItemModel model)
    {
      foreach (var dish in model.Dishes) {
        var entity = await _context.BasketDishes.FirstOrDefaultAsync(x => x.DishId == dish.Id);
        if (entity == null)
          throw new ApiException("Неверный ID блюда", 400);

        entity.Count -= dish.Count;
        if (entity.Count == 0) {
          _context.BasketDishes.Remove(entity);
        }
      }

      await _context.SaveChangesAsync();
    }
  }
}
