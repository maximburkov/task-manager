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
        /// JWT token lifetime in minutes
        /// </summary>
        public static int Lifetime { get; set; } = 1;

        /// <summary>
        /// JWT Secret
        /// </summary>
        public static string Secret { get; set; }

        /// <summary>
        /// Creates new symmetric security key based on secret
        /// </summary>
        /// <returns></returns>
        public static SymmetricSecurityKey CreateSymmetricSecurityKey(string secret) => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
    }
}
