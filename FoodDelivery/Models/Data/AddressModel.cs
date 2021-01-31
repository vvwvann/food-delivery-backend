using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Models.Data
{
  public class AddressModel
  {
    public string City { get; set; }

    public string Street { get; set; }

    public string HouseNumber { get; set; }

    public string ApartmentNumber { get; set; }

    public int DeliveryTypeId { get; set; }

    public string Description { get; set; } 

    public double Latitude { get; set; }

    public double Longitude { get; set; }
  }
}
