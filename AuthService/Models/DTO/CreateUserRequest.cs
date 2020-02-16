using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Models.DTO
{
    public class CreateUserRequest
    {
        [StringLength(100)]
        public string Login { get; set; }

        [MinLength(6)]
        [StringLength(100)]
        public string Password { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Name { get; set; }
    }
}
