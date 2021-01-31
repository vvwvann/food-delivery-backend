using FoodDelivery.Models;
using FoodDelivery.Models.Admin.Orders;
using System.Threading.Tasks;

namespace FoodDelivery.Services.Admin
{
  public interface IAdminOrderService
  {
    Task<TotalResponseModel<AdminOrderModel>> GetListAsync(PaginationInfo model, int? restaurantId = null);
    Task UpdateAsync(int id, AdminUpdateOrderModel model);
    Task<AdminOrderReponseModel> GetAsync(int id);
  }
}
