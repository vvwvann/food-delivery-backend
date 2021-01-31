using AutoMapper;
using FoodDelivery.Data;
using FoodDelivery.Data.Tables;
using FoodDelivery.Exceptions;
using FoodDelivery.Extensions;
using FoodDelivery.Models;
using FoodDelivery.Models.Admin.Orders;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Services.Admin
{
  public class AdminOrderService : IAdminOrderService
  {
    private ApplicationDbContext _context;
    private IMapper _mapper;

    public AdminOrderService(ApplicationDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task<TotalResponseModel<AdminOrderModel>> GetListAsync(PaginationInfo model, int? restaurantId = null)
    {
      var query = _context.Orders.Include(x => x.Manager).Where(x => restaurantId == null || x.RestaurantId == restaurantId);

      return new TotalResponseModel<AdminOrderModel> {
        Total = await query.CountAsync(),
        Items = _mapper.Map<List<AdminOrderModel>>(await query.PaginateAsync(model))
      };
    }

    public async Task<AdminOrderReponseModel> GetAsync(int id)
    {
      return _mapper.Map<AdminOrderReponseModel>(await GetOrderAsync(id));
    }


    public async Task UpdateAsync(int id, AdminUpdateOrderModel model)
    {
      var order = await GetOrderAsync(id);

      if (order.StatusId != model.StatusId) {
        order.StatusId = model.StatusId;
        // NOTIFICATION
      }

      await _context.SaveChangesAsync();
    }

    private async Task<OrderEntity> GetOrderAsync(int id)
    {
      var order = await _context.Orders.Include(x => x.Manager).Include(x => x.Dishes).FirstOrDefaultAsync(x => x.Id == id);
      if (order == null) throw new ApiException("Заказ не найден", 400);

      return order;
    }
  }
}
