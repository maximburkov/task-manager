using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Task = TaskManager.Models.Task;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        #region Stubs
        private static readonly Task[] tasks = new[]
        {
            new Task
            {
                Subject = "Development",
                Description = "Do nice"
            },
            new Task
            {
                Subject = "Analytic",
                Description = "Plan well. Very Urgent task!"
            }
        };
        #endregion

        private readonly ILogger<TaskController> _logger;

        public TaskController(ILogger<TaskController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Task> Get()
        {
            return tasks;
        }
    }
}
