using FoodDelivery.Services.Interfaces;
using System;

namespace FoodDelivery.Models
{
  public class PaginationInfo : IPaginationInfo
  {
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 20;
  }

  public class DateFilterModel : PaginationInfo
  {
    public DateTime From { get; set; } = DateTime.MinValue;

    public DateTime To { get; set; } = DateTime.MaxValue;
  }

  public class AdminFilterModel : PaginationInfo
  {
    public string Sort { get; set; }

    public string Search { get; set; }
  }
}
