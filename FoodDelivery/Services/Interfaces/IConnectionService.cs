using System.Threading.Tasks;

namespace FoodDelivery.Services
{
  public interface IConnectionService
  {
    Task AddAsync(string userId, string id);
  }
}
