using System;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.AzureStorage;
using TaskManager.Models;

namespace TaskManager.Services
{
    public class TaskStorageService : ITaskService
    {
        private readonly ITableStorageContext _context;
        private readonly IMapper _mapper;
        private const string TableName = "Tasks";

        public TaskStorageService(ITableStorageContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TaskModel> GetAsync(string id, string projectId)
        {
            var task = await _context.GetAsync<TaskEntity>(TableName, id, projectId);
            return _mapper.Map<TaskModel>(task);
        }

        public async Task<IEnumerable<TaskModel>> GetAllAsync()
        {
            var tasks = await _context.GetAllAsync<TaskEntity>(TableName);
            return _mapper.Map<List<TaskModel>>(tasks);
        }

        public async Task AddAsync(TaskModel task)
        {
            task.Id = Guid.NewGuid().ToString();
            var taskEntity = _mapper.Map<TaskEntity>(task);
            await _context.AddAsync(TableName, taskEntity);
        }
    }
}
