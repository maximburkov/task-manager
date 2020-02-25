using System.ComponentModel.DataAnnotations;
using TaskManager.Infrastructure;

namespace TaskManager.Models
{
    public class TaskModel
    {
        [StringLength(100)]
        [RegularExpression(Constants.KeyRegex, ErrorMessage = Constants.NotAllowedRegexMessage)]
        public string Id { get; set; }

        [StringLength(200)]
        public string Subject { get; set; }

        [StringLength(10000)]
        public string Description { get; set; }

        [StringLength(100)]
        [RegularExpression(Constants.KeyRegex, ErrorMessage = Constants.NotAllowedRegexMessage)]
        public string ProjectId { get; set; }
    }
}
