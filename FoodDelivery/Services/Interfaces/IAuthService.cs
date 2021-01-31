using FoodDelivery.Models.Auth;
using System.Threading.Tasks;

namespace FoodDelivery.Services
{
  public interface IAuthService
  {
    Task<AdminLoginResponseModel> LogInAsync(AdminLogInRequestModel model);
    Task<RefreshResponseModel> UpdateRefreshTokenAsync(RefreshRequestModel model);
    Task<LoginResponseModel> RegisterAsync(RegisterModel model, string role);
    Task ForgotPasswordAsync(ForgotPasswordModel model);
  }
}
