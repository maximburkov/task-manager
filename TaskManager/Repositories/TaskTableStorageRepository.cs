using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.Repositories
{
    public class TaskTableStorageRepository : BaseTableStorageRepository, ITaskRepository
    {
        private const string TableName = "Tasks";

        public TaskTableStorageRepository(ITableStorageContext context) : base(context)
        {
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

            var mappedResult = result.Select(i => new TaskModel
            {
                Description = i.Description,
                Id = i.RowKey,
                ProjectId = i.PartitionKey,
                Subject = i.Subject
            });

            return mappedResult;
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
