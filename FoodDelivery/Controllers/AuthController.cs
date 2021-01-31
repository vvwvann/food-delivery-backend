using FoodDelivery.Exceptions;
using FoodDelivery.Models.Auth;
using FoodDelivery.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using System.Threading.Tasks;

namespace FoodDelivery.Controllers
{
  [Produces(MediaTypeNames.Application.Json)]
  [Consumes(MediaTypeNames.Application.Json)]
  [Route("api/[controller]")]
  [ApiController]
  [ProducesResponseType(typeof(ApiError), 400)]
  [AllowAnonymous]
  public class AuthController : ControllerBase
  {
    private IAuthService _authService;
    private const string _swagger = "Auth";

    public AuthController(IAuthService authService)
    {
      _authService = authService;
    }

    /// <summary>
    /// Авторизация
    /// </summary>
    [HttpPost("login")]
    [AllowAnonymous]
    [SwaggerOperation(Tags = new[] { _swagger })]
    [ProducesResponseType(typeof(LoginResponseModel), 200)]
    public async Task<object> LogIn([FromBody] LoginRequestModel model)
    {
      AdminLoginResponseModel result = await _authService.LogInAsync(model);
      return Ok(result);
    }

    /// <summary>
    /// Обновить refresh-токен
    /// </summary>
    [HttpPost("refresh")]
    [SwaggerOperation(Tags = new[] { _swagger })]
    [ProducesResponseType(typeof(RefreshResponseModel), 200)]
    public async Task<ActionResult> Refresh([FromBody] RefreshRequestModel model)
    {
      RefreshResponseModel result = await _authService.UpdateRefreshTokenAsync(model);
      return Ok(result);
    }

    /// <summary>
    /// Восстановление пароля
    /// </summary>
    [HttpPost("forgotPassword")]
    [SwaggerOperation(Tags = new[] { _swagger })]
    [ProducesResponseType(200)]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel model)
    {
      await _authService.ForgotPasswordAsync(model);
      return Ok();
    }
  }
}
