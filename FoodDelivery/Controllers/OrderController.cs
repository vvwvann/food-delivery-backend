using FoodDelivery.Data.Tables;
using FoodDelivery.Exceptions;
using FoodDelivery.Models;
using FoodDelivery.Models.Order;
using FoodDelivery.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;


namespace FoodDelivery.Controllers
{
  [Produces(MediaTypeNames.Application.Json)]
  [Route("api/user/[controller]")]
  [ApiController]
  [ProducesResponseType(typeof(ApiError), 400)]
  [Consumes(MediaTypeNames.Application.Json)]
  [Authorize]
  public class OrderController : ControllerBase
  {
    private IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
      _orderService = orderService;
    }


    /// <summary>
    /// Получить список типов доставки
    /// </summary>
    [HttpGet("/api/delivery/types")]
    [SwaggerOperation(Tags = new[] { "Order", "Admin/Order", "Admin/Restaurants" })]
    [ProducesResponseType(typeof(List<OrderDeliveryTypeEntity>), 200)]
    [AllowAnonymous]
    public async Task<IActionResult> GetDeliveryTypes()
    {
      var result = await _orderService.GetDeliveryTypesAsync();
      return Ok(result);
    }

    /// <summary>
    /// Получить список типов оплаты
    /// </summary>
    [HttpGet("/api/payment/types")]
    [SwaggerOperation(Tags = new[] { "Order", "Admin/Order" })]
    [ProducesResponseType(typeof(List<OrderPaymentTypeEntity>), 200)]
    [AllowAnonymous]
    public async Task<IActionResult> GetPaymentTypes()
    {
      var result = await _orderService.GetPaymentTypesAsync();
      return Ok(result);
    }

    /// <summary>
    /// Получить список статусов заказов
    /// </summary>
    [HttpGet("/api/order/statuses")]
    [SwaggerOperation(Tags = new[] { "Order", "Admin/Order" })]
    [ProducesResponseType(typeof(List<OrderStatusEntity>), 200)]
    [AllowAnonymous]
    public async Task<IActionResult> GetStatuses()
    {
      var result = await _orderService.GetStatusesAsync();
      return Ok(result);
    }


    /// <summary>
    /// Список заказов
    /// </summary>
    [HttpGet("/api/user/orders")]
    [SwaggerOperation(Tags = new[] { "Order" })]
    [ProducesResponseType(typeof(TotalResponseModel<OrderEntity>), 200)]
    public async Task<IActionResult> GetAll([FromQuery] PaginationModel model)
    {
      TotalResponseModel<OrderEntity> result = await _orderService.GetAllAsync(User.Identity.Name, model);
      return Ok(result);
    }


    /// <summary>
    /// Отменить заказ
    /// </summary>
    [HttpPut("{id}/cancel")]
    [SwaggerOperation(Tags = new[] { "Order" })]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Cancel([FromRoute] int id)
    {
      await _orderService.CancelAsync(User.Identity.Name, id);
      return Ok();
    }

    /// <summary>
    /// Получить информацию о заказе
    /// </summary>
    [HttpGet("{id}")]
    [SwaggerOperation(Tags = new[] { "Order" })]
    [ProducesResponseType(typeof(OrderResponseModel), 200)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
      OrderResponseModel result = await _orderService.GetAsync(User.Identity.Name, id);
      return Ok(result); 
    }
  }
}
