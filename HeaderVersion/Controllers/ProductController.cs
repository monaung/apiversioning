using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HeaderVersion.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/product")]
    [ApiController]
    public class ProductV1Controller : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok("I am a version 1");
        }
    }


    [ApiVersion("2.0")]
    [Route("api/product")]
    [ApiController]
    public class ProductV2Controller : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok("I am a version 2");
        }
    }
}