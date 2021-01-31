using FoodDelivery.Models;
using FoodDelivery.Models.Admin;
using System.Threading.Tasks;

namespace FoodDelivery.Services
{
  public interface IStatisticsService
  {
    Task<AdminGetStatisticsModel> GetAsync(DateFilterModel model);
    Task UpdateRegistersCountAsync();
    Task UpdateOrdersCountAsync(decimal sum);
  }
}
