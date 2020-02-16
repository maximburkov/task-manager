using System.ComponentModel.DataAnnotations;

namespace AuthService.Models.DTO
{
    public class AuthRequest
    {
        [StringLength(100)]
        public string Login { get; set; }

        [MinLength(6)]
        [StringLength(100)]
        public string Password { get; set; }

        [StringLength(100)]
        public string Name { get; set; }
    }
}
