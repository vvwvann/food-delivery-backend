using FoodDelivery.Models.Fondy;
using System.Threading.Tasks;

namespace FoodDelivery.Services.Interfaces
{
  public interface IFondyResponseHandler
  {
    Task HandleServerCallbackAsync(FondyResponseModel responseModel);
    Task HandleChargebackCallbackAsync();
  }
}
