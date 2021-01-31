using FoodDelivery.Exceptions;
using FoodDelivery.Models;
using FoodDelivery.Models.Data;
using FoodDelivery.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using System.Threading.Tasks;

namespace FoodDelivery.Controllers
{
  [Route("api/user/[controller]")]
  [Produces(MediaTypeNames.Application.Json)]
  [Consumes(MediaTypeNames.Application.Json)]
  [ApiController]
  [Authorize]
  [ProducesResponseType(typeof(ApiError), 400)]
  public class AddressController : ControllerBase
  {
    private IAddressService _addressService;
    private const string _swagger = "Address";

    public AddressController(IAddressService addressService)
    {
      _addressService = addressService;
    }

    /// <summary>
    /// Получить список адресов
    /// </summary>
    [HttpGet("/api/user/addresses")]
    [SwaggerOperation(Tags = new[] { _swagger})]
    [ProducesResponseType(typeof(GetAllAddressesModel), 200)]
    public async Task<IActionResult> GetAllAdresses()
    {
      GetAllAddressesModel result = await _addressService.GetAllAsync(User.Identity.Name);
      return Ok(result);
    }

    /// <summary>
    /// Добавить адрес
    /// </summary>
    [HttpPost]
    [SwaggerOperation(Tags = new[] { _swagger })]
    [ProducesResponseType(typeof(SuccessResponseModel), 200)]
    public async Task<IActionResult> GetAllAdresses([FromBody] AddressModel model)
    {
      SuccessResponseModel result = await _addressService.AddAsync(User.Identity.Name, model);
      return Ok(result);
    }

    /// <summary>
    /// Удалить адрес
    /// </summary>
    [HttpDelete("{id}")]
    [SwaggerOperation(Tags = new[] { _swagger })]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Remove([FromRoute] int id)
    {
      await _addressService.RemoveAsync(User.Identity.Name, id);
      return Ok();
    }

    /// <summary>
    /// Типы доставки
    /// </summary>
    [HttpGet("/api/address/delivery/types")]
    [SwaggerOperation(Tags = new[] { _swagger })]
    [ProducesResponseType(typeof(DeliveryTypesResponseModel), 200)]
    public async Task<IActionResult> GetDeliveryTypes()
    {
      DeliveryTypesResponseModel result = await _addressService.GetDeliveryTypes();
      return Ok(result);
    }
  }
}
