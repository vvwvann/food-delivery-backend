using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Models.Auth
{
    public class RefreshResponseModel
    {
        public string Msg { get; set; }

        public Guid RefreshToken { get; set; }

        public string AccessToken { get; set; }

        public long ExpiresIn { get; set; }
    }
}
