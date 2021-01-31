using AutoMapper;
using FoodDelivery.Data;
using FoodDelivery.Data.Tables;
using FoodDelivery.Exceptions;
using FoodDelivery.Models;
using FoodDelivery.Models.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Services
{
  public class AddressService : IAddressService
  {
    private ApplicationDbContext _context;
    private IMapper _mapper;

    public AddressService(ApplicationDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task<GetAllAddressesModel> GetAllAsync(string userId)
    {
      return new GetAllAddressesModel {
        Items = await _context.Addresses
          .OrderByDescending(x => x.CreatedAt)
          .Where(address => address.UserId == userId)
          .ToListAsync()
      };
    }

    public async Task<SuccessResponseModel> AddAsync(string userId, AddressModel address)
    {
      AddressEntity entity = _mapper.Map<AddressEntity>(address);
      entity.Location = new NetTopologySuite.Geometries.Point(address.Latitude, address.Longitude) {
        SRID = 4326
      };

      await _context.Addresses.AddAsync(entity);
      await _context.SaveChangesAsync();

      return new SuccessResponseModel {
        Id = entity.Id
      };
    }

    public async Task RemoveAsync(string userId, int addressId)
    {
      AddressEntity entity = await _context.Addresses.FirstOrDefaultAsync(address => address.Id == addressId);
      if (entity == null) throw new ApiException("Неверный ID", 400);

      _context.Remove(entity);

      await _context.SaveChangesAsync();
    }

    public async Task<DeliveryTypesResponseModel> GetDeliveryTypes()
    {
      return new DeliveryTypesResponseModel {
        Items = await _context.DeliveryTypes.ToListAsync()
      };
    }
  }
}
