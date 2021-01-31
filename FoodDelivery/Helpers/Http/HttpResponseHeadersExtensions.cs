using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FoodDelivery.Helpers.Http
{
  public static class HttpResponseHeadersExtensions
  {
    public static bool TryGetValue(this HttpResponseHeaders http, string name, out string value)
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
