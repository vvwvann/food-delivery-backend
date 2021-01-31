using FoodDelivery.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace FoodDelivery.Helpers
{
  public static class JWTHelper
  {
    public static string Create(string userId, string role, out long expired)
    {
      var tokenHandler = new JwtSecurityTokenHandler();

      var key = Encoding.ASCII.GetBytes("sjlkwher25672afefw");
      var tokenDescriptor = new SecurityTokenDescriptor {
        Subject = new ClaimsIdentity(new Claim[]
         {
              new Claim(ClaimTypes.Name, userId),
              new Claim(ClaimTypes.Role, role)
         }),

#if DEBUG
        Expires = DateTime.UtcNow.AddDays(100),
#else
        Expires = DateTime.UtcNow.AddDays(7),
#endif
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);
      expired = (long)(tokenDescriptor.Expires.Value.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

      string result = tokenHandler.WriteToken(token);
      if (string.IsNullOrEmpty(result))
        throw new ApiException("Не удалось создать токен", 400);

      return result;

    }
  }
}
