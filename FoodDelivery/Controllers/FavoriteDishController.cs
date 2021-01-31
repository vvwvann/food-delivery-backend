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
  [Route("api/fav/dish")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(ApiError), 400)]
  [Consumes(MediaTypeNames.Application.Json)]
  [Authorize]
  [ApiController]
  public class FavoriteDishController : ControllerBase
  {
    private IFavoriteDishService _favoritesService;
    private const string _swagger = "Favorites (dishes)";

    public FavoriteDishController(IFavoriteDishService favoritesService)
    {
      _favoritesService = favoritesService;
    }

    /// <summary>
    /// Получить список избранных блюд
    /// </summary>
    [HttpGet("/api/fav/dishes")]
    [SwaggerOperation(Tags = new[] { _swagger })]
    [ProducesResponseType(typeof(GetFavoriteDishesModel), 200)]
    public async Task<IActionResult> Dishes([FromQuery] PaginationModel model)
    {
      GetFavoriteDishesModel result = await _favoritesService.GetAsync(User.Identity.Name, model);
      return Ok(result);
    }


    /// <summary>
    /// Добавить блюдо в избранное
    /// </summary>
    [HttpPut("{id}")]
    [SwaggerOperation(Tags = new[] { _swagger })]
    [ProducesResponseType(200)]
    public async Task<IActionResult> AddRDish([FromRoute] int id)
    {
      await _favoritesService.AddAsync(User.Identity.Name, id);
      return Ok();
    }

    /// <summary>
    /// Добавить список блюд в избранное
    /// </summary>
    [HttpPut]
    [SwaggerOperation(Tags = new[] { _swagger })]
    [ProducesResponseType(200)]
    public async Task<IActionResult> AddRDishes([FromBody] AddRestaurantsModel model)
    {
      await _favoritesService.AddAsync(User.Identity.Name, model.Items);
      return Ok();
    }

    /// <summary>
    /// Убрать блюдо из списка избранных
    /// </summary>
    [HttpDelete("{id}")]
    [SwaggerOperation(Tags = new[] { _swagger })]
    [ProducesResponseType(200)]
    public async Task<IActionResult> RemoveDish([FromRoute] int id)
    {
      await _favoritesService.RemoveAsync(User.Identity.Name, id);
      return Ok();
    }
  }
}
