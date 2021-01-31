using FoodDelivery.Data.Tables.User;
using System.Collections.Generic;

namespace FoodDelivery.Models.Admin
{
  public class AdminGetStatisticsModel
  {
    public int Total { get; set; }
    public List<StatisticEntity> Items { get; set; }
  }
}
