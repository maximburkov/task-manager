using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using TaskManager.Exceptions;

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
                try
                {
                    await table.CreateIfNotExistsAsync();

                }
                catch (Exception ex)
                {

                    throw;
                }
                _tables.TryAdd(tableName, table);
            }

            return table;
        }

        /// <summary>
        /// Retrieve element by row key and partition key. Fastest way to get element from table.
        /// </summary>
        /// <typeparam name="T">Element type</typeparam>
        /// <param name="tableName">Table name</param>
        /// <param name="rowKey">Row key</param>
        /// <param name="partitionKey">Partition key</param>
        public async Task<T> GetAsync<T>(string tableName, string rowKey, string partitionKey) where T : class, ITableEntity
        {
            var table = await GetTableAsync(tableName);
            var tableOperation = TableOperation.Retrieve<T>(partitionKey, rowKey);
            var queryResult = await table.ExecuteAsync(tableOperation);
            return queryResult.Result as T;
        }

        /// <summary>
        /// Get all elements from table
        /// </summary>
        /// <typeparam name="T">Element type</typeparam>
        /// <param name="tableName">Table name</param>
        public async Task<IEnumerable<T>> GetAllAsync<T>(string tableName) where T : class, ITableEntity, new()
        {
            return await QueryAsync<T>(tableName, new TableQuery<T>());
        }

        /// <summary>
        /// Query elements by parameters
        /// </summary>
        /// <typeparam name="T">Element type</typeparam>
        /// <param name="tableName">Table name</param>
        /// <param name="tableQuery">Table query</param>
        public async Task<IEnumerable<T>> QueryWithParametersAsync<T> (string tableName, TableQuery<T> tableQuery) where T : class, ITableEntity, new()
        {
            return await QueryAsync<T>(tableName, tableQuery);
        }

        /// <summary>
        /// Add element in table
        /// </summary>
        /// <param name="tableName">Table name</param>
        /// <param name="model">Element we want to add</param>
        /// <returns></returns>
        public async Task<ITableEntity> AddAsync(string tableName, ITableEntity model)
        {
            var table = await GetTableAsync(tableName);
            var operation = TableOperation.Insert(model);
            await table.ExecuteAsync(operation);
            return model;
        }

        /// <summary>
        /// Update element in table
        /// </summary>
        /// <param name="tableName">Table name</param>
        /// <param name="model">Element we want to update</param>
        public async Task<ITableEntity> UpdateAsync(string tableName, ITableEntity model)
        {
            var table = await GetTableAsync(tableName);
            model.ETag ??= "*";
            var operation = TableOperation.Merge(model);
            try
            {
                await table.ExecuteAsync(operation);
            }
            catch (StorageException e)
            {
                if(e.Message == "Not Found")
                    throw new NotFoundException($"Not Found in {tableName} with RowKey {model.RowKey} and PartitionKey {model.PartitionKey}");
                throw;
            }
            return model;
        }

        /// <summary>
        /// Delete element if exists
        /// </summary>
        /// <typeparam name="T">Element type</typeparam>
        /// <param name="tableName">Table name</param>
        /// <param name="rowKey">Row key</param>
        /// <param name="partitionKey">Partition key</param>
        public async Task DeleteAsync<T>(string tableName, string rowKey, string partitionKey) where T : class, ITableEntity
        {
            var table = await GetTableAsync(tableName);
            var entityToDelete = await GetAsync<T>(tableName, rowKey, partitionKey);

            if (entityToDelete == null)
                throw new NotFoundException($"Not Found in {tableName} with RowKey {rowKey} and PartitionKey {partitionKey}");

            var deleteOperation = TableOperation.Delete(entityToDelete);
            await table.ExecuteAsync(deleteOperation);
        }

        private async Task<IEnumerable<T>> QueryAsync<T>(string tableName, TableQuery<T> query) where T : class, ITableEntity, new()
        {
            var tasksTable = await GetTableAsync(tableName);
            var result = new List<T>();
            TableContinuationToken continuationToken = null;

            do
            {
                var queryResults = await tasksTable.ExecuteQuerySegmentedAsync(query, continuationToken);
                continuationToken = queryResults.ContinuationToken;
                result.AddRange(queryResults.Results);
            } while (continuationToken != null && (!query.TakeCount.HasValue || result.Count < query.TakeCount.Value));

            return result;
        }
    }
}
