using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Services
{
    interface IProjectService
    {
        Task<Project> GetAsync(string id, string projectId);

        Task<IEnumerable<Project>> GetAllAsync();

        Task AddAsync(Project task);
    }
}
