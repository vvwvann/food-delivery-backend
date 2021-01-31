using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Helpers
{
  public static class FileHelper
  {
    public static bool TryExistOrCreate(string path)
    {
      try {
        var info = new DirectoryInfo(path);
        if (!info.Exists) info.Create();
      }
      catch {
        return false;
      };
      return true;
    }

    public static async Task<string> UploadAsync(Stream file, string mime)
    {
      string ext = "";
      try {
        ext = MimeTypeMap.GetExtension(mime);
      }
      catch {
        return null;
      }
      string name = Guid.NewGuid().ToString();
      string prefix = $"{name[0]}/{name[1]}/{name[2]}";
      string path = Startup.StoragePath + "/" + prefix;

      if (!(TryExistOrCreate(path))) return null;

      path += ('/' + name + ext);

      try {
        using var fileStream = new FileStream(path, FileMode.Create);
        await file.CopyToAsync(fileStream);
      }
      catch { return null; }

      return "/storage/" + prefix + '/' + name + ext;
    }

  }
}
