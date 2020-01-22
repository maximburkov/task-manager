using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace TaskManager.AzureStorage
{
    public class TableStorageContext : ITableStorageContext
    {
        private readonly CloudTableClient _cloudClient;
        private readonly ConcurrentDictionary<string, CloudTable> _tables;

        public TableStorageContext(string connectionString)
        {
            _cloudClient = CloudStorageAccount.Parse(connectionString).CreateCloudTableClient();
            _tables = new ConcurrentDictionary<string, CloudTable>();
        }

        private async Task<CloudTable> GetTableAsync(string tableName)
        {
            if (!_tables.TryGetValue(tableName, out var table))
            {
                table = _cloudClient.GetTableReference(tableName);
                await table.CreateIfNotExistsAsync();
                _tables.TryAdd(tableName, table);
            }

            return table;
        }

        public async Task<T> GetAsync<T>(string tableName, string rowKey, string partitionKey) where T : class, ITableEntity
        {
            var table = await GetTableAsync(tableName);
            var tableOperation = TableOperation.Retrieve<T>(partitionKey, rowKey);
            var queryResult = await table.ExecuteAsync(tableOperation);
            return queryResult.Result as T;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(string tableName) where T : class, ITableEntity, new()
        {
            var tasksTable = await GetTableAsync(tableName);
            var query = new TableQuery<T>();
            var result = new List<T>();

            TableContinuationToken continuationToken = null;

            do
            {
                var queryResults = await tasksTable.ExecuteQuerySegmentedAsync(query, continuationToken);
                continuationToken = queryResults.ContinuationToken;
                result.AddRange(queryResults.Results);
            } while (continuationToken != null);

            return result;
        }

        public async Task AddAsync(string tableName, ITableEntity model)
        {
            var table = await GetTableAsync(tableName);
            var operation = TableOperation.InsertOrReplace(model);
            await table.ExecuteAsync(operation);
        }
    }
}
