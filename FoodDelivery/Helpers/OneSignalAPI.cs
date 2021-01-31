using FoodDelivery.Helpers.Http;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FoodDelivery.Helpers
{
  public static class OneSignalAPI
  {
    public static async Task<JObject> GetHistoryAsync(int limit, int offset, int kind)
    {
      try {
        var response = await HttpHelper.GetAsync($"https://onesignal.com/api/v1/notifications?app_id={AppConst.ONESIGNAL_APP_ID}&limit={limit}&offset={offset}&kind={kind}", new Dictionary<string, string> {
          ["Authorization"] = $"Basic {AppConst.ONESIGNAL_REST_API_KEY}"
        });
        if (response?.Body == null || response.StatusCode != HttpStatusCode.OK) {
          Console.WriteLine("STATUS CODE: " + response.StatusCode);
          Console.WriteLine("BODY: " + response?.Body);
          return null;
        }

        return JObject.Parse(response.Body);
      }
      catch (Exception ex) {
        Console.WriteLine(ex);
        return null;
      }
    }

    public static async Task<bool> DeleteAsync(string id)
    {
      try {
        var response = await HttpHelper.Delete($"https://onesignal.com/api/v1/notifications/{id}?app_id={AppConst.ONESIGNAL_APP_ID}", new Dictionary<string, string> {
          ["Authorization"] = AppConst.ONESIGNAL_REST_API_KEY
        });

        if (response?.Body == null || response.StatusCode != HttpStatusCode.OK) {
          Console.WriteLine("STATUS CODE: " + response.StatusCode);
          Console.WriteLine("BODY: " + response?.Body);
          return false;
        }

        JObject json = JObject.Parse(response.Body);
        return (bool)json["success"];
      }
      catch (Exception ex) {
        Console.WriteLine(ex);
        return false;
      }
    }

    public static async Task<(bool, string)> SendAsync(List<string> connections, OneSignalModel.ContentsOneSignalModel contents)
    {
      if (connections == null || connections.Count == 0) {
        return (true, null);
      }

      try {
        OneSignalModel model = new OneSignalModel {
          IncludePlayerIds = connections,
          AppId = AppConst.ONESIGNAL_APP_ID,
          Contents = contents
        };

        var response = await HttpHelper.PostAsync("https://onesignal.com/api/v1/notifications", model);
        Console.WriteLine("RESPONSE: " + response.Body);

        if (response?.Body == null) {
          Console.WriteLine("STATUS CODE: " + response.StatusCode);
          return (false, null);
        }
        if (response.StatusCode != HttpStatusCode.OK) {
          var obj = JObject.Parse(response.Body);
          Console.WriteLine((false, (string)(obj["errors"][0])));
          return (false, (string)(obj["errors"][0]));
        }

      }
      catch (Exception ex) {
        Console.WriteLine(ex);
        return (false, null);
      }

      return (true, null);
    }
  }
}
