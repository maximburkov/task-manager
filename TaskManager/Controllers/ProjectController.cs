using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using TaskManager.QueryParameters;
using TaskManager.Services;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ILogger<ProjectsController> _logger;
        private readonly IProjectService _projectService;

        public ProjectsController(ILogger<ProjectsController> logger, IProjectService service)
        {
            _logger = logger;
            _projectService = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Project>), (int)HttpStatusCode.OK)]
        public async Task<IEnumerable<Project>> Get(int limit = 100, int offset = 0, string id = null, string code = null, string name = null)
        {
            //if (!parameters.HasValues)
            //{
                return await _projectService.GetAllAsync();
            //}
            //else
            //{
            //    throw new NotImplementedException();
            //}
        }

        [HttpGet("{id}")]
        public string GetById(string id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}/{code}")]
        public string GetByIdAndCode(string id, string code)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public void Create()
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}/{code}")]
        public void Put(string id)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}/{code}")]
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
