using FoodDelivery.Data.Tables;
using FoodDelivery.Models;
using FoodDelivery.Models.Reviews;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodDelivery.Services
{
  public interface IReviewService
  {
    Task<List<TagEntity>> GetTagsAsync();
    Task<GetAllReviewsResponseModel> GetAllAsync(int restaurantId, PaginationModel model);
    Task<SuccessResponseModel> AddAsync(string userId, int restaurantId, AddReviewRequestModel model);
  }
}
