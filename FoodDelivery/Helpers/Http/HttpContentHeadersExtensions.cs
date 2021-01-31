using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace FoodDelivery.Helpers.Http
{
  public static class HttpContentHeadersExtensions
  {
    public static bool TryGetValue(this HttpContentHeaders http, string name, out string value)
    {
      if (http.TryGetValues(name, out IEnumerable<string> values)) {
        value = values.ElementAt(0);
        return true;
      }
      value = null;

      return false;
    }
  }
}
