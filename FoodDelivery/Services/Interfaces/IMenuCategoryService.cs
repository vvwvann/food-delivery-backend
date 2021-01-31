using FoodDelivery.Data.Tables;
using FoodDelivery.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodDelivery.Services.Admin
{
  public interface IMenuCategoryService
  {
    Task<List<MenuCategoryEntity>> GetAll(int id);

    Task<SuccessResponseModel> AddAsync(int restId, string name);
    Task UpdateAsync(int restId, int categoryId, string name);
    Task RemoveAsync(int restId, int categoryId);
  }

}
