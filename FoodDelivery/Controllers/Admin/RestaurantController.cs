using FoodDelivery.Attributes;
using FoodDelivery.Exceptions;
using FoodDelivery.Models;
using FoodDelivery.Models.Admin.Restaurant;
using FoodDelivery.Models.Admin.Users;
using FoodDelivery.Services.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using System.Threading.Tasks;

namespace FoodDelivery.Controllers.Admin
{
  [Produces(MediaTypeNames.Application.Json)]
  [Consumes(MediaTypeNames.Application.Json)]
  [Route("[controller]")]
  [ApiController]
  [ProducesResponseType(typeof(ApiError), 400)]
  [AuthorizeRoles(Roles.ADMIN, Roles.MANAGER)]
  public class RestaurantController : ControllerBase
  {
    private IAdminRestaurantService _restaurantService;
    private IAdminOrderService _orderService;
    private IMenuCategoryService _menuCategoryService;

    public RestaurantController(IAdminRestaurantService restaurantService, IAdminOrderService orderService,
      IMenuCategoryService menuCategoryService)
    {
      _restaurantService = restaurantService;
      _orderService = orderService;
      _menuCategoryService = menuCategoryService;
    }

    /// <summary>
    /// Получить список ресторанов
    /// </summary>
    [HttpGet("/api/admin/restaurants")]
    [SwaggerOperation(Tags = new[] { "Admin/Restaurants" })]
    [ProducesResponseType(typeof(TotalResponseModel<AdminRestaurantItem>), 200)]
    [AuthorizeRoles(Roles.ADMIN)]
    public async Task<IActionResult> GetAll([FromQuery] AdminFilterModel model)
    {
      TotalResponseModel<AdminRestaurantItem> result = await _restaurantService.GetRestaurantsAsync(model);
      return Ok(result);
    }

    /// <summary>
    /// Получить информацию о ресторане 
    /// </summary>
    [HttpGet("{id}")]
    [SwaggerOperation(Tags = new[] { "Admin/Restaurants" })]
    [ProducesResponseType(typeof(AdminGetRestaurantModel), 200)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
      AdminGetRestaurantModel result = await _restaurantService.GetAsync(id);
      return Ok(result);
    }

    /// <summary>
    /// Получить заказы конкретного ресторана
    /// </summary>
    [HttpGet("{id}/orders")]
    [SwaggerOperation(Tags = new[] { "Admin/Restaurants" })]
    [ProducesResponseType(typeof(TotalResponseModel<AdminOrderModel>), 200)]
    public async Task<IActionResult> GetOrders([FromRoute] int id, [FromQuery] PaginationInfo model)
    {
      TotalResponseModel<AdminOrderModel> result = await _orderService.GetListAsync(model, id);
      return Ok(result);
    }

    /// <summary>
    /// Получить блюда конкретной категории меню конкретного ресторана
    /// </summary>
    [HttpGet("{id}/dishes")]
    [SwaggerOperation(Tags = new[] { "Admin/Restaurants" })]
    [ProducesResponseType(typeof(TotalResponseModel<MenuItem>), 200)]
    public async Task<IActionResult> GetDishes([FromRoute] int id, [FromQuery] int categoryId, [FromQuery] PaginationInfo model)
    {
      TotalResponseModel<MenuItem> result = await _restaurantService.GetDishes(categoryId, model);
      return Ok(result);
    }

    /// <summary>
    /// Добавить новый ресторан
    /// </summary>
    [HttpPost]
    [SwaggerOperation(Tags = new[] { "Admin/Restaurants" })]
    [ProducesResponseType(typeof(SuccessResponseModel), 200)]
    public async Task<IActionResult> Add([FromBody] AdminRestaurantModel model)
    {
      SuccessResponseModel result = await _restaurantService.AddAsync(model);
      return Ok(result);
    }

    /// <summary>
    /// Обновить информацию о ресторане
    /// </summary>
    [HttpPut("{id}")]
    [SwaggerOperation(Tags = new[] { "Admin/Restaurants" })]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] AdminRestaurantModel model)
    {
      await _restaurantService.UpdateAsync(id, model);
      return Ok();
    }

    /// <summary>
    /// Удалить ресторан
    /// </summary>
    [HttpDelete("{id}")]
    [SwaggerOperation(Tags = new[] { "Admin/Restaurants" })]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Remove([FromRoute] int id)
    {
      await _restaurantService.RemoveAsync(id);
      return Ok();
    }

    /// <summary>
    /// Заблокировать ресторан
    /// </summary>
    [HttpPut("{id}/block")]
    [SwaggerOperation(Tags = new[] { "Admin/Restaurants" })]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Block([FromRoute] int id)
    {
      await _restaurantService.BlockAsync(id);
      return Ok();
    }

    /// <summary>
    /// Разблокировать ресторан
    /// </summary>
    [HttpPut("{id}/unblock")]
    [SwaggerOperation(Tags = new[] { "Admin/Restaurants" })]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Unblock([FromRoute] int id)
    {
      await _restaurantService.UnBlockAsync(id);
      return Ok();
    }

    /// <summary>
    /// Добавить новую категорию меню ресторана
    /// </summary>
    [HttpPost("{id}/category")]
    [SwaggerOperation(Tags = new[] { "Admin/Restaurants" })]
    [ProducesResponseType(typeof(SuccessResponseModel), 200)]
    public async Task<IActionResult> AddCategory([FromRoute] int id, [FromQuery] string name)
    {
      SuccessResponseModel result = await _menuCategoryService.AddAsync(id, name);
      return Ok(result);
    }

    /// <summary>
    /// Обновить категорию меню ресторана
    /// </summary>
    [HttpPut("{id}/category")]
    [SwaggerOperation(Tags = new[] { "Admin/Restaurants" })]
    [ProducesResponseType(00)]
    public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromQuery] string name, [FromQuery] int categoryId)
    {
      await _menuCategoryService.UpdateAsync(id, categoryId, name);
      return Ok();
    }

    /// <summary>
    /// Удалить категорию меню ресторана
    /// </summary>
    [HttpDelete("{id}/category")]
    [SwaggerOperation(Tags = new[] { "Admin/Restaurants" })]
    [ProducesResponseType(200)]
    public async Task<IActionResult> RemoveCategory([FromRoute] int id, [FromQuery] int categoryId)
    {
      await _menuCategoryService.RemoveAsync(id, categoryId);
      return Ok();
    }
  }
}
