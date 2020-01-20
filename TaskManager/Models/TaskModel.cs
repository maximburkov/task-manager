using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class TaskModel
    {
        public string Id { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public string ProjectId { get; set; }
    }
}
