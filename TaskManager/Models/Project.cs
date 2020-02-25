using System.ComponentModel.DataAnnotations;
using TaskManager.Infrastructure;

namespace TaskManager.Models
{
    public class Project
    {
        [StringLength(100)]
        [RegularExpression(Constants.KeyRegex, ErrorMessage = Constants.NotAllowedRegexMessage)]
        public string Id { get; set; }

        [StringLength(100)]
        [Required]
        [RegularExpression(Constants.KeyRegex, ErrorMessage = Constants.NotAllowedRegexMessage)]
        public string Code { get; set; }

        [MaxLength(1000)]
        [Required]
        public string Name { get; set; }
    }
}
