using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JosepApp.Business.ExampleManagement.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JosepApp.Business.ExampleManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExampleController : ControllerBase
    {
        private readonly ILogger<ExampleController> _logger;
        private readonly IExampleService _exampleService;

        public ExampleController(ILogger<ExampleController> logger, IExampleService exampleService)
        {
            _logger = logger;
            _exampleService = exampleService;
        }

        [HttpGet]
        [Route("api/ping")]
        [ProducesResponseType(typeof(string),200)]
        public IActionResult Ping()
        {
            return Ok(_exampleService.GeExample());
        }
    }
}
