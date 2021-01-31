using FoodDelivery.Data.Tables;
using FoodDelivery.Models;
using FoodDelivery.Models.Restaurants;
using System.Collections.Generic;
using System.Threading.Tasks;
using static FoodDelivery.Models.Restaurants.GetAllRestaurantsResponseModel;

namespace FoodDelivery.Services
{
  public interface IRestaurantService
  {
    Task<GetAllRestaurantsResponseModel> GetAllAsync(string userId, RestaurantPaginationModel model);
    Task<RestaurantItem> GetByIdAsync(string userId, int id);
    Task<List<CuisineEntity>> GetCategoriesAsync();
    Task<GetAllRestaurantsResponseModel> GetMapAsync(string userId, MapRestaurantPaginationModel model);
  }
}
