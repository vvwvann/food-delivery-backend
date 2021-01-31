using FoodDelivery.Data.Tables;
using FoodDelivery.Models.Reviews;
using System;
using System.Collections.Generic;
using static FoodDelivery.Models.Restaurants.GetAllRestaurantsResponseModel.RestaurantItem;

namespace FoodDelivery.Models.Restaurants
{
  public class GetAllRestaurantsResponseModel
  {
    public int Total { get; set; }

    public List<RestaurantListItem> Restaurants { get; set; }

    public class RestaurantListItem
    {
      public int Id { get; set; }

      public string Name { get; set; }

      public string[] Photo { get; set; }

      public RatingItem Rating { get; set; }

      public bool IsFavorite { get; set; }

      public string Delivery { get; set; }

      public List<int> Cuisines { get; set; }
    }

    public class RestaurantItem : RestaurantListItem
    {
      public string Description { get; set; }

      public AddressItem Address { get; set; }

      public ScheduleItem Schedule { get; set; }

      public GetAllReviewsResponseModel Reviews { get; set; }

      public List<MenuCategoryEntity> Menu { get; set; }

      public bool FreeDelivery { get; set; }

      public class TimeOfDelivery
      {
        public int From { get; set; }

        public int To { get; set; }
      }

      public class RatingItem
      {
        public double Value { get; set; }

        public int Total { get; set; }
      }

      public class AddressItem
      {
        public string City { get; set; }

        public string Street { get; set; }

        public string HouseNumber { get; set; }

        public string ApartmentNumber { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
      }

      public class ScheduleItem
      {
        public DateTime From { get; set; }

        public DateTime To { get; set; }
      }
    }
  }

}
