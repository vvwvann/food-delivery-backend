using FoodDelivery.Models;
using FoodDelivery.Models.Favorites;
using System.Threading.Tasks;

namespace FoodDelivery.Services
{
  public interface IFavoriteDishService
  {
    Task<GetFavoriteDishesModel> GetAsync(string userId, PaginationModel model);
    Task AddAsync(string userId, int dishId);
    Task AddAsync(string userId, int[] dishIds);
    Task RemoveAsync(string userId, int dishId);
  }
}
