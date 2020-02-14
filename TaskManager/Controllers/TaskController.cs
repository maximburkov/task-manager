using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using TaskManager.Exceptions;
using TaskManager.Models;
using TaskManager.Models.DTO;
using TaskManager.QueryParameters;
using TaskManager.Services;
using Microsoft.AspNetCore.Authorization;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/projects/{projectId}/tasks")]
    [Authorize]
    public class TasksController : ControllerBase
    {

        private readonly ILogger<TasksController> _logger;
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;


        public TasksController(ILogger<TasksController> logger, ITaskService service, IMapper mapper)
        {
            _logger = logger;
            _taskService = service;
            _mapper = mapper;
        }

        [HttpGet("/api/tasks")]
        [ProducesResponseType(typeof(IEnumerable<TaskModel>), (int)HttpStatusCode.OK)]
        public async Task<IEnumerable<TaskModel>> Get([FromQuery]TasksParameters parameters)
        {
            if (!parameters.HasValues())
            {
                return await _taskService.GetAllAsync();
            }

            if (parameters.HasOnlyKeys())
            {
                var task = await _taskService.GetAsync(parameters.Id, parameters.ProjectId);
                return new List<TaskModel> {task};
            }

            return await _taskService.GetWithParameters(parameters);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TaskModel>), (int)HttpStatusCode.OK)]
        public async Task<IEnumerable<TaskModel>> GetByProjectId(string projectId)
        {
            return await _taskService.GetWithParameters(new TasksParameters {ProjectId = projectId});
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Project), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<TaskModel>> GetById(string projectId, string id)
        {
            var task = await _taskService.GetAsync(id, projectId);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Project), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<TaskModel>> Create(string projectId, TaskDto task)
        {
            var taskModel = _mapper.Map<TaskModel>(task);
            taskModel.ProjectId = projectId;
            taskModel.Id = Guid.NewGuid().ToString();
            var addedItem = await _taskService.AddAsync(taskModel);
            return CreatedAtAction(nameof(GetById), new { projectId = addedItem.ProjectId, id = addedItem.Id}, addedItem);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Project), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<TaskModel>> Update(string projectId, string id, TaskDto task)
        {
            try
            {
                var updatedTask = await _taskService.UpdateAsync(id, projectId, _mapper.Map<TaskModel>(task));
                return Ok(updatedTask);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> Delete(string projectId, string id)
        {
            try
            {
                await _taskService.DeleteAsync(id, projectId);
            }
            catch (NotFoundException e)
            {
                return NotFound(new { error = e.Message });
            }
            return NoContent();
        }
    }
}

