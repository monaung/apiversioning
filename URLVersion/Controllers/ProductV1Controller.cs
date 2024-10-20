using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace URLVersion.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/product")]
    public class ProductV1Controller : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok("I am a version 1");
        }
    }
}