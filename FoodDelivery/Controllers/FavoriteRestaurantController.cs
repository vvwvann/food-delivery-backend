using FoodDelivery.Exceptions;
using FoodDelivery.Models;
using FoodDelivery.Models.Favorites;
using FoodDelivery.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using System.Threading.Tasks;

namespace FoodDelivery.Controllers
{
  [Route("api/fav/restaurant")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(ApiError), 400)]
  [Consumes(MediaTypeNames.Application.Json)]
  [Authorize]
  [ApiController]
  public class FavoriteRestaurantController : ControllerBase
  {
    private IFavoriteRestaurantService _favoritesService;
    private const string _swagger = "Favorites (restaurants)";

    public FavoriteRestaurantController(IFavoriteRestaurantService favoritesService)
    {
      _favoritesService = favoritesService;
    }

    /// <summary>
    /// Получить список избранных ресторанов
    /// </summary>
    [HttpGet("/api/fav/restaurants")]
    [SwaggerOperation(Tags = new[] { _swagger })]
    [ProducesResponseType(typeof(GetFavoriteRestaurantsModel), 200)]
    public async Task<IActionResult> Register([FromQuery] PaginationModel model)
    {
      GetFavoriteRestaurantsModel result = await _favoritesService.GetRestaurantsAsync(User.Identity.Name, model);
      return Ok(result);
    }


    /// <summary>
    /// Добавить ресторан в избранное
    /// </summary>
    [HttpPut("{id}")]
    [SwaggerOperation(Tags = new[] { _swagger })]
    [ProducesResponseType(200)]
    public async Task<IActionResult> AddRestaurant([FromRoute] int id)
    {
      await _favoritesService.AddRestaurantAsync(User.Identity.Name, id);
      return Ok();
    }

    /// <summary>
    /// Добавить список ресторанов в избранное
    /// </summary>
    [HttpPut]
    [SwaggerOperation(Tags = new[] { _swagger })]
    [ProducesResponseType(200)]
    public async Task<IActionResult> AddRestaurants([FromBody] AddRestaurantsModel model)
    {
      await _favoritesService.AddRestaurantAsync(User.Identity.Name, model);
      return Ok();
    }

    /// <summary>
    /// Убрать ресторан из списка избранных
    /// </summary>
    [HttpDelete("{id}")]
    [SwaggerOperation(Tags = new[] { _swagger })]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ApiError), 400)]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> RemoveRestaurant([FromRoute] int id)
    {
      await _favoritesService.RemoveRestaurantAsync(User.Identity.Name, id);
      return Ok();
    }
  }
}
