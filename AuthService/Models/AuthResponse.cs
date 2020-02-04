﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Models
{
    public class AuthResponse
    {
        public string Login { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
