using Microsoft.AspNetCore.Authorization;

namespace FoodDelivery.Attributes
{
  public class AuthorizeRolesAttribute : AuthorizeAttribute
  {
    public AuthorizeRolesAttribute(params string[] roles) : base()
    {
      Roles = string.Join(",", roles);
    }
  }
}
