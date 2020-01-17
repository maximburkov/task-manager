using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using TaskManager.Models;

namespace TaskManager.Services
{
    public class TableStorageRepository<TEntity> : ITableStorageRepository<TEntity> where TEntity : TableEntity, IEntity, new()
    {
        private CloudStorageAccount _storage;

        public TableStorageRepository(ITableStorageContext context)
        {
            _storage = context.StorageAccount;
        }

        public Task<IEnumerable<TEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public TEntity Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task Create(TEntity newItem)
        {
            throw new NotImplementedException();
        }

        public async Task Insert(TEntity entity)
        {
            throw new NotImplementedException();

        }

        public async Task Delete(string id)
        {
            throw new NotImplementedException();

        }
    }
}
