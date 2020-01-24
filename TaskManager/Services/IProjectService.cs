using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Services
{
    public interface IProjectService
    {
        Task<Project> GetAsync(string id, string code);

        Task<IEnumerable<Project>> GetAllAsync();

        Task<Project> AddAsync(Project project);

        Task<TaskModel> UpdateAsync(string id, string code, Project project);

        Task DeleteAsync(string id, string code);
    }
}
