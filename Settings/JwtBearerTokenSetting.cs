using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeApp.Settings
{
    public class JwtBearerTokenSetting
    {
        public string SecretKey { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ExpiryTimeInSeconds { get; set; }

        public SecurityKey SecurityKey
        {
            get
            {
                return new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(SecretKey));
            }
        }
    }
}
