using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Infrastructure.Cryptography;
using AuthService.Infrastructure.Exceptions;
using AuthService.Models;
using AuthService.Models.DTO;
using AuthService.Models.TableStorage;
using AuthService.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.AzureStorage;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUserService _service;
        private const string TableName = "Users";
        private const string Partition = "TaskManager";
        private readonly IMapper _mapper;
        public AccountsController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateUserRequest userRequest)
        {
            var user = _mapper.Map<User>(userRequest);
            user.Salt = Hashing.CreateSalt();
            user.Password = Hashing.CreateHash(user.Password, user.Salt);

            try
            {
                await _service.CreateUserAsync(user);
            }
            catch (UserAlreadyExistsException e)
            {
                return Conflict(new
                {
                    Error = e.Message
                });
            }
            
            return Ok();
        }
    }
}