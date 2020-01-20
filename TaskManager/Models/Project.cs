using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace TaskManager.Models
{
    public class Project : TableEntity
    {
        public string Code { get; set; }
    }
}
