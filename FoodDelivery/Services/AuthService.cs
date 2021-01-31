using FoodDelivery.Models.Auth;
using Microsoft.AspNetCore.Identity;
using FoodDelivery.Data.Tables;
using FoodDelivery.Exceptions;
using FoodDelivery.Helpers;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace FoodDelivery.Services
{
  public class AuthService : IAuthService
  {
    private UserManager<ApplicationUser> _userManager;
    private SessionService _sessionService;
    private IConnectionService _connectionService;
    private IStatisticsService _statisticsService;
    private IMapper _mapper;

    public AuthService(SessionService sessionService,
      UserManager<ApplicationUser> userManager,
      IConnectionService connectionService,
      IStatisticsService statisticsService,
      IMapper mapper)
    {
      _userManager = userManager;
      _sessionService = sessionService;
      _connectionService = connectionService;
      _statisticsService = statisticsService;
      _mapper = mapper;
    }


    public async Task<LoginResponseModel> RegisterAsync(RegisterModel model, string role)
    {
      if (model.Password != model.ConfirmPassword)
        throw new ApiException("Пароли не совпадают", 400);

      ApplicationUser user = _mapper.Map<ApplicationUser>(model);

      var result = await _userManager.CreateAsync(user, model.Password);

      if (result.Succeeded) {
        result = await _userManager.AddToRoleAsync(user, role);
        if (result.Succeeded) {
          var token = JWTHelper.Create(user.Id, role, out long expiresIn);
          var refresh = await _sessionService.CreateAsync(user.Id);

          await _connectionService.AddAsync(user.Id, model.PlayerId);
          await _statisticsService.UpdateRegistersCountAsync();

          return new LoginResponseModel(user, token, refresh, expiresIn, Roles.CLIENT);
        }
      }

      throw new ApiException(result.Errors.First().Description, 400);
    }


    public async Task<AdminLoginResponseModel> LogInAsync(AdminLogInRequestModel model)
    {
      await _sessionService.DeleteAsync();

      ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);
      if (user == null) throw new ApiException("Неверный email", 400);

      bool ok = await _userManager.CheckPasswordAsync(user, model.Password);
      if (!ok) throw new ApiException("Неверный пароль", 400);

      string role = (await _userManager.GetRolesAsync(user)).First();
      string token = JWTHelper.Create(user.Id, role, out long expiresIn);
      Guid refresh = await _sessionService.CreateAsync(user.Id);

      if (model is LoginRequestModel clientModel) {
        await _connectionService.AddAsync(user.Id, clientModel.PlayerId);
      }

      return new AdminLoginResponseModel(user, token, refresh, expiresIn, role, user.ManagedRestaurantId);
    }

    public async Task<RefreshResponseModel> UpdateRefreshTokenAsync(RefreshRequestModel model)
    {
      return await _sessionService.UpdateAsync(model.RefreshToken);
    }

    public async Task ForgotPasswordAsync(ForgotPasswordModel model)
    {
      var user = await _userManager.FindByEmailAsync(model.Email);
      if (user == null) throw new ApiException("Неверный email", 400);

      string code = await _userManager.GeneratePasswordResetTokenAsync(user);
      string password = GeneratePassword();
      var result = await _userManager.ResetPasswordAsync(user, code, password);

      if (result.Succeeded) {
        //MailService emailService = new MailService();
        //bool ok = emailService.SendMessage(user.Email, "Восстановление пароля", $"Новый пароль: " + password);
        //return ok ? new ResultModel() : new ResultModel { Error = "Произошла ошибка при отправке сообщения." };
      }

      throw new ApiException(result.Errors?.First()?.Description, 400);
    }

    private string GeneratePassword()
    {
      var options = _userManager.Options.Password;

      int length = options.RequiredLength;

      bool nonAlphanumeric = options.RequireNonAlphanumeric;
      bool digit = options.RequireDigit;
      bool lowercase = options.RequireLowercase;
      bool uppercase = options.RequireUppercase;

      StringBuilder password = new StringBuilder();
      Random random = new Random();

      while (password.Length < 8) {
        char c = (char)random.Next(32, 126);
        if (!char.IsLetter(c))
          continue;
        password.Append(c);
      }

      if (nonAlphanumeric)
        password.Append((char)random.Next(33, 48));
      if (digit)
        password.Append((char)random.Next(48, 58));
      if (lowercase)
        password.Append((char)random.Next(97, 123));
      if (uppercase)
        password.Append((char)random.Next(65, 91));

      return password.ToString();
    }

  }
}
