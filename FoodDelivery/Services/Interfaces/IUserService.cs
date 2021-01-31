using FoodDelivery.Data.Tables;
using FoodDelivery.Models;
using FoodDelivery.Models.Data;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FoodDelivery.Services
{
  public interface IUserService
  { 
    Task<PersonalDataResponseModel> GetPersonalDataAsync(string userId);
    Task<PersonalDataResponseModel> UpdatePersonalDataAsync(string userId, PersonalDataRequestModel model);
    Task<PathResponseModel> UploadFileAsync(Stream stream, string mime);
    Task<IndexResponseModel> GetHomePage(string userId);

  }
}
