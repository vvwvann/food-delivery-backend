using FoodDelivery.Exceptions;
using FoodDelivery.Models;
using FoodDelivery.Models.Admin.Users;
using FoodDelivery.Services;
using FoodDelivery.Services.Admin;
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
  [ProducesResponseType(typeof(ApiError), 400)]
  [Authorize(Roles = Roles.ADMIN)]
  public class UserController : ControllerBase
  {
    private IAdminService _adminService;

    public UserController(IAdminService adminService)
    {
      _adminService = adminService;
    }


    /// <summary>
    /// Получить список пользователей
    /// </summary>
    [HttpGet("/api/admin/users")]
    [SwaggerOperation(Tags = new[] { "Admin/Users" })]
    [ProducesResponseType(typeof(TotalResponseModel<AdminListItemUser>), 200)]
    public async Task<IActionResult> Users([FromQuery] AdminFilterModel model)
    {
      TotalResponseModel<AdminListItemUser> result = await _adminService.GetUsers(model);
      return Ok(result);
    }

    /// <summary>
    /// Добавить нового пользователя
    /// </summary>
    [HttpPost]
    [SwaggerOperation(Tags = new[] { "Admin/Users" })]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Add([FromBody] AdminAddUser model)
    {
      await _adminService.AddAsync(model);
      return Ok();
    }

    /// <summary>
    /// Обновить информацию пользователя
    /// </summary>
    [HttpPut("{id}")]
    [SwaggerOperation(Tags = new[] { "Admin/Users" })]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Update([FromRoute] string id, [FromBody] AdminAddUser model)
    {
      await _adminService.UpdateAsync(id, model);
      return Ok();
    }

    /// <summary>
    /// Удалить пользователя
    /// </summary>
    [HttpDelete("{id}")]
    [SwaggerOperation(Tags = new[] { "Admin/Users" })]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Remove([FromRoute] string id)
    {
      await _adminService.RemoveAsync(id);
      return Ok();
    }

    /// <summary>
    /// Заблокировать пользователя
    /// </summary>
    [HttpPut("{id}/block")]
    [SwaggerOperation(Tags = new[] { "Admin/Users" })]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Block([FromRoute] string id)
    {
      await _adminService.BlockAsync(id);
      return Ok();
    }

    /// <summary>
    /// Разблокировать пользователя
    /// </summary>
    [HttpPut("{id}/unblock")]
    [SwaggerOperation(Tags = new[] { "Admin/Users" })]
    [ProducesResponseType(200)]
    public async Task<IActionResult> UnBlock([FromRoute] string id)
    {
      await _adminService.UnBlockAsync(id);
      return Ok();
    }
  }
}
