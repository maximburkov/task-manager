using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models.DTO
{
    public class TaskDto
    {
        [StringLength(200)]
        public string Subject { get; set; }


        [StringLength(10000)]
        public string Description { get; set; }
    }
}
