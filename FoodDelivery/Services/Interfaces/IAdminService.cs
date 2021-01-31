using FoodDelivery.Models;
using FoodDelivery.Models.Admin.Users;
using System.Threading.Tasks;

namespace FoodDelivery.Services.Admin
{
  public interface IAdminService
  {
    Task<TotalResponseModel<AdminListItemUser>> GetUsers(AdminFilterModel model);
    Task AddAsync(AdminAddUser model);
    Task UpdateAsync(string id, AdminAddUser model);
    Task RemoveAsync(string id);
    Task BlockAsync(string id);
    Task UnBlockAsync(string id);
  }
}
