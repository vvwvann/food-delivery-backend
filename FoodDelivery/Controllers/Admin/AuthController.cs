using FoodDelivery.Exceptions;
using FoodDelivery.Models.Auth;
using FoodDelivery.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using System.Threading.Tasks;

namespace FoodDelivery.Controllers.Admin
{
  [Consumes(MediaTypeNames.Application.Json)]
  [Produces(MediaTypeNames.Application.Json)]
  [Route("api/admin/[controller]")]
  [ApiController]
  [AllowAnonymous]
  [ProducesResponseType(typeof(ApiError), 400)]
  public class AuthController : ControllerBase
  {
    private IAuthService _authService;

    public AuthController(IAuthService authService)
    {
      _authService = authService;
    }

    /// <summary>
    /// Авторизация
    /// </summary>
    [HttpPost("login")]
    [SwaggerOperation(Tags = new[] { "Admin" })]
    [ProducesResponseType(typeof(AdminLoginResponseModel), 200)]

    public async Task<IActionResult> LogIn([FromBody] AdminLogInRequestModel model)
    {
      AdminLoginResponseModel result = await _authService.LogInAsync(model);
      return Ok(result);
    }

    /// <summary>
    /// Обновить refresh-токен
    /// </summary>
    [HttpPost("refresh")]
    [SwaggerOperation(Tags = new[] { "Admin" })]
    [ProducesResponseType(typeof(RefreshResponseModel), 200)]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequestModel model)
    {
      RefreshResponseModel result = await _authService.UpdateRefreshTokenAsync(model);
      return Ok(result);
    }
  }
}
