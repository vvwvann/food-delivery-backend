using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Data.Tables
{
  public class ConnectionEntity
  {
    [Key]
    public string ConnectionId { get; set; }

    public string UserId { get; set; }

    public ApplicationUser User { get; set; }

    public ConnectionEntity(string userId, string connectionId)
    {
      ConnectionId = connectionId;
      UserId = userId;
    }
  }
}
