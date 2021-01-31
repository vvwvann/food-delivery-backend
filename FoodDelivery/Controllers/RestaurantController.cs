using FoodDelivery.Data.Tables;
using FoodDelivery.Exceptions;
using FoodDelivery.Models;
using FoodDelivery.Models.Restaurants;
using FoodDelivery.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using static FoodDelivery.Models.Restaurants.GetAllRestaurantsResponseModel;

namespace FoodDelivery.Controllers
{
  [Produces(MediaTypeNames.Application.Json)]
  [Route("api/[controller]")]
  [ApiController]
  [ProducesResponseType(typeof(ApiError), 400)]
  [Consumes(MediaTypeNames.Application.Json)]
  [Authorize]
  public class RestaurantController : ControllerBase
  {
    private IRestaurantService _restaurantService;

    public RestaurantController(IRestaurantService restaurantService)
    {
      _restaurantService = restaurantService;
    }

    /// <summary>
    /// Получить карту ресторанов 
    /// </summary>
    [HttpGet("/api/map/restaurants")]
    [SwaggerOperation(Tags = new[] { "Restaurants" })]
    [ProducesResponseType(typeof(GetAllRestaurantsResponseModel), 200)]
    public async Task<IActionResult> GetMap([FromQuery] MapRestaurantPaginationModel model)
    {
      GetAllRestaurantsResponseModel result = await _restaurantService.GetMapAsync(User.Identity.Name, model);
      return Ok(result);
    }

    /// <summary>
    /// Получить список ресторанов
    /// </summary>
    [HttpGet("/api/restaurants")]
    [SwaggerOperation(Tags = new[] { "Restaurants" })]
    [ProducesResponseType(typeof(GetAllRestaurantsResponseModel), 200)]
    public async Task<IActionResult> GetAll([FromQuery] RestaurantPaginationModel model)
    {
      GetAllRestaurantsResponseModel result = await _restaurantService.GetAllAsync(User.Identity.Name, model);
      return Ok(result);
    }

    /// <summary>
    /// Получить информацию о ресторане
    /// </summary>
    [HttpGet("{id}")]
    [SwaggerOperation(Tags = new[] { "Restaurants" })]
    [Authorize(Roles = Roles.CLIENT)]
    [ProducesResponseType(typeof(RestaurantItem), 200)]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
      RestaurantItem result = await _restaurantService.GetByIdAsync(User.Identity.Name, id);
      return Ok(result);
    }

    /// <summary>
    /// Получить список типов кухни 
    /// </summary>
    [HttpGet("categories")]
    [SwaggerOperation(Tags = new[] { "Restaurants", "Admin/Restaurants" })]
    [Authorize(Roles = Roles.CLIENT)]
    [ProducesResponseType(typeof(List<CuisineEntity>), 200)]
    public async Task<IActionResult> Categories([FromRoute] int id)
    {
      List<CuisineEntity> result = await _restaurantService.GetCategoriesAsync();
      return Ok(result);
    }
  }
}
