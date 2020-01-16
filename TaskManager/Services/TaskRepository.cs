using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Services
{
    public class TaskRepository : IRepository<TaskEntity>
    {
        private IDbContext _context;

        public TaskRepository(IDbContext context)
        {
            _context = context;
        }

        public void Create(TaskEntity newItem)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public TaskEntity Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TaskEntity> GetAll()
        {
            return new[]
            {
                new TaskEntity
                {
                    Subject = "Development",
                    Description = "Do nice"
                },
                new TaskEntity
                {
                    Subject = "Analytic",
                    Description = "Plan well. Very Urgent task!"
                }
            };
        }
}
}
