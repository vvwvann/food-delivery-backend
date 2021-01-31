using FoodDelivery.Data;
using FoodDelivery.Data.Tables;
using FoodDelivery.Exceptions;
using FoodDelivery.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodDelivery.Services.Admin
{
  public class MenuService
  {
    private ApplicationDbContext _context;

    public MenuService(ApplicationDbContext context)
    {
      _context = context;
    }
    public async Task<List<MenuCategoryEntity>> GetAll(int id)
    {
      var rest = await _context.Restaurants
       .AsNoTracking()
       .Include(x => x.Menu)
       .FirstOrDefaultAsync(x => x.Id == id);

      if (rest == null) throw new ApiException("Ресторан не найден", 400);

      return rest.Menu;
    }

    public async Task<SuccessResponseModel> AddAsync(int restId, string name)
    {
      await CheckIfExistAsync(restId, name);

      MenuCategoryEntity menuCategory = new MenuCategoryEntity {
        RestaurantId = restId,
        Name = name
      };

      await _context.MenuCategories.AddAsync(menuCategory);
      await _context.SaveChangesAsync();

      return new SuccessResponseModel { Id = menuCategory.Id };
    }

    public async Task UpdateAsync(int restId, int categoryId, string name)
    {
      await CheckIfExistAsync(restId, name);
      var entity = await GetCategoryAsync(restId, categoryId);
      entity.Name = name;

      await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(int restId, int categoryId)
    {
      var entity = await GetCategoryAsync(restId, categoryId);
      _context.MenuCategories.Remove(entity);

      await _context.SaveChangesAsync();
    }


    private async Task<MenuCategoryEntity> GetCategoryAsync(int restId, int categoryId)
    {
      var entity = await _context.MenuCategories.FirstOrDefaultAsync(x => x.Id == categoryId && x.RestaurantId == restId);

      if (entity == null) throw new ApiException("Категория не найдена", 400);

      return entity;
    }

    private async Task CheckIfExistAsync(int restId, string name)
    {
      var entity = await _context.MenuCategories.FirstOrDefaultAsync(x => x.Name == name && x.RestaurantId == restId);

      if (entity == null) throw new ApiException("Категория с таким именем уже существует", 400);
    }
  }

}
