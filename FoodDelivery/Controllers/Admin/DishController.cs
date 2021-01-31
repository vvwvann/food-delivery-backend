using FoodDelivery.Attributes;
using FoodDelivery.Exceptions;
using FoodDelivery.Models;
using FoodDelivery.Models.Admin.Users;
using FoodDelivery.Services.Admin;
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
  [AuthorizeRoles(Roles.ADMIN, Roles.MANAGER)]
  [ProducesResponseType(typeof(ApiError), 400)]
  public class DishController : ControllerBase
  {
    private IAdminDishService _dishService;

    public DishController(IAdminDishService dishService)
    {
      _dishService = dishService;
    }

    /// <summary>
    /// Добавить новое блюдо
    /// </summary>
    [HttpPost]
    [SwaggerOperation(Tags = new[] { "Admin/Dishes" })]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Add([FromBody] AdminAddDishModel model)
    {
      SuccessResponseModel result = await _dishService.AddAsync(model);
      return Ok(result);
    }

    /// <summary>
    /// Обновить информацию о блюде
    /// </summary>
    [HttpPut("{id}")]
    [SwaggerOperation(Tags = new[] { "Admin/Dishes" })]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] AdminUpdateDishModel model)
    {
      await _dishService.UpdateAsync(id, model);
      return Ok();
    }

    /// <summary>
    /// Удалить блюдо
    /// </summary>
    [HttpDelete("{id}")]
    [SwaggerOperation(Tags = new[] { "Admin/Dishes" })]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Remove([FromRoute] int id)
    {
      await _dishService.RemoveAsync(id);
      return Ok();
    }

    /// <summary>
    /// Заблокировать блюдо
    /// </summary>
    [HttpPut("{id}/block")]
    [SwaggerOperation(Tags = new[] { "Admin/Dishes" })]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Block([FromRoute] int id)
    {
      await _dishService.BlockAsync(id);
      return Ok();
    }

    /// <summary>
    /// Разблокировать блюдо
    /// </summary>
    [HttpPut("{id}/unblock")]
    [SwaggerOperation(Tags = new[] { "Admin/Dishes" })]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Unblock([FromRoute] int id)
    {
      await _dishService.UnBlockAsync(id);
      return Ok();
    }
  }
}
