using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace TaskManager.Models
{
    public class Project : TableEntity, IEntity
    {
        public string Id
        {
            get => RowKey;
            set => RowKey = value;
        }

        public string Code { get; set; }

        public string Name
        {
            get => PartitionKey;
            set => PartitionKey = value;
        }
    }
}
