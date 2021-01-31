using FoodDelivery.Models;
using FoodDelivery.Models.Favorites;
using System.Threading.Tasks;

namespace FoodDelivery.Services
{
  public interface IFavoriteRestaurantService
  {
    Task<GetFavoriteRestaurantsModel> GetRestaurantsAsync(string userId, PaginationModel model);
    Task AddRestaurantAsync(string userId, int restaurantId);
    Task AddRestaurantAsync(string userId, AddRestaurantsModel model);
    Task RemoveRestaurantAsync(string userId, int restaurantId);
  }
}
