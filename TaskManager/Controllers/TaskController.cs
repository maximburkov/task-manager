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
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {

        private readonly ILogger<TaskController> _logger;
        private readonly ITaskService _taskService;

        public TaskController(ILogger<TaskController> logger, ITaskService service)
        {
            _logger = logger;
            _taskService = service;
        }

        [HttpGet]
        public async Task<IEnumerable<TaskModel>> Get()
        {
            return await _taskService.GetAllAsync();
        }

        [HttpGet("{projectId}/{id}")]
        public async Task<TaskModel> Get(string projectId, string id)
        {
            return await _taskService.GetAsync(id, projectId);
        }

        [HttpPost]
        public async Task<ActionResult<TaskModel>> Post(TaskModel task)
        {
            var addedItem = await _taskService.AddAsync(task);
            return CreatedAtAction(nameof(Get), addedItem);
        }
    }
}

