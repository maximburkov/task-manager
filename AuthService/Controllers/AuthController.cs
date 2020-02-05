using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AuthService.Models;
using AuthService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IUserService _userService;
        private readonly AppSettings _settings;

        public AuthController(ILogger<AuthController> logger, IUserService userService, IOptions<AppSettings> settings)
        {
            _logger = logger;
            _userService = userService;
            _settings = settings.Value;
        }

        [HttpPost]
        public async Task<ActionResult> GetToken(AuthRequest request)
        {
            var user = await _userService.GetUserAsync(request.Login, request.Password);

            if (user == null)
            {
                return BadRequest();
            }

            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddMinutes(_settings.Auth.Lifetime),
                signingCredentials: new SigningCredentials(
                    AuthManager.CreateSymmetricSecurityKey(_settings.Auth.Secret), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new AuthResponse
            {
                AccessToken = encodedJwt,
                //RefreshToken = 
                Login = user.Login,
            });
        }

        [HttpGet]
        [Authorize]
        public async Task<string> Test()
        {
            return "secret string";
        }

    }
}
