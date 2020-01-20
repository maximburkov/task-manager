using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.WindowsAzure.Storage.Table;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.Repositories
{
    public class TaskTableStorageRepository : BaseTableStorageRepository, ITaskRepository
    {
        private const string TableName = "Tasks";
        private readonly IMapper _mapper;

        public TaskTableStorageRepository(ITableStorageContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskModel>> GetAllAsync()
        {
            var tasksTable = await GetTableAsync(TableName);
            var query = new TableQuery<TaskEntity>();
            var result = new List<TaskEntity>();

            TableContinuationToken continuationToken = null;

            do
            {
                var queryResults = await tasksTable.ExecuteQuerySegmentedAsync(query, continuationToken);
                continuationToken = queryResults.ContinuationToken;
                result.AddRange(queryResults.Results);
            } while (continuationToken != null);

            return result.Select(task => _mapper.Map<TaskModel>(task));
        }

        public async Task<TaskModel> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(TaskModel newItem)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
