using System;
using System.Collections.Generic;

namespace FoodDelivery.Models.Reviews
{
  public class GetAllReviewsResponseModel
  {
    public int Total { get; set; }

    public List<ReviewItem> Items { get; set; }

    public class ReviewItem
    {
      public int Id { get; set; }

      public string Text { get; set; }

      public double Rating { get; set; }

      public DateTimeOffset CreatedAt { get; set; }

      public UserReviewItem User { get; set; }

      public List<int> Tags { get; set; }

      public class UserReviewItem
      {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Photo { get; set; }
      }
    }
  }
}
