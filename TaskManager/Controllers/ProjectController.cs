using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            throw new NotImplementedException();
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
