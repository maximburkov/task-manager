using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthService.Infrastructure.Exceptions;
using AuthService.Models;
using AuthService.Models.TableStorage;
using AutoMapper;
using Microsoft.WindowsAzure.Storage;
using TaskManager.AzureStorage;

namespace AuthService.Services
{
    public class UserStorageService : IUserService
    {
        private const string TableName = "Users";
        private ITableStorageContext _context;
        private const string Partition = "TaskManager";
        private readonly IMapper _mapper;

        public UserStorageService(ITableStorageContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<User> GetUserAsync(string login)
        {
            var userEntity = await _context.GetAsync<UserEntity>(TableName, login, Partition);
            return _mapper.Map<User>(userEntity);
        }

        public async Task CreateUserAsync(User user)
        {
            var userEntity = _mapper.Map<UserEntity>(user);
            userEntity.PartitionKey = Partition;
            try
            {
                var result = await _context.AddAsync(TableName, userEntity);
            }
            catch (StorageException e)
            {
                if (e.Message.Contains("Conflict"))
                {
                    throw new UserAlreadyExistsException($"User with login {user.Login} already exists!");
                }
                throw;
            }
        } 
    }
}
