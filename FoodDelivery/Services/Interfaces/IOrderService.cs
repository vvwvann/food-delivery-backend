using FoodDelivery.Data.Tables;
using FoodDelivery.Models;
using FoodDelivery.Models.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodDelivery.Services
{
  public interface IOrderService
  {
    Task<TotalResponseModel<OrderEntity>> GetAllAsync(string userId, PaginationModel model);
    Task<SuccessResponseModel> ConfirmAsync(string userId, OrderModel model);
    Task CancelAsync(string userId, int orderId);
    Task<List<OrderDeliveryTypeEntity>> GetDeliveryTypesAsync();
    Task<List<OrderPaymentTypeEntity>> GetPaymentTypesAsync();
    Task<List<OrderStatusEntity>> GetStatusesAsync();
    Task<OrderResponseModel> GetAsync(string userId, int id);
  }
}
