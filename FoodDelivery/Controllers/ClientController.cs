using FoodDelivery.Exceptions;
using FoodDelivery.Models;
using FoodDelivery.Models.Auth;
using FoodDelivery.Models.Data;
using FoodDelivery.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using System.Threading.Tasks;

namespace FoodDelivery.Controllers
{
  [Route("api/[controller]")]
  [Produces(MediaTypeNames.Application.Json)]
  [Consumes(MediaTypeNames.Application.Json)]
  [ApiController]
  [Authorize(Roles = Roles.CLIENT)]
  [ProducesResponseType(typeof(ApiError), 400)]
  public class ClientController : ControllerBase
  {
    private IUserService _userService;
    private IAuthService _authService;
    private const string _swagger = "User";

    public ClientController(IUserService userService, IAuthService authService)
    {
      _userService = userService;
      _authService = authService;
    }

    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    /// <remarks>
    /// Password must have at least one lowercase ('a'-'z') and one uppercase ('A'-'Z').
    /// It must be at least 6 and at max 40 characters long. 
    /// </remarks>
    [HttpPost("register")]
    [SwaggerOperation(Tags = new[] { _swagger })]
    [AllowAnonymous]
    [ProducesResponseType(typeof(LoginResponseModel), 200)]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
      LoginResponseModel result = await _authService.RegisterAsync(model, Roles.CLIENT);
      return Ok(result);
    }

    /// <summary>
    /// Домашняя страница
    /// </summary>
    [HttpGet("home")]
    [AllowAnonymous]
    [SwaggerOperation(Tags = new[] { _swagger })]
    [ProducesResponseType(typeof(IndexResponseModel), 200)]
    public async Task<IActionResult> Index()
    {
      IndexResponseModel result = await _userService.GetHomePage(User.Identity.Name);
      return Ok(result);
    }

    /// <summary>
    /// Получить личные данные пользователя
    /// </summary>
    [Authorize(Roles = Roles.CLIENT)]
    [HttpGet("data")]
    [SwaggerOperation(Tags = new[] { _swagger })]
    [ProducesResponseType(typeof(PersonalDataResponseModel), 200)]
    public async Task<IActionResult> GetData()
    {
      PersonalDataResponseModel result = await _userService.GetPersonalDataAsync(User.Identity.Name);
      return Ok(result);
    }

    /// <summary>
    /// Обновить личные данные пользователя
    /// </summary>
    [HttpPut("data")]
    [SwaggerOperation(Tags = new[] { _swagger })]
    [ProducesResponseType(typeof(PersonalDataResponseModel), 200)]
    public async Task<IActionResult> UpdateData([FromBody] PersonalDataRequestModel model)
    {
      PersonalDataResponseModel result = await _userService.UpdatePersonalDataAsync(User.Identity.Name, model);
      return Ok(result);
    }
  }
}
