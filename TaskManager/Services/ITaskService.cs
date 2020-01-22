using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Services
{
    public interface ITaskService
    {
        Task<TaskModel> GetAsync(string id, string projectId);

        Task<IEnumerable<TaskModel>> GetAllAsync();

        Task<TaskModel> AddAsync(TaskModel task);
    }
}
