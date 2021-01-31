using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Data.Tables
{
  public class SessionEntity
  {
    public int Id { get; set; }

    public Guid RefreshToken { get; set; } = Guid.NewGuid();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public DateTime ExpiresIn { get; set; }

    public string UserId { get; set; }

    public virtual ApplicationUser User { get; set; }

    public SessionEntity(string userId)
    {
      UserId = userId;
   //   ExpiresIn = DateTime.UtcNow.AddDays(expiredDays);
    }
  }
}
