using System;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using TaskManager.AzureStorage;
using TaskManager.Models;
using TaskManager.QueryParameters;

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

        public Task<TaskModel> GetAllByProjectIdAsync(string projectId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TaskModel>> GetWithParameters(TasksParameters parameters)
        {
            var queryFilters = string.Empty;

            if (!string.IsNullOrWhiteSpace(parameters.Id))
            {
                queryFilters = TableQuery.GenerateFilterCondition(nameof(TaskEntity.RowKey), QueryComparisons.Equal, parameters.Id);
            }
            if (!string.IsNullOrWhiteSpace(parameters.ProjectId))
            {
                var projectCondition =
                    TableQuery.GenerateFilterCondition(nameof(TaskEntity.PartitionKey), QueryComparisons.Equal, parameters.ProjectId);
                queryFilters = string.IsNullOrEmpty(queryFilters) ? projectCondition : TableQuery.CombineFilters(queryFilters, TableOperators.And, projectCondition);
            }
            if (!string.IsNullOrWhiteSpace(parameters.Subject))
            {
                var subjectCondition =
                    TableQuery.GenerateFilterCondition(nameof(TaskEntity.Subject), QueryComparisons.Equal, parameters.Subject);
                queryFilters = string.IsNullOrEmpty(queryFilters) ? subjectCondition : TableQuery.CombineFilters(queryFilters, TableOperators.And, subjectCondition);
            }

            TableQuery<TaskEntity> tableQuery = new TableQuery<TaskEntity>().Where(queryFilters);

            if (parameters.Take.HasValue)
            {
                tableQuery = tableQuery.Take(parameters.Take.Value);
            }

            var tasks = await _context.QueryWithParametersAsync(TableName, tableQuery);
            return _mapper.Map<List<TaskModel>>(tasks);
        }

        public async Task<IEnumerable<TaskModel>> GetAllAsync()
        {
            var tasks = await _context.GetAllAsync<TaskEntity>(TableName);
            return _mapper.Map<List<TaskModel>>(tasks);
        }

        public async Task<TaskModel> AddAsync(TaskModel task)
        {
            var taskEntity = _mapper.Map<TaskEntity>(task);
            await _context.AddAsync(TableName, taskEntity);
            return task;
        }

        public async Task<TaskModel> UpdateAsync(string id, string projectId, TaskModel task)
        {
            task.Id = id;
            task.ProjectId = projectId;

            await _context.UpdateAsync(TableName, _mapper.Map<TaskEntity>(task));
            return task;
        }

        public async Task DeleteAsync(string id, string projectId)
        {
            await _context.DeleteAsync<ProjectEntity>(TableName, id, projectId);
        }
    }
}
