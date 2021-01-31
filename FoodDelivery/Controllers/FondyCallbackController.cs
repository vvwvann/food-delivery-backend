using FoodDelivery.Models.Fondy;
using FoodDelivery.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FoodDelivery.Controllers
{
  [Route("Fondy/Callback")]
  [ApiController]
  public class FondyCallbackController : ControllerBase
  {
    private readonly IFondyResponseHandler _responseHandler;

    public FondyCallbackController(IFondyResponseHandler responseHandler)
    {
      _responseHandler = responseHandler;
    }

    [HttpPost("server")]
    public async Task<IActionResult> ReceiveServerCallbackAsync([FromBody] FondyResponseModel responseModel)
    {
        await _responseHandler.HandleServerCallbackAsync(responseModel);
        return Ok();
    }

    [HttpPost("chargeback")]
    public async Task<IActionResult> ReceiveChargebackCallback()
    {
      await _responseHandler.HandleChargebackCallbackAsync();

      return Ok();
    }
  }
}
