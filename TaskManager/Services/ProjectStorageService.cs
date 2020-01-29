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
                queryFilters = TableQuery.GenerateFilterCondition(nameof(ProjectEntity.RowKey), QueryComparisons.Equal, parameters.Id);
            }
            if (!string.IsNullOrWhiteSpace(parameters.Code))
            {
                var codeCondition =
                    TableQuery.GenerateFilterCondition(nameof(ProjectEntity.PartitionKey), QueryComparisons.Equal, parameters.Code);
                queryFilters = string.IsNullOrEmpty(queryFilters) ? codeCondition : TableQuery.CombineFilters(queryFilters, TableOperators.And, codeCondition);
            }
            if (!string.IsNullOrWhiteSpace(parameters.Name))
            {
                var nameCondition =
                    TableQuery.GenerateFilterCondition(nameof(ProjectEntity.Name), QueryComparisons.Equal, parameters.Name);
                queryFilters = string.IsNullOrEmpty(queryFilters) ? nameCondition : TableQuery.CombineFilters(queryFilters, TableOperators.And, nameCondition);
            }

            TableQuery<ProjectEntity> tableQuery = new TableQuery<ProjectEntity>().Where(queryFilters);

            if (parameters.Take.HasValue)
            {
                tableQuery = tableQuery.Take(parameters.Take.Value);
            }
            //if (parameters.Offset.HasValue)
            //{
            //    //TODO: offset parameter
            //}

            var projects = await _context.QueryWithParametersAsync(TableName, tableQuery);
            return _mapper.Map<List<Project>>(projects);
        }

        public async Task<Project> AddAsync(Project project)
        {
            await _context.AddAsync(TableName, _mapper.Map<ProjectEntity>(project));
            //TODO: should we return
            return project;
        }

        public async Task<Project> UpdateAsync(string id, string code, Project project)
        {
            project.Id = id;
            project.Code = code;

            await _context.UpdateAsync(TableName, _mapper.Map<ProjectEntity>(project));
            return project;
        }

        public async Task DeleteAsync(string id, string code)
        {
            await _context.DeleteAsync<ProjectEntity>(TableName,id, code);
        }
    }
}
