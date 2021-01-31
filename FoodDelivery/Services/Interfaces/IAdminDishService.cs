using FoodDelivery.Models;
using FoodDelivery.Models.Admin.Users;
using System.Threading.Tasks;

namespace FoodDelivery.Services.Admin
{
  public interface IAdminDishService
  {
    Task<SuccessResponseModel> AddAsync(AdminAddDishModel model);
    Task UpdateAsync(int id, AdminUpdateDishModel model);
    Task RemoveAsync(int id);
    Task BlockAsync(int id);
    Task UnBlockAsync(int id);
  }
}
