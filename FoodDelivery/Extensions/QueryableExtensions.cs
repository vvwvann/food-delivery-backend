using FoodDelivery.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Extensions
{
  public static class QueryableExtensions
  {
    public static async Task<List<T>> PaginateAsync<T>(
        this IQueryable<T> source,
        IPaginationInfo pagination)
    {
      return await source
          .Skip((pagination.PageNumber - 1) * pagination.PageSize)
          .Take(pagination.PageSize)
          .ToListAsync();
    }
  }
}
