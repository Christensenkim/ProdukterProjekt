using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProdukterProjekt.UI.Helpers
{
    public class JWTSecurityKey
    {
        private static byte[] secretBytes = Encoding.UTF8.GetBytes("A secret from for HmacSha256");

        public static SymmetricSecurityKey Key
        {
            get { return new SymmetricSecurityKey(secretBytes); }
        }
    }
}
