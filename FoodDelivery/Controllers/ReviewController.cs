using FoodDelivery.Data.Tables;
using FoodDelivery.Exceptions;
using FoodDelivery.Models;
using FoodDelivery.Models.Reviews;
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
  [ProducesResponseType(typeof(ApiError), 400)]
  [Consumes(MediaTypeNames.Application.Json)]
  public class ReviewController : ControllerBase
  {
    private IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
      _reviewService = reviewService;
    }


    /// <summary>
    /// Получить список доступных тегов
    /// </summary>
    [HttpGet("tags")]
    [SwaggerOperation(Tags = new[] { "Reviews" })]
    [AllowAnonymous]
    [ProducesResponseType(typeof(List<TagEntity>), 200)]
    public async Task<IActionResult> GetAllTags()
    {
      var result = await _reviewService.GetTagsAsync();
      return Ok(result);
    }

    /// <summary>
    /// Получить список отзывов
    /// </summary>
    [HttpGet("{id}")]
    [SwaggerOperation(Tags = new[] { "Reviews" })]
    [AllowAnonymous]
    [ProducesResponseType(typeof(GetAllReviewsResponseModel), 200)]
    public async Task<IActionResult> GetAll([FromRoute] int id, [FromQuery] PaginationModel model)
    {
      GetAllReviewsResponseModel result = await _reviewService.GetAllAsync(id, model);
      return Ok(result);
    }

    /// <summary>
    /// Добавить отзыв 
    /// </summary>
    [HttpPut("{id}")]
    [SwaggerOperation(Tags = new[] { "Reviews" })]
    [Authorize(Roles = Roles.CLIENT)]
    [ProducesResponseType(typeof(SuccessResponseModel), 200)]
    public async Task<IActionResult> Add([FromRoute] int id, [FromBody] AddReviewRequestModel model)
    {
      SuccessResponseModel result = await _reviewService.AddAsync(User.Identity.Name, id, model);
      return Ok(result);
    }
  }
}
