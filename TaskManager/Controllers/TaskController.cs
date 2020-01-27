using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/projects/{projectId}/tasks")]
    public class TasksController : ControllerBase
    {

        private readonly ILogger<TasksController> _logger;
        private readonly ITaskService _taskService;


        public TasksController(ILogger<TasksController> logger, ITaskService service)
        {
            _logger = logger;
            _taskService = service;
        }

        [HttpGet("/api/tasks")]
        public async Task<IEnumerable<TaskModel>> Get()
        {
            return await _taskService.GetAllAsync();
        }

        [HttpGet]
        public async Task<IEnumerable<TaskModel>> GetByProjectId(string projectId)
        {
            return await _taskService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<TaskModel> GetById(string projectId, string id)
        {
            return await _taskService.GetAsync(id, projectId);
        }

        [HttpPost]
        public async Task<ActionResult<TaskModel>> Create(string projectId, TaskModel task)
        {
            var addedItem = await _taskService.AddAsync(task);
            return CreatedAtAction(nameof(Create), addedItem);
        }

        [HttpPut("{id}")]
        public Task<ActionResult<TaskModel>> Update(string projectId, string id, TaskModel task)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public Task<ActionResult> Delete(string projectId, string id)
        {
            throw new NotImplementedException();
        }
    }
}

