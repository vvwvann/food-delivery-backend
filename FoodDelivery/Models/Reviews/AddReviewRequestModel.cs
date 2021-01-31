using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Models.Reviews
{
  public class AddReviewRequestModel
  {
    public List<int> Tags { get; set; }

    public string Text { get; set; }

    public int Rating { get; set; }
  }
}
