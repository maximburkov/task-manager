using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models.DTO
{
    public class UpdateProjectDto
    {
        [MaxLength(1000)]
        [Required]
        public string Name { get; set; }
    }
}
