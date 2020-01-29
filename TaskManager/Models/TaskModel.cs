using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class TaskModel
    {
        [StringLength(100)]
        public string Id { get; set; }

        [StringLength(200)]
        public string Subject { get; set; }

        [StringLength(10000)]
        public string Description { get; set; }

        [StringLength(100)]
        public string ProjectId { get; set; }
    }
}
