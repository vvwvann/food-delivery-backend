using FoodDelivery.Models;
using FoodDelivery.Models.Data;
using System.Threading.Tasks;

namespace FoodDelivery.Services
{
  public interface IAddressService
  {
    Task<GetAllAddressesModel> GetAllAsync(string userId);
    Task<SuccessResponseModel> AddAsync(string userId, AddressModel address);
    Task RemoveAsync(string userId, int addressId);
    Task<DeliveryTypesResponseModel> GetDeliveryTypes();
  }
}
