using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthService.Models;

namespace AuthService.Services
{
    public class UserService : IUserService
    {
        //temporary
        private List<User> _users = new List<User>
        {
            new User {Login="123", Password="123" },
            new User { Login="qwerty@gmail.com", Password="55555"}
        };


        public Task<User> GetUserAsync(string login, string password)
        {
            return Task.FromResult(_users.FirstOrDefault(u => u.Login == login && u.Password == password));
        }
    }
}
