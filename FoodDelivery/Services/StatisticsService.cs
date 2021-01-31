using FoodDelivery.Data;
using FoodDelivery.Data.Tables.User;
using FoodDelivery.Extensions;
using FoodDelivery.Models;
using FoodDelivery.Models.Admin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Services
{
  public class StatisticsService : IStatisticsService
  {
    private ApplicationDbContext _context;

    public StatisticsService(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<AdminGetStatisticsModel> GetAsync(DateFilterModel model)
    {
      var query = _context.Statistics.Where(x => x.Date < model.From && x.Date > model.To);
      return new AdminGetStatisticsModel {
        Total = await query.CountAsync(),
        Items = await query.OrderByDescending(x => x.Date).PaginateAsync(model)
      };
    }

    public async Task UpdateRegistersCountAsync()
    {
      var entity = await GetEntryAsync();
      entity.RegisterCount += 1;

      await _context.SaveChangesAsync();
    }

    public async Task UpdateOrdersCountAsync(decimal sum)
    {
      var entity = await GetEntryAsync();
      entity.OrdersCount += 1;
      entity.TotalSum += sum;

      await _context.SaveChangesAsync();
    }

    private async Task<StatisticEntity> GetEntryAsync()
    {
      var entity = await _context.Statistics.FirstOrDefaultAsync(x => x.Date == DateTime.UtcNow);
      if (entity == null) {
        entity = new StatisticEntity();
        await _context.Statistics.AddAsync(entity);
      }

      return entity;
    }
  }
}
