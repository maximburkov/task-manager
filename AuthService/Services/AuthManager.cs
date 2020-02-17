using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Services
{
    public static class AuthManager
    {
        /// <summary>
        /// Creates new symmetric security key based on secret
        /// </summary>
        /// <returns></returns>
        public static SymmetricSecurityKey CreateSymmetricSecurityKey(string secret) => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
    }
}
