﻿using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class Project
    {
        [StringLength(100)]
        public string Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Code { get; set; }

        [MaxLength(1000)]
        [Required]
        public string Name { get; set; }
    }
}
