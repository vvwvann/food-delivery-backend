using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Models
{
  public class OneSignalModel
  {
    [JsonProperty("app_id")]
    public string AppId { get; set; }

    [JsonProperty("contents")]
    public ContentsOneSignalModel Contents { get; set; }

    [JsonProperty("include_player_ids")]
    public List<string> IncludePlayerIds { get; set; }

    public class ContentsOneSignalModel
    {
      public string En { get; set; } = "";

      public string Ru { get; set; }
    }
  }
}
