using FoodDelivery.Data.Tables.Reference;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodDelivery.Services
{
  public interface IInfoService
  {
    Task<string> GetInfoAboutUs();
    Task<string> GetInfoAboutApp();
    Task<string> GetWorkingConditions();
    Task<List<NewsEntity>> GetNews();
    Task<double> GetCostOfDelivery();
    Task<double> GetOrderPercent();
    Task<string> GetPrivacyPolicy();

    Task UpdateOrderPercent(double percent);
    Task UpdateCostOfDelivery(int id);
    Task UpdatePrivacyPolicy(string text);
    Task UpdateWorkingConditions(string text);
    Task UpdateInfoAboutApp(string text);
    Task UpdateInfoAboutUs(string text);
  }
}
