using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.WebEncoders.Testing;
using Microsoft.WindowsAzure.Storage.Table;
using TaskManager.Services;

namespace TaskManager.Repositories
{
    public abstract class BaseTableStorageRepository
    {
        protected readonly CloudTableClient TableClient;

        protected BaseTableStorageRepository(ITableStorageContext context)
        {
            TableClient = context.StorageAccount.CreateCloudTableClient();
        }

        protected async Task<CloudTable> GetTableAsync(string tableName)
        {
            var table = TableClient.GetTableReference(tableName);
            await table.CreateIfNotExistsAsync();
            return table;
        }
    }
}
