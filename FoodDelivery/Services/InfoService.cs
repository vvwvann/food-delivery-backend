using FoodDelivery.Data;
using FoodDelivery.Data.Tables.Reference;
using FoodDelivery.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Services
{
  public class InfoService : IInfoService
  {
    private ApplicationDbContext _context;

    public InfoService(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<string> GetInfoAboutUs()
    {
      return await _context.Settings
        .Where(x => x.Id == (int)SettingsType.AboutUs)
        .Select(x => x.Info)
        .FirstOrDefaultAsync();
    }

    public async Task<string> GetInfoAboutApp()
    {
      return await _context.Settings
        .Where(x => x.Id == (int)SettingsType.AboutApp)
        .Select(x => x.Info)
        .FirstOrDefaultAsync();
    }

    public async Task<string> GetWorkingConditions()
    {
      return await _context.Settings
        .Where(x => x.Id == (int)SettingsType.WorkingConditions)
        .Select(x => x.Info)
        .FirstOrDefaultAsync();
    }

    public async Task<List<NewsEntity>> GetNews()
    {
      return await _context.News.ToListAsync();
    }

    public async Task<string> GetPrivacyPolicy()
    {
      return await _context.Settings
        .Where(x => x.Id == (int)SettingsType.PrivacyPolicy)
        .Select(x => x.Info)
        .FirstOrDefaultAsync();
    }

    public async Task<double> GetCostOfDelivery()
    {
      return await _context.Settings
        .Where(x => x.Id == (int)SettingsType.DeliveryCost)
        .Select(x => x.Value)
        .FirstOrDefaultAsync();
    }

    public async Task<double> GetOrderPercent()
    {
      return await _context.Settings
        .Where(x => x.Id == (int)SettingsType.OrderPercent)
        .Select(x => x.Value)
        .FirstOrDefaultAsync();
    }

    public async Task UpdateInfoAboutUs(string text)
    {
      var entity = await _context.Settings
        .FirstOrDefaultAsync(x => x.Id == (int)SettingsType.AboutUs);
      entity.Info = text;

      await _context.SaveChangesAsync();
    }

    public async Task UpdateInfoAboutApp(string text)
    {
      var entity = await _context.Settings
        .FirstOrDefaultAsync(x => x.Id == (int)SettingsType.AboutApp);
      entity.Info = text;

      await _context.SaveChangesAsync();
    }

    public async Task UpdateWorkingConditions(string text)
    {
      var entity = await _context.Settings
        .FirstOrDefaultAsync(x => x.Id == (int)SettingsType.WorkingConditions);
      entity.Info = text;

      await _context.SaveChangesAsync();
    }

    public async Task UpdatePrivacyPolicy(string text)
    {
      var entity = await _context.Settings
       .FirstOrDefaultAsync(x => x.Id == (int)SettingsType.PrivacyPolicy);
      entity.Info = text;

      await _context.SaveChangesAsync();
    }

    public async Task UpdateCostOfDelivery(int id)
    {
      var entity = await _context.Settings
       .FirstOrDefaultAsync(x => x.Id == (int)SettingsType.DeliveryCost);
      entity.Value = id;

      await _context.SaveChangesAsync();
    }

    public async Task UpdateOrderPercent(double percent)
    {
      var entity = await _context.Settings
       .FirstOrDefaultAsync(x => x.Id == (int)SettingsType.OrderPercent);
      entity.Value = percent;

      await _context.SaveChangesAsync();
    }
  }
}
