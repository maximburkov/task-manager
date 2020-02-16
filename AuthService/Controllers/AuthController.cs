using AuthService.Models;
using AuthService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using AuthService.Models.DTO;

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
            var user = await _userService.GetUserAsync(request.Login);

            if (user == null)
            {
                return BadRequest();
            }

            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddMinutes(_settings.Auth.Lifetime),
                signingCredentials: new SigningCredentials(
                    AuthManager.CreateSymmetricSecurityKey(_settings.SecretKey), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new AuthResponse
            {
                AccessToken = encodedJwt,
                //RefreshToken = 
                Login = user.Login,
            });
        }
    }
}
