using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace TaskManager.AzureStorage
{
    public interface ITableStorageContext
    {
        Task<T> GetAsync<T>(string tableName, string rowKey, string partitionKey) where T : class, ITableEntity;

        Task<IEnumerable<T>> GetAllAsync<T>(string tableName) where T : class, ITableEntity, new();

        Task<IEnumerable<T>> QueryWithParametersAsync<T>(string tableName, TableQuery<T> query) where T : class, ITableEntity, new();


        Task<ITableEntity> AddAsync(string tableName, ITableEntity model);
    }
}
