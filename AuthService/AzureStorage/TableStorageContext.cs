using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using TaskManager.AzureStorage;

namespace AuthService.AzureStorage
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

        /// <summary>
        /// Get CloudTable object by table name
        /// </summary>
        /// <param name="tableName">Table name</param>
        /// <returns></returns>
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

        public async Task<ITableEntity> AddAsync(string tableName, ITableEntity model)
        {
            var table = await GetTableAsync(tableName);
            var operation = TableOperation.Insert(model);
            await table.ExecuteAsync(operation);
            return model;
        }
    }
}



