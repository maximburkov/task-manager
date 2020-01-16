using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;

namespace TaskManager.Models
{
    public class TaskEntity : TableEntity
    {
        public string Subject { get; set; }

        public string Description { get; set; }
    }
}
