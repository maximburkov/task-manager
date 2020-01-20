using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.Repositories
{
    public class TaskTableStorageRepository : ITaskRepository
    {
        private ITableStorageContext _context;
        private const string TableName = "Tasks";
        private readonly CloudTableClient _tableClient;

        public TaskTableStorageRepository(ITableStorageContext context)
        {
            _context = context;
            _tableClient = _context.StorageAccount.CreateCloudTableClient();
        }

        public async Task<IEnumerable<TaskModel>> GetAllAsync()
        {
            var tasksTable = _tableClient.GetTableReference("Tasks");
            var query = new TableQuery<TaskEntity>();
            var result = new List<TaskEntity>();

            TableContinuationToken continuationToken = null;

            do
            {
                try
                {
                    var queryResults = await tasksTable.ExecuteQuerySegmentedAsync(query, continuationToken);
                    continuationToken = queryResults.ContinuationToken;


                    result.AddRange(queryResults.Results);
                }
                catch (Exception e)
                {

                }
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
