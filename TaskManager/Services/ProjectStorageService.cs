using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.WindowsAzure.Storage.Table;
using TaskManager.AzureStorage;
using TaskManager.Models;
using TaskManager.QueryParameters;

namespace TaskManager.Services
{
    public class ProjectStorageService : IProjectService
    {
        private readonly ITableStorageContext _context;
        private readonly IMapper _mapper;
        private const string TableName = "Projects";

        public ProjectStorageService(ITableStorageContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Project> GetAsync(string id, string code)
        {
            var result = await _context.GetAsync<ProjectEntity>(TableName, id, code);
            return _mapper.Map<Project>(result);
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            var projects = await _context.GetAllAsync<ProjectEntity>(TableName);
            return _mapper.Map<List<Project>>(projects);
        }

        public async Task<IEnumerable<Project>> GetWithParameters(ProjectsParameters parameters)
        {
            var queryFilters = string.Empty;

            if (!string.IsNullOrWhiteSpace(parameters.Id))
            {
                queryFilters += TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, parameters.Id);
            }
            if (!string.IsNullOrWhiteSpace(parameters.Code))
            {
                queryFilters += TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, parameters.Code);
            }
            if (!string.IsNullOrWhiteSpace(parameters.Name))
            {
                queryFilters += TableQuery.GenerateFilterCondition("Name", QueryComparisons.Equal, parameters.Name);
            }
            if (parameters.Take.HasValue)
            {

            }

            TableQuery<ProjectEntity> tableQuery = new TableQuery<ProjectEntity>().Where(queryFilters);

            var projects = await _context.QueryWithParametersAsync(TableName, tableQuery);
            return _mapper.Map<List<Project>>(projects);
        }

        public Task<Project> AddAsync(Project project)
        {
            throw new NotImplementedException();
        }

        public Task<TaskModel> UpdateAsync(string id, string code, Project project)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id, string code)
        {
            throw new NotImplementedException();
        }
    }
}
