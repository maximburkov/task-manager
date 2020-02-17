﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Models
{
    public class RefreshToken
    {
        public string Token { get; set; }

        public DateTime ExcpirationDate { get; set; }

        public string UserLogin {get;set;}

        public bool IsExpired => DateTime.UtcNow < ExcpirationDate;
    }
}
