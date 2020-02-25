using System.ComponentModel.DataAnnotations;
using TaskManager.Infrastructure;

namespace TaskManager.Models.DTO
{
    public class CreateProjectDto
    {
        [StringLength(100)]
        [Required]
        [RegularExpression(Constants.KeyRegex, ErrorMessage = Constants.NotAllowedRegexMessage)]
        public string Code { get; set; }

        [MaxLength(1000)]
        [Required]
        public string Name { get; set; }
    }
}
