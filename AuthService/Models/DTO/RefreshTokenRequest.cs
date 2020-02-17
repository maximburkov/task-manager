using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Models.DTO
{
    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; }

        public string Login { get; set; }
    }
}
