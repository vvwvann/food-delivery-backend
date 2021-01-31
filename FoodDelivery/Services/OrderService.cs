using FoodDelivery.Data;
using FoodDelivery.Data.Tables;
using FoodDelivery.Exceptions;
using FoodDelivery.Models;
using FoodDelivery.Models.Order;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Services
{
  public class OrderService : IOrderService
  {
    private ApplicationDbContext _context;
    private IStatisticsService _statisticsService;
    private Random _rnd = new Random();

    public OrderService(ApplicationDbContext context, IStatisticsService statisticsService)
    {
      _context = context;
      _statisticsService = statisticsService;
    }

    public async Task<List<OrderDeliveryTypeEntity>> GetDeliveryTypesAsync()
    {
      return await _context.OrderDeliveryTypes.ToListAsync();
    }

    public async Task<List<OrderPaymentTypeEntity>> GetPaymentTypesAsync()
    {
      return await _context.OrderPaymentTypes.ToListAsync();
    }

    public async Task<List<OrderStatusEntity>> GetStatusesAsync()
    {
      return await _context.OrderStatuses.ToListAsync();
    }

    public async Task<OrderResponseModel> GetAsync(string userId, int id)
    {
      var order = await GetOrderAsync(id);

      var dishes = new List<BasketResponseItem>();
      var items = await _context.OrderDishes.Include(x => x.Dish).Where(x => x.OrderId == id).ToListAsync();

      foreach (var item in items) {
        dishes.Add(new BasketResponseItem {
          Id = item.DishId,
          Count = item.Count,
          Photo = item.Dish.Photo?[0],
          Price = item.Sum / item.Count
        });
      }

      OrderResponseModel result = new OrderResponseModel {
        Id = order.Id,
        Sum = order.Sum,
        DeliveryMinutes = order.DeliveryMinutes,
        DeliverySum = order.DeliverySum,
        StatusId = order.StatusId,
        Address = new AddressEntity {
          ApartmentNumber = order.Address.ApartmentNumber,
          City = order.Address.City,
          HouseNumber = order.Address.HouseNumber,
          Street = order.Address.Street
        },
        Dishes = dishes
      };

      return result;
    }

    private async Task<int> GetRandomId()
    {
      int id = _rnd.Next(1000, 10000);
      var tmp = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
      while (tmp != null) {
        id = _rnd.Next(1000, 10000);
        tmp = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
      }
      return id;
    }

    public async Task<SuccessResponseModel> ConfirmAsync(string userId, OrderModel model)
    {
      if (model.Dishes == null || model.Dishes.Count == 0)
        throw new ApiException("Блюда не найдено", 400);

      OrderEntity order = new OrderEntity {
        Id = await GetRandomId(),
        PaymentTypeId = model.PaymentTypeId,
        DeliveryTypeId = model.DeliveryTypeId,
        DeliveryMinutes = model.DeliveryMinutes,
        DeliverySum = model.DeliverySum,
        Tip = model.Tip,
        UserId = userId,
        AddressId = model.AddressId,
        RestaurantId = (await _context.Dishes.FirstOrDefaultAsync(x => x.Id == model.Dishes[0].Id)).RestaurantId
      };

      await _context.Orders.AddAsync(order);

      decimal totalSum = 0;

      foreach (var dish in model.Dishes) {
        var entity = await _context.Dishes.FirstOrDefaultAsync(d => d.Id == dish.Id);
        if (entity == null) throw new ApiException("Блюдо не найдено", 400);
        if (entity.IsBlock) throw new ApiException("Блюдо заблокировано. Удалите блюдо из корзины", 400);
        if (entity.Price != dish.Sum / dish.Count) throw new ApiException("Цена блюда изменилась", 400);
        if (entity.RestaurantId != order.RestaurantId) throw new ApiException("Невозможно cделать заказ cблюдами из разных ресторанов", 400);

        OrderDishEntity od = new OrderDishEntity {
          DishId = dish.Id,
          Count = dish.Count,
          Sum = dish.Sum,
          Order = order
        };

        totalSum += dish.Sum;

        //if (dish.Description != null)
        //  od.Description.Add(dish.Description);

        await _context.OrderDishes.AddAsync(od);
      }

      order.Sum = totalSum;
      await _statisticsService.UpdateOrdersCountAsync(totalSum);
      await _context.SaveChangesAsync();

      return new SuccessResponseModel { Id = order.Id };
    }

    public async Task<TotalResponseModel<OrderEntity>> GetAllAsync(string userId, PaginationModel model)
    {
      var query = _context.Orders.AsNoTracking().Where(x => x.UserId == userId);

      var date = model.Id > 0 ? await
       query.Select(x => x.CreatedAt)
       .FirstOrDefaultAsync() : DateTime.MaxValue;

      var orders = await _context.Orders
        .AsNoTracking()
        .Where(order => order.UserId == userId && order.CreatedAt < date)
        .OrderByDescending(x => x.CreatedAt)
        .Take(model.Count)
        .ToListAsync();

      return new TotalResponseModel<OrderEntity> {
        Total = await query.CountAsync(),
        Items = orders
      };
    }

    public async Task CancelAsync(string userId, int orderId)
    {
      var order = await GetOrderAsync(orderId);

      if (order.StatusId != (int)OrderStatus.NEW)
        throw new ApiException("Невозможно отменить заказ", 400);

      order.StatusId = (int)OrderStatus.CANCEL;
      await _context.SaveChangesAsync();
    }

    private async Task<OrderEntity> GetOrderAsync(int id)
    {
      var order = await _context.Orders.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == id);
      if (order == null)
        throw new ApiException("Заказ не найден", 400);

      return order;
    }
  }
}
