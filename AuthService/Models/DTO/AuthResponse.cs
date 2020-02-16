

namespace AuthService.Models.DTO
{
    public class AuthResponse
    {
        public string Login { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
