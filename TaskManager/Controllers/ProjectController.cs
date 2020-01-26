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
        public async Task<IEnumerable<Project>> Get([FromQuery] ProjectsParameters parameters)
        {
            if (!parameters.HasValues())
            {
                return await _projectService.GetAllAsync();
            }
            else if (parameters.HasOnlyKeys())
            {
                var project = await _projectService.GetAsync(parameters.Id, parameters.Code);
                return new List<Project>() { project };
            }
            else
            {
                return await _projectService.GetWithParameters(parameters);
            }        
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
