using FoodDelivery.Models;
using FoodDelivery.Models.Order;
using System.Threading.Tasks;

namespace FoodDelivery.Services
{
  public interface IBasketService
  {
    Task RemoveItemAsync(string userId, RemoveBasketItemModel model);
    Task AddItemAsync(string userId, BasketItemModel model);
    Task<BasketResponseModel> GetAsync(string userId);
  }
}
