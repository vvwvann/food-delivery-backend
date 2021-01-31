using AutoMapper;
using FoodDelivery.Data;
using FoodDelivery.Data.Tables;
using FoodDelivery.Exceptions;
using FoodDelivery.Extensions;
using FoodDelivery.Models;
using FoodDelivery.Models.Admin.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FoodDelivery.Services.Admin
{
  public class AdminService : IAdminService
  {
    private IMapper _mapper;
    private ApplicationDbContext _context;
    private UserManager<ApplicationUser> _userManager;

    public AdminService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
      _userManager = userManager;
    }

    public async Task<TotalResponseModel<AdminListItemUser>> GetUsers(AdminFilterModel model)
    {
      string text = model.Search?.Trim();

      if (!string.IsNullOrEmpty(text))
        text = $"%{text}%";

      var query = _context.Users.AsNoTracking().Where(x => string.IsNullOrEmpty(model.Search) || EF.Functions.ILike(x.Name, text));

      Expression<Func<ApplicationUser, object>> orderExp = x => x.Id;
      orderExp = model.Sort switch {
        "name" => x => x.Name,
        "role" => x => _userManager.GetRolesAsync(x).Result.First(),
        "date" => x => x.CreatedAt,
        _ => x => x.Id
      };

      int total = await query.CountAsync();

      query = model.Sort == "date" ? query.OrderByDescending(orderExp)
       : query.OrderBy(orderExp);


      var users = await query.PaginateAsync(model);

      var result = new List<AdminListItemUser>();
      foreach (var item in users) {
        result.Add(new AdminListItemUser {
          Id = item.Id,
          CreatedAt = item.CreatedAt,
          Email = item.Email,
          IsBlock = item.IsBlock,
          Name = item.Name,
          Phone = item.PhoneNumber,
          Role = (await _userManager.GetRolesAsync(item)).First()
        });
      }

      return new TotalResponseModel<AdminListItemUser> {
        Total = total,
        Items = result
      };
    }


    public async Task AddAsync(AdminAddUser model)
    {
      if (model.Password != model.ConfirmPassword)
        throw new ApiException("Пароли не совпадают", 400);

      if (model.Role != Roles.ADMIN && model.Role != Roles.CLIENT && model.Role != Roles.MANAGER)
        throw new ApiException("Роль не существует", 400);

      ApplicationUser user = _mapper.Map<ApplicationUser>(model);

      var result = await _userManager.CreateAsync(user, model.Password);
      if (result.Succeeded) {
        result = await _userManager.AddToRoleAsync(user, model.Role);
        if (result.Succeeded) {
          result = await _userManager.AddPasswordAsync(user, model.Password);
          if (!result.Succeeded) throw new ApiException(result.Errors.First().Description, 400);
        }
      }
    }

    public async Task UpdateAsync(string id, AdminAddUser model)
    {
      var user = await GetUser(id);

      if (model.Role != Roles.ADMIN && model.Role != Roles.CLIENT && model.Role != Roles.MANAGER)
        throw new ApiException("Роль не существует", 400);

      user = _mapper.Map(model, user);

      IdentityResult result;
      if (model.Email != user.Email) {
        result = await _userManager.SetEmailAsync(user, model.Email);
        if (!result.Succeeded) throw new ApiException(result.Errors.First().Description, 400);
        result = await _userManager.SetUserNameAsync(user, model.Email);
        if (!result.Succeeded) throw new ApiException(result.Errors.First().Description, 400);
      }

      var roles = await _userManager.GetRolesAsync(user);
      await _userManager.RemoveFromRolesAsync(user, roles);

      result = await _userManager.AddToRoleAsync(user, model.Role);
      if (!result.Succeeded) throw new ApiException(result.Errors.First().Description, 400);
    }


    public async Task RemoveAsync(string id)
    {
      var user = await GetUser(id);

      _context.Users.Remove(user);
      await _context.SaveChangesAsync();
    }

    public async Task BlockAsync(string id)
    {
      var user = await GetUser(id);

      user.IsBlock = true;
      await _context.SaveChangesAsync();
    }

    public async Task UnBlockAsync(string id)
    {
      var user = await GetUser(id);

      user.IsBlock = false;
      await _context.SaveChangesAsync();
    }

    private async Task<ApplicationUser> GetUser(string id)
    {
      var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
      if (user == null) throw new ApiException("Пользователь не найден", 400);

      return user;
    }
  }
}
