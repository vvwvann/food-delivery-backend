using FoodDelivery.Data;
using FoodDelivery.Data.Tables;
using FoodDelivery.Exceptions;
using FoodDelivery.Models;
using FoodDelivery.Models.Reviews;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static FoodDelivery.Models.Reviews.GetAllReviewsResponseModel;

namespace FoodDelivery.Services
{
  public class ReviewService : IReviewService
  {
    private ApplicationDbContext _context;

    public ReviewService(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<List<TagEntity>> GetTagsAsync()
    {
      return await _context.Tags.ToListAsync();
    }

    public async Task<GetAllReviewsResponseModel> GetAllAsync(int restaurantId, PaginationModel model)
    {
      var restaurant = await GetRestaurantAsync(restaurantId);

      var date = model.Id > 0 ? await _context.Reviews
        .Where(x => x.Id == model.Id)
        .Select(x => x.CreatedAt)
        .FirstOrDefaultAsync() : DateTime.MaxValue;

      var reviews = await _context.Reviews
          .Include(x => x.User)
          .Include(x => x.Tags)
          .Where(review => review.RestaurantId == restaurantId && review.CreatedAt < date)
          .OrderByDescending(x => x.CreatedAt)
          .Take(model.Count)
          .ToListAsync();

      List<ReviewItem> result = new List<ReviewItem>();

      foreach (var item in reviews) {
        result.Add(new ReviewItem {
          Id = item.Id,
          Text = item.Text,
          Rating = item.Rating,
          CreatedAt = item.CreatedAt,
          Tags = item.Tags.Select(x => x.TagId).ToList(),
          User = new ReviewItem.UserReviewItem {
            Id = item.UserId,
            Name = item.User.Name,
            Photo = item.User.Photo
          }
        });
      }

      return new GetAllReviewsResponseModel {
        Items = result,
        Total = await _context.Reviews
          .Where(review => review.RestaurantId == restaurantId)
          .CountAsync()
      };
    }

    public async Task<SuccessResponseModel> AddAsync(string userId, int restaurantId, AddReviewRequestModel model)
    {
      var restaurant = await GetRestaurantAsync(restaurantId);

      ReviewEntity entity = new ReviewEntity {
        Rating = model.Rating,
        RestaurantId = restaurantId,
        Text = model.Text,
        UserId = userId
      };

      await _context.Reviews.AddAsync(entity);

      restaurant.Rating = await _context.Reviews
        .Where(x => x.RestaurantId == restaurantId)
        .AverageAsync(x => x.Rating); 

      await _context.ReviewTags.AddRangeAsync(await AssignTagsToReview(entity.Id, model.Tags));
      await _context.SaveChangesAsync();

      return new SuccessResponseModel {
        Id = entity.Id
      };
    }

    private async Task<List<ReviewTagEntity>> AssignTagsToReview(int reviewId, List<int> tags)
    {
      var list = new List<ReviewTagEntity>(); 

      foreach (var item in tags) {
        var rt = await _context.ReviewTags.FirstOrDefaultAsync(x => x.TagId == item && x.ReviewId == reviewId);
        if (rt == null) {
          list.Add(new ReviewTagEntity {
            ReviewId = reviewId,
            TagId = item
          });
        }
      }

      return list;
    }

    private async Task<RestaurantEntity> GetRestaurantAsync(int id)
    {
      var restaurant = await _context.Restaurants.FirstOrDefaultAsync(x => x.Id == id);
      if (restaurant == null) throw new ApiException("Ресторан не найден", 400);
      if (restaurant.IsBlock) throw new ApiException("Ресторан заблокирован", 400);

      return restaurant;
    }
  }
}
