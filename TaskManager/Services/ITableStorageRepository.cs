using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Services
{
    public interface ITableStorageRepository<T>
    {
        public Task<IEnumerable<T>> GetAll();

        public T Get(string id);

        public Task Create(T newItem);

        public Task Delete(string id);
    }
}
