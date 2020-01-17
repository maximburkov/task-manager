using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;

namespace TaskManager.Services
{
    public class TaskManagerContext : ITableStorageContext
    {
        public TaskManagerContext(string connectionString)
        {
            StorageAccount = CloudStorageAccount.Parse(connectionString);
        }

        public CloudStorageAccount StorageAccount { get; set; }
    }
}
