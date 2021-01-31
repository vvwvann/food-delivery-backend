using FoodDelivery.Exceptions;
using FoodDelivery.Models;
using FoodDelivery.Models.Admin;
using FoodDelivery.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using System.Threading.Tasks;

namespace FoodDelivery.Controllers.Admin
{
  [Consumes(MediaTypeNames.Application.Json)]
  [Produces(MediaTypeNames.Application.Json)]
  [Route("api/[controller]")]
  [ApiController]
  [ProducesResponseType(typeof(ApiError), 400)]
  [Authorize(Roles = Roles.ADMIN)]
  public class AdminController : ControllerBase
  {
    private IStatisticsService _statisticsService;

    public AdminController(IStatisticsService statisticsService)
    {
      _statisticsService = statisticsService;
    }

    /// <summary>
    /// Получить статистику
    /// </summary>
    [HttpGet("statistics")]
    [SwaggerOperation(Tags = new[] { "Admin" })]
    [ProducesResponseType(typeof(AdminGetStatisticsModel), 200)]
    public async Task<IActionResult> Statistics([FromQuery] DateFilterModel model)
    {
      AdminGetStatisticsModel result = await _statisticsService.GetAsync(model);
      return Ok(result);
    }

  }
}
