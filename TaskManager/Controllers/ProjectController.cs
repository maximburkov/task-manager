using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (parameters.HasOnlyKeys())
            {
                var project = await _projectService.GetAsync(parameters.Id, parameters.Code);
                return new List<Project>() { project };
            }

            return await _projectService.GetWithParameters(parameters);
        }

        [HttpGet("{id}")]
        public async Task<Project> GetById(string id)
        {
            var projects = await _projectService.GetWithParameters(new ProjectsParameters {Id = id});
            return projects.First();
        }

        [HttpGet("{id}/{code}")]
        public async Task<ActionResult<Project>> GetByIdAndCode(string id, string code)
        {
            var project = await _projectService.GetAsync(id, code);

            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [HttpPost]
        public async Task<ActionResult<Project>> Create(Project project)
        {
            var createdProject = await _projectService.AddAsync(project);
            return Ok(createdProject);
        }

        [HttpPut("{id}/{code}")]
        public void Put(string id)
        {
            
        }

        [HttpDelete("{id}/{code}")]
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
