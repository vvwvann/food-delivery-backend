using AutoMapper;
using FoodDelivery.Data.Tables;
using FoodDelivery.Models.Admin.Orders;
using FoodDelivery.Models.Admin.Restaurant;
using FoodDelivery.Models.Admin.Users;
using FoodDelivery.Models.Auth;
using FoodDelivery.Models.Data;
using FoodDelivery.Models.Favorites;
using FoodDelivery.Models.Order;
using FoodDelivery.Services.Admin;
using System.Collections.Generic;
using static FoodDelivery.Models.Reviews.GetAllReviewsResponseModel;

namespace FoodDelivery
{
  public class DefaultAutoMapperProfile : Profile
  {
    public DefaultAutoMapperProfile()
    {
      CreateMap<PersonalDataResponseModel, ApplicationUser>();

      CreateMap<ReviewItem[], ReviewEntity[]>();
      CreateMap<AddressEntity, AddressModel>();
      CreateMap<ApplicationUser, RegisterModel>().ReverseMap()
       .AfterMap((entity, model) => {
         model.UserName = entity.Email;
       });

      CreateMap<BasketResponseItem, BasketDishEntity>()
       .ForMember(dest => dest.Dish, opt => opt.Ignore());


      CreateMap<BasketResponseItem[], BasketDishEntity[]>();
      CreateMap<DishEntity, DishListItem>()
        .ForMember(dest => dest.Photo, opt => opt.MapFrom(x => x.Photo[0]));
      CreateMap<DishListItem[], DishEntity[]>();

      CreateMap<DishEntity, AdminAddDishModel>()
        .ForMember(dest => dest.Photo, opt => opt.MapFrom(x => x.Photo[0]));
      CreateMap<DishEntity, AdminUpdateDishModel>()
        .ForMember(dest => dest.Photo, opt => opt.MapFrom(x => x.Photo[0]));
      CreateMap<AdminGetRestaurantModel, RestaurantEntity>();
      CreateMap<AdminManagerModel[], ApplicationUser[]>();
      CreateMap<RestaurantEntity, AdminRestaurantModel>();
      CreateMap<List<AdminRestaurantItem>, RestaurantEntity[]>();
      CreateMap<List<AdminListItemUser>, ApplicationUser[]>();
      CreateMap<ApplicationUser, AdminAddUser>();

      CreateMap<OrderDishEntity, AdminOrderDishModel>().AfterMap((entity, model) => {
        model.Name = entity.Dish.Name;
      });

      CreateMap<OrderEntity, AdminOrderReponseModel>()
         .AfterMap((entity, model) => {
           model.Manager = new AdminManagerModel {
             Id = entity.ManagerId,
             Name = entity.Manager?.Name
           };
         });

      CreateMap<OrderEntity, AdminOrderModel>()
         .AfterMap((entity, model) => {
           model.Manager = new AdminManagerModel {
             Id = entity.ManagerId,
             Name = entity.Manager?.Name
           };
         });

    }
  }
}
