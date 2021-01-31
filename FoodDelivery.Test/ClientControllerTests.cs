using FoodDelivery.Controllers;
using FoodDelivery.Data;
using FoodDelivery.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace FoodDelivery.Test
{
  public class ClientControllerTests
  {
    private const string EMAIL = "any1@any.com";
    private const string PASSWORD = "Anyany_1";

    private ApplicationDbContext _context;
    private ClientController _controller;

    public ClientControllerTests()
    {
      _context = ServiceLocator.ResolveRequired<ApplicationDbContext>();

      _context.Roles.Add(new IdentityRole {
        Name = Roles.CLIENT,
        NormalizedName = Roles.CLIENT.ToUpper()
      });
      _context.SaveChanges();

      _controller = new ClientController(ServiceLocator.ResolveRequired<IUserService>(), ServiceLocator.ResolveRequired<IAuthService>());
    }

    [Fact]
    public async Task Register_ReturnOk()
    {
      var result = await _controller.Register(new Models.Auth.RegisterModel {
        Email = EMAIL,
        Password = PASSWORD,
        ConfirmPassword = PASSWORD
      }) as OkObjectResult;

      Assert.NotNull(result);
    }

    [Fact]
    public async Task GetData_ReturnOk()
    {
      var result = await _controller.GetData() as OkObjectResult;
      Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateData_ReturnOk()
    {
      var result = await _controller.UpdateData(new Models.Data.PersonalDataRequestModel {
        Email = EMAIL,
        Password = PASSWORD,
        Name = "Test Name",
        Phone = "3983909333"
      }) as OkObjectResult;

      Assert.NotNull(result);
    }
  }
}
