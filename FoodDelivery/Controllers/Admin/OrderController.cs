using FoodDelivery.Attributes;
using FoodDelivery.Exceptions;
using FoodDelivery.Models;
using FoodDelivery.Models.Admin.Orders;
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
  public class OrderController : ControllerBase
  {
    private IAdminOrderService _orderService;

    public OrderController(IAdminOrderService orderService)
    {
      _orderService = orderService;
    }

    /// <summary>
    /// Получить список заказов
    /// </summary>
    [HttpGet("/api/admin/orders")]
    [SwaggerOperation(Tags = new[] { "Admin/Order" })]
    [ProducesResponseType(typeof(TotalResponseModel<AdminOrderModel>), 200)]
    [Authorize(Roles = Roles.ADMIN)]
    public async Task<IActionResult> GetOrders([FromQuery] PaginationInfo model)
    {
      TotalResponseModel<AdminOrderModel> result = await _orderService.GetListAsync(model);
      return Ok(result);
    }

    /// <summary>
    /// Получить конкретный заказ
    /// </summary>
    [HttpGet("{id}")]
    [SwaggerOperation(Tags = new[] { "Admin/Order" })]
    [ProducesResponseType(typeof(AdminOrderReponseModel), 200)]
    [AuthorizeRoles(Roles.ADMIN, Roles.MANAGER)]
    public async Task<IActionResult> GetOrder([FromRoute] int id)
    {
      AdminOrderReponseModel result = await _orderService.GetAsync(id);
      return Ok(result);
    }

    /// <summary>
    /// Обновить информацию о заказе
    /// </summary>
    [HttpPut("{id}")]
    [SwaggerOperation(Tags = new[] { "Admin/Order" })]
    [AuthorizeRoles(Roles.ADMIN, Roles.MANAGER)]
    [ProducesResponseType(200)]
    public async Task<IActionResult> UpdateOrder([FromRoute] int id, [FromBody] AdminUpdateOrderModel model)
    {
      await _orderService.UpdateAsync(id, model);
      return Ok();
    }
  }
}
