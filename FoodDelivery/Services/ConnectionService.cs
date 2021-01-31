using FoodDelivery.Data;
using FoodDelivery.Data.Tables;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FoodDelivery.Services
{
  public class ConnectionService : IConnectionService
  {
    private ApplicationDbContext _context;

    public ConnectionService(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task AddAsync(string userId, string id)
    {
      if (string.IsNullOrEmpty(id)) return;

      var entity = await _context.Connections.FirstOrDefaultAsync(x => x.ConnectionId == id);
      if (entity == null) {
        entity = new ConnectionEntity(userId, id); 
        await _context.Connections.AddAsync(entity);
      }
      else {
        entity.UserId = userId;
      }

      await _context.SaveChangesAsync(); 
    }
  }
}
