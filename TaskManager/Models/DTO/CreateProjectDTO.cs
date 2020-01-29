using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models.DTO
{
    public class CreateProjectDto
    {
        [StringLength(100)]
        [Required]
        public string Code { get; set; }

        [MaxLength(1000)]
        [Required]
        public string Name { get; set; }
    }
}
