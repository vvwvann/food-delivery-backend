using AutoMapper;
using FoodDelivery.Data;
using FoodDelivery.Data.Tables;
using FoodDelivery.Exceptions;
using FoodDelivery.Models;
using FoodDelivery.Models.Admin.Users;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodDelivery.Services.Admin
{
  public class AdminDishService : IAdminDishService
  {
    private ApplicationDbContext _context;
    private IMapper _mapper;

    public AdminDishService(ApplicationDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task<SuccessResponseModel> AddAsync(AdminAddDishModel model)
    {
      DishEntity dish = _mapper.Map<DishEntity>(model);

      await _context.Dishes.AddAsync(dish);
      await _context.SaveChangesAsync();

      await AddAdditionals(dish.Id, dish.Additionals, model.Additionals);

      return new SuccessResponseModel { Id = dish.Id };
    }

    private async Task AddAdditionals(int dishId, List<DishAdditionalEntity> old, List<int> newAdditionals)
    {
      if (old != null)
        _context.DishAdditionals.RemoveRange(old);

      if (newAdditionals != null) {
        foreach (var item in newAdditionals) {
          await _context.DishAdditionals.AddAsync(new DishAdditionalEntity {
            DishId = dishId,
            AdditionalId = item
          });
        }
      }

      await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, AdminUpdateDishModel model)
    {
      var dish = await _context.Dishes.FirstOrDefaultAsync(x => x.Id == id);
      if (dish == null) throw new ApiException("Блюдо не найдено", 400);

      dish = _mapper.Map<DishEntity>(model);
      await AddAdditionals(dish.Id, dish.Additionals, model.Additionals);
    }

    public async Task RemoveAsync(int id)
    {
      var dish = await GetDishAsync(id);

      _context.Dishes.Remove(dish);
      await _context.SaveChangesAsync();
    }

    public async Task BlockAsync(int id)
    {
      var dish = await GetDishAsync(id);

      dish.IsBlock = true;
      await _context.SaveChangesAsync();
    }

    public async Task UnBlockAsync(int id)
    {
      var dish = await GetDishAsync(id);

      dish.IsBlock = false;
      await _context.SaveChangesAsync();
    }

    private async Task<DishEntity> GetDishAsync(int id)
    {
      var dish = await _context.Dishes.FirstOrDefaultAsync(x => x.Id == id);
      if (dish == null) throw new ApiException("Блюдо не найдено", 400);
   
      return dish;
    }
  }
}
