using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TaskManager.AzureStorage;
using TaskManager.Models;

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

        public Task<IEnumerable<Project>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Project task)
        {
            throw new NotImplementedException();
        }
    }
}
