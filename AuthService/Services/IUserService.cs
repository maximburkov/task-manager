using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Models;

namespace AuthService.Services
{
    public interface IUserService
    {
        public Task<User> GetUserAsync(string login, string password);
    }
}
