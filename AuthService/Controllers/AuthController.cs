using AuthService.Models;
using AuthService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AuthService.Models.DTO;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly AppSettings _settings;

        public AuthController(ILogger<AuthController> logger, IUserService userService, IOptions<AppSettings> settings, ITokenService tokenService)
        {
            _logger = logger;
            _userService = userService;
            _tokenService = tokenService;
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

            var currentTime = DateTime.UtcNow;

            var token = new JwtSecurityToken(
                expires: currentTime.AddMinutes(_settings.Auth.AccessTokenLifetime),
                signingCredentials: new SigningCredentials(
                    AuthManager.CreateSymmetricSecurityKey(_settings.SecretKey), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = await _tokenService.CreateTokenAsync(
                new RefreshToken
                {
                    Token = GenerateRefreshToken(),
                    ExcpirationDate = currentTime.AddMinutes(_settings.Auth.RefreshTokenLifetime),
                    UserLogin = user.Login
                });

            return Ok(new AuthResponse
            {
                AccessToken = encodedJwt,
                RefreshToken = refreshToken.Token,
                Login = user.Login,
            });
        }

        [HttpPost]
        [Route("Refresh")]
        public async Task<ActionResult> RefreshToken(RefreshTokenRequest request)
        {
            var refreshToken = await _tokenService.GetTokenAsync(request.RefreshToken, request.Login);
            if(refreshToken != null)
            {
                if(refreshToken.ExcpirationDate > DateTime.UtcNow)
                {
                    var currentTime = DateTime.UtcNow;

                    var accessToken = new JwtSecurityToken(
                        expires: currentTime.AddMinutes(_settings.Auth.AccessTokenLifetime),
                        signingCredentials: new SigningCredentials(
                            AuthManager.CreateSymmetricSecurityKey(_settings.SecretKey), SecurityAlgorithms.HmacSha256));

                    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(accessToken);

                    return Ok(new AuthResponse
                    {
                        AccessToken = encodedJwt,
                        RefreshToken = refreshToken.Token,
                        Login = request.Login,
                    });
                }
            }

            return Unauthorized();
        }

        private string GenerateRefreshToken()
        {
            byte[] randomBytes = new byte[16];
            using var generator = RandomNumberGenerator.Create();
            generator.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        } 

        private string GenerateAccessToken(DateTime expirationDate)
        {
            //TODO: 
            throw new NotImplementedException();
        }
    }
}
