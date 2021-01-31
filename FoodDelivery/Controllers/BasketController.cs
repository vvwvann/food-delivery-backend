using FoodDelivery.Exceptions;
using FoodDelivery.Models.Order;
using FoodDelivery.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using System.Threading.Tasks;


namespace FoodDelivery.Controllers
{
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(ApiError), 400)]
  [Consumes(MediaTypeNames.Application.Json)]
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class BasketController : ControllerBase
  {
    private IBasketService _basketService;
    private const string _swagger = "Basket";

    public BasketController(IBasketService basketService)
    {
      _basketService = basketService;
    }

    /// <summary>
    /// Получить корзину
    /// </summary>
    [HttpGet]
    [SwaggerOperation(Tags = new[] { _swagger })]
    [ProducesResponseType(typeof(BasketResponseModel), 200)]
    public async Task<IActionResult> Get()
    {
      BasketResponseModel result = await _basketService.GetAsync(User.Identity.Name);
      return Ok(result);
    }


    /// <summary>
    /// Добавить блюда в корзину
    /// </summary>
    [HttpPost]
    [SwaggerOperation(Tags = new[] { _swagger })]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Add([FromBody] BasketItemModel model)
    {
      await _basketService.AddItemAsync(User.Identity.Name, model);
      return Ok();
    }

    /// <summary>
    /// Удалить блюда из корзины
    /// </summary>
    [HttpDelete]
    [SwaggerOperation(Tags = new[] { _swagger })]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Remove([FromBody] RemoveBasketItemModel model)
    {
      await _basketService.RemoveItemAsync(User.Identity.Name, model);
      return Ok();
    }
  }
}
