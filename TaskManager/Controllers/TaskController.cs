using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Logging;
using TaskManager.Services;
using TaskEntity = TaskManager.Models.TaskEntity;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {

        private readonly ILogger<TaskController> _logger;
        private readonly IRepository<TaskEntity> _repository;

        public TaskController(ILogger<TaskController> logger, IRepository<TaskEntity> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<TaskEntity> Get()
        {
            return _repository.GetAll();
        }
    }
}
