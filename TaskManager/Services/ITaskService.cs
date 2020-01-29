using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;
using TaskManager.QueryParameters;

namespace TaskManager.Services
{
    public interface ITaskService
    {
        Task<TaskModel> GetAsync(string id, string projectId);

        Task<TaskModel> GetAllByProjectIdAsync(string projectId);

        Task<IEnumerable<TaskModel>> GetWithParameters(TasksParameters parameters);

        Task<IEnumerable<TaskModel>> GetAllAsync();

        Task<TaskModel> AddAsync(TaskModel task);

        Task<TaskModel> UpdateAsync(string id, string projectId, TaskModel task);

        Task DeleteAsync(string id, string projectId);
    }
}
