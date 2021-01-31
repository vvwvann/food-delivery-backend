using FoodDelivery.Models;
using FoodDelivery.Models.Admin.Restaurant;
using FoodDelivery.Models.Admin.Users;
using System.Threading.Tasks;

namespace FoodDelivery.Services.Admin
{
  public interface IAdminRestaurantService
  {
    Task<TotalResponseModel<AdminRestaurantItem>> GetRestaurantsAsync(AdminFilterModel model);
    Task<TotalResponseModel<MenuItem>> GetDishes(int categoryId, PaginationInfo model);
    Task<AdminGetRestaurantModel> GetAsync(int id);
    Task<SuccessResponseModel> AddAsync(AdminRestaurantModel model);

    Task UpdateAsync(int id, AdminRestaurantModel model);
    Task RemoveAsync(int id);
    Task BlockAsync(int id);
    Task UnBlockAsync(int id);
  }
}
