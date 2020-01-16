using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Services
{
    public interface IRepository<T>
    {
        public IEnumerable<T> GetAll();

        public T Get(int id);

        public void Create(T newItem);

        public void Delete(int id);
    }
}
