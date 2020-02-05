using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using TaskManager.Exceptions;
using TaskManager.QueryParameters;
using TaskManager.Services;
using TaskManager.Models;
using TaskManager.Models.DTO;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ILogger<ProjectsController> _logger;
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public ProjectsController(ILogger<ProjectsController> logger, IProjectService service, IMapper mapper)
        {
            _logger = logger;
            _projectService = service;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Project>), (int)HttpStatusCode.OK)]
        [Authorize]
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
        [ProducesResponseType(typeof(Project), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Project>> GetById(string id)
        {
            var projects = await _projectService.GetWithParameters(new ProjectsParameters {Id = id});

            if (projects.Any())
            {
                return NotFound();
            }

            return Ok(projects.First());
        }

        [HttpGet("{id}/{code}")]
        [ProducesResponseType(typeof(Project), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Project>> GetByIdAndCode(string id, string code)
        {
            var project = await _projectService.GetAsync(id, code);

            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Project), (int) HttpStatusCode.Created)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Project>> Create(CreateProjectDto project)
        {
            var createdProject = await _projectService.AddAsync(_mapper.Map<Project>(project));
            return CreatedAtAction(nameof(GetByIdAndCode), new {id = createdProject.Id, code = createdProject.Code},
                createdProject);
        }

        [HttpPut("{id}/{code}")]
        [ProducesResponseType(typeof(Project), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<Project>> Put(string id, string code, UpdateProjectDto project)
        {
            try
            {
                var updatedProject = await _projectService.UpdateAsync(id, code, _mapper.Map<Project>(project));
                return Ok(updatedProject);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}/{code}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> Delete(string id, string code)
        {
            try
            {
                await _projectService.DeleteAsync(id, code);
            }
            catch (NotFoundException e)
            {
                return NotFound(new {error = e.Message});
            }
            return NoContent();
        }
    }
}
