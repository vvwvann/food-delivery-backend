using System;

namespace FoodDelivery.Models.Admin.Users
{
  public class AdminListItemUser : AdminUser
  {
    public string Id { get; set; }

    public bool IsBlock { get; set; }

    public DateTime CreatedAt { get; set; }
  }
}
