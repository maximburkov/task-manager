using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace AuthService.Models.TableStorage
{
    public class UserEntity : TableEntity
    {
        public string Password { get; set; }

        public string Salt { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }
    }
}
