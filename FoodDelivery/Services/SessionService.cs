using FoodDelivery.Data;
using FoodDelivery.Data.Tables;
using FoodDelivery.Exceptions;
using FoodDelivery.Helpers;
using FoodDelivery.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Services
{
  public class SessionService
  {
    public const double EXPIRED_DAYS = 90;

    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public SessionService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
      _context = context;
      _userManager = userManager;
    }

    public async Task<Guid> CreateAsync(string userId)
    {
      SessionEntity session = new SessionEntity(userId);

      await _context.AddAsync(session);
      await _context.SaveChangesAsync();

      return session.RefreshToken;
    }

    public async Task<RefreshResponseModel> UpdateAsync(Guid refresh)
    {
      var session = await _context.Sessions
        .Include(x => x.User)
        .FirstOrDefaultAsync(x => x.RefreshToken == refresh);

      if (session == null) throw new ApiException("Невереный токен", 400);

      if (session.ExpiresIn < DateTime.UtcNow) throw new ApiException("Токен истек", 400);

      session.RefreshToken = Guid.NewGuid();
      session.ExpiresIn = DateTime.UtcNow.AddDays(EXPIRED_DAYS);
      session.UpdatedAt = DateTime.UtcNow;

      await _context.SaveChangesAsync();

      string role = (await _userManager.GetRolesAsync(session.User)).First();
      var token = JWTHelper.Create(session.UserId, role, out long expiresIn);

      return new RefreshResponseModel {
        RefreshToken = session.RefreshToken,
        AccessToken = token,
        ExpiresIn = expiresIn
      };
    }

    public async Task DeleteAsync()
    {
      var sessions = await _context.Sessions
        .Take(50)
        .Where(x => x.ExpiresIn < DateTime.UtcNow)
        .ToListAsync();

      _context.Sessions.RemoveRange(sessions);
      await _context.SaveChangesAsync();
    }
  }

}
