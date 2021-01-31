using FoodDelivery.Exceptions;
using FoodDelivery.Models;
using FoodDelivery.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using System.Threading.Tasks;

namespace FoodDelivery.Controllers
{
  [Produces(MediaTypeNames.Application.Json)]
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class UserController : ControllerBase
  {
    private IUserService _userService;

    public UserController(IUserService userService)
    {
      _userService = userService;
    }


    /// <summary>
    /// Загрузить файл
    /// </summary>
    /// <remarks>  
    ///
    /// Content-Type: */*
    /// 
    /// body: BINARY_FILE_DATA
    /// </remarks>
    [HttpPost("/api/files/upload")]
    [SwaggerOperation(Tags = new[] { "User", "Admin" })]
    [ProducesResponseType(typeof(PathResponseModel), 200)]
    [ProducesResponseType(typeof(ApiError), 400)]
    public async Task<IActionResult> Upload()
    {
      if (Request.ContentLength == 0)
        throw new ApiException("Файл не найден", 400);

      PathResponseModel result = await _userService.UploadFileAsync(Request.Body, Request.ContentType);
      return Ok(result);
    }
  }
}
