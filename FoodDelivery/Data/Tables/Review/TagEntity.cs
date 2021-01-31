using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Data.Tables
{
  public class TagEntity
  {
    public int Id { get; set; }

    public string Description { get; set; }

    [JsonIgnore]
    public List<ReviewTagEntity> Reviews { get; set; }
  }
}
