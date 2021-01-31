using FoodDelivery.Data.Tables.Reference;
using FoodDelivery.Exceptions;
using FoodDelivery.Models;
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
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  [ProducesResponseType(typeof(ApiError), 400)]
  public class InfoController : ControllerBase
  {
    private IInfoService _infoService;

    public InfoController(IInfoService infoService)
    {
      _infoService = infoService;
    }

    /// <summary>
    /// Работа у нас
    /// </summary>
    [HttpGet("working/conditions")]
    [SwaggerOperation(Tags = new[] { "Info", "Admin/Settings" })]
    [Authorize]
    [ProducesResponseType(typeof(string), 200)]
    public async Task<IActionResult> Work()
    {
      string result = await _infoService.GetWorkingConditions();
      return Ok(result);
    }

    /// <summary>
    /// Обновить Работа у нас
    /// </summary>
    [HttpPut("working/conditions")]
    [SwaggerOperation(Tags = new[] { "Admin/Settings" })]
    [Authorize(Roles = Roles.ADMIN)]
    [ProducesResponseType(200)]
    public async Task<IActionResult> UpdateWorkingConditions(InfoRequestModel model)
    {
      await _infoService.UpdateWorkingConditions(model.Text);
      return Ok();
    }

    /// <summary>
    /// О приложении
    /// </summary>
    [HttpGet("about/app")]
    [SwaggerOperation(Tags = new[] { "Info", "Admin/Settings" })]
    [Authorize]
    [ProducesResponseType(typeof(string), 200)]
    public async Task<IActionResult> AboutApp()
    {
      string result = await _infoService.GetInfoAboutApp();
      return Ok(result);
    }

    /// <summary>
    /// Обновить О приложении
    /// </summary>
    [HttpPut("about/app")]
    [SwaggerOperation(Tags = new[] { "Admin/Settings" })]
    [Authorize(Roles = Roles.ADMIN)]
    [ProducesResponseType(200)]
    public async Task<IActionResult> UpdateInfoAboutApp(InfoRequestModel model)
    {
      await _infoService.UpdateInfoAboutApp(model.Text);
      return Ok();
    }

    /// <summary>
    /// О нас
    /// </summary>
    [HttpGet("about/us")]
    [SwaggerOperation(Tags = new[] { "Info", "Admin/Settings" })]
    [Authorize]
    [ProducesResponseType(typeof(string), 200)]
    public async Task<IActionResult> AboutUs()
    {
      string result = await _infoService.GetInfoAboutUs();
      return Ok(result);
    }

    /// <summary>
    /// Обновить О нас
    /// </summary>
    [HttpPut("about/us")]
    [SwaggerOperation(Tags = new[] { "Admin/Settings" })]
    [Authorize(Roles = Roles.ADMIN)]
    [ProducesResponseType(200)]
    public async Task<IActionResult> UpdateInfoAboutUs(InfoRequestModel model)
    {
      await _infoService.UpdateInfoAboutUs(model.Text);
      return Ok();
    }

    /// <summary>
    /// Новости
    /// </summary>
    [HttpGet("news")]
    [SwaggerOperation(Tags = new[] { "Info", "Admin/Settings" })]
    [Authorize]
    [ProducesResponseType(typeof(List<NewsEntity>), 200)]
    public async Task<IActionResult> GetNews()
    {
      var result = await _infoService.GetNews();
      return Ok(result);
    }

    /// <summary>
    /// Политика конфиденциальности
    /// </summary>
    [HttpGet("privacy")]
    [SwaggerOperation(Tags = new[] { "Info", "Admin/Settings" })]
    [Authorize]
    [ProducesResponseType(typeof(string), 200)]
    public async Task<IActionResult> GetPrivacyPolicy()
    {
      var result = await _infoService.GetPrivacyPolicy();
      return Ok(result);
    }


    /// <summary>
    /// Обновить политику конфиденциальности
    /// </summary>
    [HttpPut("privacy")]
    [SwaggerOperation(Tags = new[] { "Admin/Settings" })]
    [Authorize(Roles = Roles.ADMIN)]
    [ProducesResponseType(200)]
    public async Task<IActionResult> UpdatePrivacyPolicy(InfoRequestModel model)
    {
      await _infoService.UpdatePrivacyPolicy(model.Text);
      return Ok();
    }

    /// <summary>
    /// Получить процент, взимаемый сервисом за каждый заказ
    /// </summary>
    [HttpGet("order/percent")]
    [SwaggerOperation(Tags = new[] { "Admin/Settings" })]
    [Authorize(Roles = Roles.ADMIN)]
    [ProducesResponseType(typeof(string), 200)]
    public async Task<IActionResult> GetOrderPercent()
    {
      var result = await _infoService.GetOrderPercent();
      return Ok(result);
    }


    /// <summary>
    /// Обновить процент, взимаемый сервисом за каждый заказ
    /// </summary>
    [HttpPut("order/percent")]
    [SwaggerOperation(Tags = new[] { "Admin/Settings" })]
    [Authorize(Roles = Roles.ADMIN)]
    [ProducesResponseType(200)]
    public async Task<IActionResult> UpdateOrderPercent([FromQuery] double percent)
    {
      await _infoService.UpdateOrderPercent(percent);
      return Ok();
    }
  }

}
