using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Repositories
{
    public interface ITaskRepository
    {
        public Task<IEnumerable<TaskModel>> GetAllAsync();

        public Task<TaskModel> GetAsync(string projectId, string id);

        public Task CreateAsync(TaskModel newItem);

        public Task DeleteAsync(string id);
    }
}
