using AutoMapper;
using FoodDelivery.Data;
using FoodDelivery.Data.Tables;
using FoodDelivery.Enums;
using FoodDelivery.Exceptions;
using FoodDelivery.Helpers;
using FoodDelivery.Models;
using FoodDelivery.Models.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static FoodDelivery.Models.Restaurants.GetAllRestaurantsResponseModel;

namespace FoodDelivery.Services
{
  public class UserService : IUserService
  {
    private ApplicationDbContext _context;
    private UserManager<ApplicationUser> _userManager;
    private IMapper _mapper;

    public UserService(ApplicationDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
    {
      _context = context;
      _userManager = userManager;
      _mapper = mapper;
    }


    private async Task<List<IndexResponseModel.WidgetItem>> GetWidgets(string userId)
    {
      var widgets = await _context.Widgets.OrderBy(x => x.Id).ToListAsync();
      var result = new List<IndexResponseModel.WidgetItem>();
      var user = await _context.Users.Include(x => x.Addresses).FirstOrDefaultAsync(x => x.Id == userId);

      Point point = user.Addresses.Count == 0 ? null : new Point(user.Addresses[0].Latitude, user.Addresses[0].Longitude) { SRID = 4326 };

      foreach (var widget in widgets) {
        int count = widget.Id == (int)WidgetType.Popular ? 3 : 6;

        var list = await _context.WidgetRestaurants
            .Where(x => x.WidgetId == widget.Id && !x.Restaurant.IsBlock)
            .Include(x => x.Restaurant)
            .ThenInclude(x => x.Reviews)
            .Select(x => x.Restaurant)
            .Take(count)
            .ToListAsync();

        if (list.Count < count) {
          var query = _context.Restaurants.Include(x => x.Reviews);

          IQueryable<RestaurantEntity> range = widget.Id switch {
            (int)WidgetType.Popular => query.OrderByDescending(x => x.Rating),
            (int)WidgetType.New => query.OrderByDescending(x => x.CreatedAt),
            (int)WidgetType.FreeDelivery => query.Where(x => x.FreeDelivery == true),
            (int)WidgetType.Near => point == null ? query : query.Where(c => c.Address.Location.Distance(point) < 1000)
          };

          list.AddRange(range.Take(count - list.Count).AsEnumerable());
        }

        List<RestaurantListItem> res = new List<RestaurantListItem>();

        foreach (var item in list) {
          res.Add(new RestaurantListItem {
            Id = item.Id,
            Delivery = item.Delivery,
            Name = item.Name,
            Photo = item.Photo,
            Rating = new RestaurantItem.RatingItem {
              Total = item.Reviews.Count(),
              Value = item.Rating,
            },
            IsFavorite = userId != null && (await _context.FavoriteRestaurants.FirstOrDefaultAsync(x => x.RestaurantId == item.Id && x.UserId == userId) != null),
            Cuisines = await _context.RestaurantCategories.Where(x => x.RestaurantId == item.Id).Select(x => x.CuisineId).ToListAsync()
          });
        }

        result.Add(new IndexResponseModel.WidgetItem {
          Id = widget.Id,
          Name = widget.Name,
          Restaurants = res
        });
      }

      return result;
    }


    public async Task<IndexResponseModel> GetHomePage(string userId)
    {
      return new IndexResponseModel {
        Promotions = await _context.HomePromotions.ToListAsync(),
        Widgets = await GetWidgets(userId)
      };
    }


    public async Task<PathResponseModel> UploadFileAsync(Stream stream, string mime)
    {
      string filepath = await FileHelper.UploadAsync(stream, mime);
      if (filepath != null) {
        return new PathResponseModel {
          Path = filepath
        };
      }

      throw new ApiException("Произошла ошибка", 400);
    }

    public async Task<PersonalDataResponseModel> GetPersonalDataAsync(string userId)
    {
      ApplicationUser user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
      return _mapper.Map<PersonalDataResponseModel>(user);
    }

    public async Task<PersonalDataResponseModel> UpdatePersonalDataAsync(string userId, PersonalDataRequestModel model)
    {
      ApplicationUser user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

      if (model.Email != user.Email) {
        var result = await _userManager.SetEmailAsync(user, model.Email);
        if (!result.Succeeded)  throw new ApiException("Неверный email", 400);

        result = await _userManager.SetUserNameAsync(user, model.Email);
        if (!result.Succeeded) throw new ApiException("Неверный email", 400);
      }

      user.Photo = model.Photo;
      user.PhoneNumber = model.Phone;
      user.Name = model.Name;

      if (!string.IsNullOrEmpty(model.Password)) {
        var code = await _userManager.GeneratePasswordResetTokenAsync(user);

        var result = await _userManager.ResetPasswordAsync(user, code, model.Password);
        if (!result.Succeeded) throw new ApiException("Неверный пароль", 400);
      }

      await _context.SaveChangesAsync();

      return _mapper.Map<PersonalDataResponseModel>(user);
    }
  }
}
